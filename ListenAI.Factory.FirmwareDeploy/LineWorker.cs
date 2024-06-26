﻿using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using ListenAI.Factory.FirmwareDeploy.Models;
using static ListenAI.Factory.FirmwareDeploy.Constants;

namespace ListenAI.Factory.FirmwareDeploy {
    public class LineWorker {
        private int _groupId;
        private string? _chipId;
        private string _asrLogPath;
        private string _cskLogPath;
        //private StringBuilder _asrLogTemp = new StringBuilder();
        //private StringBuilder _cskLogTemp = new StringBuilder();
        private StreamWriter _asrLogTemp;
        private StreamWriter _cskLogTemp;

        private WorkerState _cskState;
        private WorkerState _wifiState;
        private Process _cskProcess;
        private Process _wifiProcess;
        private DateTime? _startAt;
        private DateTime? _endAt;
        private int _cskProgress;
        private int _wifiProgress;

        private BackgroundWorker _cskWorker;
        private BackgroundWorker _wifiWorker;

        public WorkerState State {
            get {
                switch (Global.WorkingMode) {
                    case WorkingMode.OnlineAndOffline:
                        if (_cskState == WorkerState.Error || _wifiState == WorkerState.Error) {
                            return WorkerState.Error;
                        }
                        if (_cskState == WorkerState.Success && _wifiState == WorkerState.Success) {
                            return WorkerState.Success;
                        }
                        return WorkerState.Processing;
                    case WorkingMode.OfflineOnly:
                        return _cskState;
                    default:
                        return WorkerState.Error;
                }
                
            }
        }

        public LineWorker(int groupId) { 
            _groupId = groupId;
            _chipId = null;

            var now = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            var logDir = Path.Join(Environment.CurrentDirectory, "logcat");
            _asrLogPath = Path.Join(logDir, $"asr_{_groupId}_{now}.log");
            _cskLogPath = Path.Join(logDir, $"csk_{_groupId}_{now}.log");
            if (!Directory.Exists(logDir)) {
                Directory.CreateDirectory(logDir);
            }

            _asrLogTemp = new StreamWriter(_asrLogPath);
            _cskLogTemp = new StreamWriter(_cskLogPath);
        }

        /// <summary>
        /// Start working
        /// </summary>
        public void Start() {
            _startAt = DateTime.UtcNow.AddHours(8);

            _cskWorker = new BackgroundWorker();
            _cskWorker.WorkerReportsProgress = true;
            _cskWorker.WorkerSupportsCancellation = true;
            _cskWorker.DoWork += CskFlash_Work;
            _cskWorker.ProgressChanged += CskFlash_ProgressChanged;
            _cskWorker.RunWorkerCompleted += CskFlash_RunWorkerCompleted;
            _cskWorker.RunWorkerAsync();

            if (Global.WorkingMode == WorkingMode.OnlineAndOffline) {
                _wifiWorker = new BackgroundWorker();
                _wifiWorker.WorkerReportsProgress = true;
                _wifiWorker.WorkerSupportsCancellation = true;
                _wifiWorker.DoWork += WifiFlash_Work;
                _wifiWorker.ProgressChanged += WifiFlash_ProgressChanged;
                _wifiWorker.RunWorkerCompleted += WifiFlash_RunWorkerCompleted;
                _wifiWorker.RunWorkerAsync();
            } else {
                _wifiState = WorkerState.Success;
            }
            
            _cskProgress = 0;
            _wifiProgress = 0;

            var ctrlPb = (ProgressBar)GetControl(_groupId, GroupType.Common, GroupConfigType.Progress);
            ctrlPb.Invoke(() => { ctrlPb.Value = 0; });

            var ctrlResult = GetControl(_groupId, GroupType.Common, GroupConfigType.Result);
            ctrlResult.Invoke(() => {
                ctrlResult.BackColor = ColorProcessing;
                ctrlResult.Text = "烧录中...0.0%";
            });
        }

