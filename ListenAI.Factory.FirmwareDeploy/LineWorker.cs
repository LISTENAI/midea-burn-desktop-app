using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using ListenAI.Factory.FirmwareDeploy.Models;
using static ListenAI.Factory.FirmwareDeploy.Constants;

namespace ListenAI.Factory.FirmwareDeploy {
    public class LineWorker {
        private int _groupId;
        private WorkerState _cskState;
        private WorkerState _wifiState;
        private Process _process;
        private DateTime _startAt;
        private DateTime _endAt;

        private BackgroundWorker _cskWorker;
        private BackgroundWorker _wifiWorker;

        public WorkerState CskState => _cskState;
        public WorkerState WifiState => _wifiState;

        public LineWorker(int groupId) { 
            _groupId = groupId;
        }

        public void Start() {
            _cskWorker = new BackgroundWorker();
            _cskWorker.WorkerReportsProgress = true;
            _cskWorker.WorkerSupportsCancellation = true;
            _cskWorker.DoWork += CskFlash_Work;
            _cskWorker.ProgressChanged += CskFlash_ProgressChanged;
            _cskWorker.RunWorkerCompleted += CskFlash_RunWorkerCompleted;

            _wifiWorker = new BackgroundWorker();
            _wifiWorker.WorkerReportsProgress = true;
            _wifiWorker.WorkerSupportsCancellation = true;
            _wifiWorker.DoWork += WifiFlash_Work;
            _wifiWorker.ProgressChanged += WifiFlash_ProgressChanged;
            _wifiWorker.RunWorkerCompleted += WifiFlash_RunWorkerCompleted;

            _startAt = DateTime.UtcNow.AddHours(8);
            _cskWorker.RunWorkerAsync();
            _wifiWorker.RunWorkerAsync();
        }

        public void Stop() {
            if (_cskState == WorkerState.Processing) {
                _cskWorker.CancelAsync();
            }

            if (_wifiState == WorkerState.Processing) {
                _wifiWorker.CancelAsync();
            }
        }

        private void CskFlash_Work(object? sender, DoWorkEventArgs e) {
            if (_cskState == WorkerState.Processing) {
                return;
            }
            _cskState = WorkerState.Processing;
            var _groupType = GroupType.Csk;

            var baudRate = GetControl(_groupId, _groupType, GroupConfigType.BaudRate).Text;
            var port = GetControl(_groupId, _groupType, GroupConfigType.Port).Text;
            var fwPackPath = Global.SelectedFirmware.FullPath;
            var fwFile = Global.SelectedFirmware.GetFirmware(GroupType.Csk);
            if (fwFile == null) {
                return;
            }
            var flashArgs = $"-b {baudRate} -p {port} -c -t 10 -f \"{Path.Combine(fwPackPath, fwFile.Name)}\" -c -m -d -a 0x{fwFile.Offset:x} -s";
            var timeoutCount = 0;

            StartProcessAsync(BurnToolPath, flashArgs, (o, args) => {
                if (args.Data == null) {
                    return;
                }
                if (e.Cancel) {
                    _process.Kill();
                    throw new Exception("Operation cancelled!");
                }
                //data handler
                Debug.WriteLine(args.Data);
                if (args.Data.StartsWith("FLASH DATA SEND PROGESS:")) {
                    try {
                        var prog = int.Parse(args.Data.Replace("FLASH DATA SEND PROGESS:", "").Replace("%", "").Trim());
                        timeoutCount = 0;
                        _cskWorker.ReportProgress(prog);
                    }
                    catch {
                        timeoutCount++;
                    }
                }
                else if (args.Data.StartsWith("FLASH DOWNLOAD SUCCESS")) {
                    _cskWorker.ReportProgress(100);
                }
                else if (args.Data.StartsWith("RECEIVE OVERTIME") ||
                           args.Data.StartsWith("CONNECT CHIP OVERTIME")) {
                    timeoutCount++;
                    if (timeoutCount >= 10) {
                        _process.Kill();
                        Debug.WriteLine("Too many timeout exception, flash aborted!");
                        throw new Exception("Too many timeout exception, flash aborted!");
                    }
                }
                else if (args.Data.StartsWith("SERILA PORT NUMBER ERROR") ||
                           args.Data.StartsWith("SYNC FORMAT ERROR") ||
                           args.Data.StartsWith("MD5 CHECK ERROR")) {
                    _process.Kill();
                    Debug.WriteLine($"Critical error, flash aborted! Msg = {args.Data}");
                    throw new Exception($"Critical error, flash aborted! Msg = {args.Data}");
                }
            }, (o, args) => {
                Debug.WriteLine($"Burn-tools exited with code {_process.ExitCode}");
                if (_process.ExitCode != 0) {
                    throw new Exception($"Burn-tools exited with code {_process.ExitCode}");
                }
            });
        }

        private void CskFlash_ProgressChanged(object? sender, ProgressChangedEventArgs e) {
            var ctrlPb = (ProgressBar) GetControl(_groupId, GroupType.Common, GroupConfigType.Progress);
            if (ctrlPb == null) {
                return;
            }

            if (ctrlPb.InvokeRequired) {
                ctrlPb.Invoke(() => {
                    ctrlPb.Value = e.ProgressPercentage;
                });
            }
            else {
                ctrlPb.Value = e.ProgressPercentage;
            }
        }