        /// <summary>
        /// Stop working
        /// </summary>
        public void Stop() {
            if (_cskState == WorkerState.Processing) {
                _cskWorker.CancelAsync();
                _cskProcess?.Kill(true);
                _cskState = WorkerState.Error;
            }

            if (_wifiState == WorkerState.Processing) {
                _wifiWorker?.CancelAsync();
                _wifiProcess?.Kill(true);
                _cskState = WorkerState.Error;
            }
        }

        /// <summary>
        /// Just let worker knows how to flash CSK chips
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CskFlash_Work(object? sender, DoWorkEventArgs e) {
            if (_cskState == WorkerState.Processing) {
                return;
            }
            _cskState = WorkerState.Processing;
            var groupType = GroupType.Csk;

            var baudRate = GetControl(_groupId, groupType, GroupConfigType.BaudRate).Text;
            var port = "COM";
            var portCtrl = GetControl(_groupId, groupType, GroupConfigType.Port);
            portCtrl.Invoke(() => {
                port = $"COM{((ComboBox)portCtrl).Text}";
            });
            var fwPackPath = Global.SelectedFirmware.FullPath;
            var fwFile = Global.SelectedFirmware.GetFirmware(GroupType.Csk);
            if (fwFile == null) {
                return;
            }
            string flashArgs = string.Empty;
            switch (Global.WorkingMode) {
                case WorkingMode.OnlineAndOffline:
                default:
                    flashArgs = $"-b {baudRate} -p {port} -c -t 10 -f \"{Path.Combine(fwPackPath, fwFile.Name)}\" -c -l -m -d -a 0x{fwFile.Offset:x} -s";
                    break;
                case WorkingMode.OfflineOnly:
                    flashArgs = $"-b {baudRate} -p {port} -c -t 10 -f \"{Path.Combine(fwPackPath, fwFile.Name)}\" -c -m -d -s";
                    break;
            }
            var timeoutCount = 0;

            StartProcessAsync(BurnToolPath, flashArgs, (_, args) => {
                if (args.Data == null) {
                    return;
                }
                if ((sender as BackgroundWorker).CancellationPending) {
                    _cskProcess?.Kill();
                    (sender as BackgroundWorker)?.TryToReportProgress(-1, "Operation cancelled!");
                    return;
                }
                //data handler
                WriteDebugLog(args.Data, type: FirmwareType.Csk);
                if (args.Data.StartsWith("FLASH DATA SEND PROGESS:")) {
                    try {
                        var prog = int.Parse(args.Data.Replace("FLASH DATA SEND PROGESS:", "").Replace("%", "").Trim());
                        timeoutCount = 0;
                        (sender as BackgroundWorker)?.TryToReportProgress(prog, null);
                    }
                    catch {
                        timeoutCount++;
                    }
                }
                else if (args.Data.StartsWith("FLASH DOWNLOAD SUCCESS")) {
                    (sender as BackgroundWorker)?.TryToReportProgress(100, null);
                }
                else if (args.Data.StartsWith("RECEIVE OVERTIME") ||
                           args.Data.StartsWith("CONNECT CHIP OVERTIME")) {
                    timeoutCount++;
                    if (timeoutCount >= 10) {
                        _cskProcess?.Kill();
                        _cskState = WorkerState.Error;
                        Debug.WriteLine("Too many timeout exception, flash aborted!");
                        (sender as BackgroundWorker)?.TryToReportProgress(-1, "Too many timeout exception, flash aborted!");
                    }
                }
                else if (args.Data.StartsWith("SERILA PORT NUMBER ERROR") ||
                           args.Data.StartsWith("SYNC FORMAT ERROR") ||
                           args.Data.StartsWith("MD5 CHECK ERROR")) {
                    _cskProcess?.Kill();
                    _cskState = WorkerState.Error;
                    Debug.WriteLine($"Critical error, flash aborted! Msg = {args.Data}");
                    (sender as BackgroundWorker)?.TryToReportProgress(-1, $"Critical error, flash aborted! Msg = {args.Data}");
                } else if (args.Data.StartsWith("uuid:")) {
                    _chipId = args.Data.Replace("uuid:", "");
                    Debug.WriteLine($"Chip ID = {args.Data.Replace("uuid:", "")}");
                }
            }, (_, _) => {
                Debug.WriteLine($"Burn-tools exited with code {_cskProcess.ExitCode}");
                if (_cskProcess.ExitCode != 0) {
                    (sender as BackgroundWorker)?.TryToReportProgress(-1, $"Burn-tools exited with code {_cskProcess.ExitCode}");
                }
            });

            while (!_cskProcess.HasExited) {
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// CSK workers report their progress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CskFlash_ProgressChanged(object? sender, ProgressChangedEventArgs e) {
            _cskProgress = e.ProgressPercentage;

            var ctrlPb = (ProgressBar) GetControl(_groupId, GroupType.Common, GroupConfigType.Progress);
            if (ctrlPb == null || e.ProgressPercentage < 0) {
                return;
            }

            var ctrlResult = (Button)GetControl(_groupId, GroupType.Common, GroupConfigType.Result);
            if (ctrlResult == null) {
                return;
            }

            ctrlPb.Invoke(() => {
                switch (Global.WorkingMode) {
                    case WorkingMode.OnlineAndOffline:
                    default:
                        ctrlPb.Value = _cskProgress + _wifiProgress;
                        break;
                    case WorkingMode.OfflineOnly:
                        ctrlPb.Value = _cskProgress * 2;
                        break;
                }
            });
            ctrlResult.Invoke(() => {
                var fullProgress = Global.WorkingMode == WorkingMode.OnlineAndOffline ? (_cskProgress + _wifiProgress) / 2.0m : _cskProgress;
                fullProgress = fullProgress >= 100.0m ? 99.9m : fullProgress;
                ctrlResult.Text = $"烧录中...{fullProgress:0.0}%";
            });
        }

        /// <summary>
        /// Things to do after csk flash completed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CskFlash_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e) {
            if (_cskProgress < 100 || e.Cancelled || _wifiState == WorkerState.Error) {
                _cskState = WorkerState.Error;
                _wifiWorker?.CancelAsync();
                _wifiProcess?.Kill();
                _wifiState = WorkerState.Error;
            }
            else {
                _cskState = WorkerState.Success;
            }

            Thread.Sleep(new Random().Next(500, 2000));
            if (_cskState != WorkerState.Processing && _wifiState != WorkerState.Processing) {
                ReportResultToUi();
            }
        }

        /// <summary>
        /// Just let worker knows how to flash WB01 chips
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WifiFlash_Work(object? sender, DoWorkEventArgs e) {
            if (Global.WorkingMode == WorkingMode.OfflineOnly) {
                return;
            }

            if (_wifiState == WorkerState.Processing) {
                return;
            }
            if (Global.SelectedFirmware == null) {
                Debug.WriteLine("[ASR] 未选择固件。");
                (sender as BackgroundWorker)?.TryToReportProgress(-1, "未选择固件。");
                return;
            }
            _wifiState = WorkerState.Processing;
            var groupType = GroupType.Wifi;


            var port = "COM";
            var portCtrl = GetControl(_groupId, groupType, GroupConfigType.Port);
            portCtrl.Invoke(() => {
                port = $"COM{((ComboBox)portCtrl).Text}";
            });

            //step 1 - Check for device
            WriteDebugLog("[ASR] step 1...");
            var args = $"dl --port {port} --chip 2";
            WriteDebugLog($"[ASR] step 1, args = {args}");
            var runAsr = StartProcessSync(ASRToolPath, args);
            WriteDebugLog($"[ASR] step 1 debug\ncode = {runAsr.ExitCode}");
            if (runAsr.ExitCode == 0) {
                (sender as BackgroundWorker)?.TryToReportProgress(20);
            }
            else {
                WriteDebugLog($"[ASR] 无法与模块通讯，请检查连线。Code = {runAsr.ExitCode}");
                (sender as BackgroundWorker)?.TryToReportProgress(-1, $"无法与模块通讯，请检查连线。Code = {runAsr.ExitCode}");
                return;
            }

            //step 2 - flash firmware
            WriteDebugLog("[ASR] step 2...");
            var fwInfo = Global.SelectedFirmware?.GetFirmware(GroupType.Wifi);
            args = $"burn --port {port} --chip 2 --path \"{Path.Combine(Global.SelectedFirmware.FullPath, fwInfo.Name)}\" --multi";
            WriteDebugLog($"[ASR] step 2, args = {args}");
            runAsr = StartProcessSync(ASRToolPath, args);
            WriteDebugLog($"[ASR] step 2 debug\ncode = {runAsr.ExitCode}");
            if (runAsr.ExitCode == 0) {
                (sender as BackgroundWorker)?.TryToReportProgress(80);
            }
            else {
                WriteDebugLog($"[ASR] 烧录失败。Code = {runAsr.ExitCode}");
                (sender as BackgroundWorker)?.TryToReportProgress(-1, $"烧录失败。Code = {runAsr.ExitCode}");
                return;
            }

            //step 3 - verify firmware flashed
            WriteDebugLog("[ASR] step 3...");
            args = $"verify --port {port} --chip 2 --path \"{Path.Combine(Global.SelectedFirmware.FullPath, fwInfo.Name)}\" --multi";
            WriteDebugLog($"[ASR] step 3, args = {args}");
            runAsr = StartProcessSync(ASRToolPath, args);
            WriteDebugLog($"[ASR] step 3 debug\ncode = {runAsr.ExitCode}");
            if (runAsr.ExitCode == 0) {
                (sender as BackgroundWorker)?.TryToReportProgress(100);
            }
            else {
                WriteDebugLog($"[ASR] 校验失败。Code = {runAsr.ExitCode}");
                (sender as BackgroundWorker)?.TryToReportProgress(-1, $"校验失败。Code = {runAsr.ExitCode}");
                return;
            }

            WriteDebugLog("[ASR] Completed!");
        }