        private void CskFlash_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e) {
            if (e.Error != null) {
                _cskState = WorkerState.Error;
                _wifiWorker.CancelAsync();
            }
            else {
                _cskState = WorkerState.Success;
            }

            Thread.Sleep(new Random().Next(3000, 6000));
            if (_cskState != WorkerState.Processing && _wifiState != WorkerState.Processing) {
                ReportResultToUi();
            }
        }

        private void WifiFlash_Work(object? sender, DoWorkEventArgs e) {
            if (_wifiState == WorkerState.Processing) {
                return;
            }
            _wifiState = WorkerState.Processing;
            var _groupType = GroupType.Wifi;

            var port = GetControl(_groupId, _groupType, GroupConfigType.Port).Text;

            //step 1 - Check for device
            var args = $"dl --port {port} --chip 2";
            var runAsr = StartProcessSync(ASRToolPath, args);
            if (runAsr.ExitCode == 0) {
                _wifiWorker.ReportProgress(20);
            }
            else {
                throw new Exception($"无法与模块通讯，请检查连线。Code = {runAsr.ExitCode}");
            }

            //step 2 - flash firmware
            var fwInfo = Global.SelectedFirmware.GetFirmware(GroupType.Wifi);
            args = $"burn --port {port} --chip 2 --path {Path.Combine(Global.SelectedFirmware.FullPath, fwInfo.Name)} --multi";
            runAsr = StartProcessSync(ASRToolPath, args);
            if (runAsr.ExitCode == 0) {
                _wifiWorker.ReportProgress(80);
            }
            else {
                throw new Exception($"烧录失败。Code = {runAsr.ExitCode}");
            }

            //step 3 - verify firmware flashed
            args = $"verify --port {port} --chip 2 --path {Path.Combine(Global.SelectedFirmware.FullPath, fwInfo.Name)} --multi";
            runAsr = StartProcessSync(ASRToolPath, args);
            if (runAsr.ExitCode == 0) {
                _wifiWorker.ReportProgress(100);
            }
            else {
                throw new Exception($"校验失败。Code = {runAsr.ExitCode}");
            }
        }

        private void WifiFlash_ProgressChanged(object? sender, ProgressChangedEventArgs e) {
            
        }

        private void WifiFlash_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e) {
            if (e.Error != null) {
                _wifiState = WorkerState.Error;
                _cskWorker.CancelAsync();
            }
            else {
                _wifiState = WorkerState.Success;
            }

            Thread.Sleep(new Random().Next(3000, 6000));
            if (_cskState != WorkerState.Processing && _wifiState != WorkerState.Processing) {
                ReportResultToUi();
            }
        }

        private void ReportResultToUi() {
            SaveLog();

            var passFailIndicator = (Button) GetControl(_groupId, GroupType.Common, GroupConfigType.Result);
            if (passFailIndicator == null) {
                return;
            }
            var bgc = _cskState == WorkerState.Success && _wifiState == WorkerState.Success ? 
                ColorProcceed : ColorBlock;

            if (passFailIndicator.InvokeRequired) {
                passFailIndicator.Invoke(() => {
                    passFailIndicator.BackColor = bgc;
                });
            }
            else {
                passFailIndicator.BackColor = bgc;
            }
        }

        private void SaveLog() {
            _endAt = DateTime.UtcNow.AddHours(8);

            lock (Global.LogOperationLock) {
                if (!Directory.Exists(LogDirPath)) {
                    Directory.CreateDirectory(LogDirPath);
                }

                var logPath = Path.Combine(LogDirPath, $"{DateTime.UtcNow.AddHours(8):YYYY'-'MM'-'dd}.txt");
                var encoding = new UTF8Encoding(false);

                if (!File.Exists(logPath)) {
                    File.WriteAllText(logPath, "mes指令单号,产品编号,产品名称,规格型号,烧录开始时间,烧录结束时间,烧录人,烧录程序名,烧录机器编号,烧如结果,产品序列号（按年月日5位流水码）\r\n", encoding);
                }

                var sn = ((TextBox)GetControl(_groupId, GroupType.Common, GroupConfigType.Serial))?.Text;
                var isSuccess = _cskState == WorkerState.Success && _wifiState == WorkerState.Success ? "OK" : "NG";

                File.WriteAllText(logPath, $"{Global.MesRecord.MesCmdId},{Global.MesRecord.ProductId},{Global.MesRecord.ProductName},{Global.MesRecord.ProductModel}," +
                                           $"{_startAt},{_endAt},{Global.MesRecord.FlashOperator},{Global.MesRecord.FlashToolName},{Global.MesRecord.FlashMachineId}," +
                                           $"{isSuccess},{sn}\r\n");
            }
        }

        private void StartProcessAsync(string file, string args, DataReceivedEventHandler dataHandler, EventHandler exitHandler) {
            var startInfo = new ProcessStartInfo(file, args) {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };

            _process = new Process();
            _process.EnableRaisingEvents = true;
            _process.OutputDataReceived += dataHandler;
            _process.Exited += exitHandler;
            _process.StartInfo = startInfo;
            _process.Start();
            _process.BeginOutputReadLine();
        }

        private ProcessExitInfo StartProcessSync(string file, string args) {
            var startInfo = new ProcessStartInfo(file, args) {
                UseShellExecute = false,
                CreateNoWindow = true
            };

            _process = new Process();
            _process.StartInfo = startInfo;
            _process.Start();

            _process.WaitForExit();

            return new ProcessExitInfo() {
                ExitCode = _process.ExitCode,
                StdErr = _process.StandardError.ReadToEnd(),
                StdOut = _process.StandardOutput.ReadToEnd()
            };
        }
    }
}