        private void WifiFlash_ProgressChanged(object? sender, ProgressChangedEventArgs e) {
            if (Global.WorkingMode == WorkingMode.OfflineOnly) {
                return;
            }

            _wifiProgress = e.ProgressPercentage;

            var ctrlPb = (ProgressBar)GetControl(_groupId, GroupType.Common, GroupConfigType.Progress);
            if (ctrlPb == null || e.ProgressPercentage < 0) {
                return;
            }

            var ctrlResult = (Button)GetControl(_groupId, GroupType.Common, GroupConfigType.Result);
            if (ctrlResult == null) {
                return;
            }

            ctrlPb.Invoke(() => {
                ctrlPb.Value = _cskProgress + _wifiProgress;
            });
            ctrlResult.Invoke(() => {
                var fullProgress = (_cskProgress + _wifiProgress) / 2.0m;
                fullProgress = fullProgress >= 100.0m ? 99.9m : fullProgress;
                ctrlResult.Text = $"烧录中...{fullProgress:0.0}%";
            });
        }

        private void WifiFlash_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e) {
            if (Global.WorkingMode == WorkingMode.OfflineOnly) {
                return;
            }

            if (_wifiProgress < 100 || e.Cancelled || _cskState == WorkerState.Error) {
                _wifiState = WorkerState.Error;
                _cskWorker.CancelAsync();
                _cskProcess?.Kill();
                _cskState = WorkerState.Error;
            }
            else {
                _wifiState = WorkerState.Success;
            }

            Thread.Sleep(new Random().Next(500, 2000));
            if (_cskState != WorkerState.Processing && _wifiState != WorkerState.Processing) {
                ReportResultToUi();
            }
        }

        /// <summary>
        /// Report flashing result to ui
        /// </summary>
        private void ReportResultToUi() {
            SaveLog();
            Utils.KillProcessByName(ASRToolExeName);
            Utils.KillProcessByName(BurnToolExeName);

            var passFailIndicator = (Button) GetControl(_groupId, GroupType.Common, GroupConfigType.Result);
            if (passFailIndicator == null) {
                return;
            }
            var bgc = _cskState == WorkerState.Success && _wifiState == WorkerState.Success ? 
                ColorProcceed : ColorBlock;
            var text = _cskState == WorkerState.Success && _wifiState == WorkerState.Success ? "烧录成功" : "烧录失败";

            if (passFailIndicator.InvokeRequired) {
                passFailIndicator.Invoke(() => {
                    passFailIndicator.BackColor = bgc;
                    passFailIndicator.Text = text;
                });
            }
            else {
                passFailIndicator.BackColor = bgc;
                passFailIndicator.Text = text;
            }

            Global.WorkersPool.Remove(_groupId);
            if (Global.WorkersPool.Count == 0) {
                Global.AllWorkersCompleted.Invoke(this, null);
            }
        }

        /// <summary>
        /// Save log of flash this time
        /// </summary>
        private void SaveLog() {
            lock (Global.LogOperationLock) {
                if (_endAt.HasValue) {
                    return;
                }
                _endAt = DateTime.UtcNow.AddHours(8);
                _asrLogTemp.Flush();
                _cskLogTemp.Flush();
                _asrLogTemp.Close();
                _cskLogTemp.Close();

                if (!Directory.Exists(LogDirPath)) {
                    Directory.CreateDirectory(LogDirPath);
                }

                var logPath = Path.Combine(LogDirPath, $"{DateTime.UtcNow.AddHours(8):yyyy'-'MM'-'dd}.txt");
                var encoding = new UTF8Encoding(false);

                if (!File.Exists(logPath)) {
                    File.WriteAllText(logPath, "ChipID,mes指令单号,产品编号,产品名称,规格型号,烧录开始时间,烧录结束时间,烧录人,烧录程序名,烧录机器编号,烧录结果,产品序列号（按年月日5位流水码）\r\n", encoding);
                }

                var sn = ((TextBox)GetControl(_groupId, GroupType.Common, GroupConfigType.Serial))?.Text;
                var isSuccess = _cskState == WorkerState.Success && _wifiState == WorkerState.Success ? "OK" : "NG";

                var mesRecord = Global.MesRecord ?? new MesRecord();
                File.AppendAllText(logPath, $"{_chipId},{mesRecord.MesCmdId},{mesRecord.ProductId},{mesRecord.ProductName},{mesRecord.ProductModel}," +
                                           $"{_startAt},{_endAt},{mesRecord.FlashOperator},{mesRecord.FlashToolName},{mesRecord.FlashMachineId}," +
                                           $"{isSuccess},{sn},{_groupId}\r\n");
            }
        }

        
        private void StartProcessAsync(string file, string args, DataReceivedEventHandler dataHandler, EventHandler exitHandler) {
            var startInfo = new ProcessStartInfo(file, args) {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };

            _cskProcess = new Process();
            _cskProcess.EnableRaisingEvents = true;
            _cskProcess.OutputDataReceived += dataHandler;
            _cskProcess.Exited += exitHandler;
            _cskProcess.StartInfo = startInfo;
            _cskProcess.Start();
            _cskProcess.BeginOutputReadLine();
        }

        private ProcessExitInfo StartProcessSync(string file, string args) {
            var startInfo = new ProcessStartInfo(file, args) {
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            _wifiProcess = new Process();
            _wifiProcess.EnableRaisingEvents = true;
            _wifiProcess.OutputDataReceived += (_, args) => {
                if (args.Data != null) {
                    WriteDebugLog(args.Data);
                }
            };
            _wifiProcess.ErrorDataReceived += (_, args) => {
                if (args.Data != null) {
                    WriteDebugLog(args.Data);
                }
            };
            _wifiProcess.StartInfo = startInfo;
            _wifiProcess.Start();
            _wifiProcess.BeginOutputReadLine();
            _wifiProcess.BeginErrorReadLine();

            _wifiProcess.WaitForExit();

            return new ProcessExitInfo {
                ExitCode = _wifiProcess.ExitCode,
                StdErr = "",
                StdOut = ""
            };
        }

        private void WriteDebugLog(string log, bool noTimestamp = false, FirmwareType type = FirmwareType.Asr) {
            var timeNow = DateTime.UtcNow;
            Debug.WriteLine($"[{timeNow}] {log}");
            try {
                switch (type) {
                    case FirmwareType.Asr:
                        _asrLogTemp.WriteLine(noTimestamp ? log : $"[{timeNow}] {log}");
                        break;
                    case FirmwareType.Csk:
                        _cskLogTemp.WriteLine(noTimestamp ? log : $"[{timeNow}] {log}");
                        break;
                }
            } catch { }
        }
    }
}
