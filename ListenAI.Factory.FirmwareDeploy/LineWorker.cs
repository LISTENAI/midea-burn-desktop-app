using System.ComponentModel;
using System.Diagnostics;
using static ListenAI.Factory.FirmwareDeploy.Constants;

namespace ListenAI.Factory.FirmwareDeploy {
    public class LineWorker {
        private int _groupId;
        private WorkerState _cskState;
        private WorkerState _wifiState;
        private Process _process;

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

            _cskWorker.RunWorkerAsync();
            _wifiWorker.RunWorkerAsync();
        }

        public void Stop() {

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

            StartProcess(BurnToolPath, flashArgs, (o, args) => {
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
            var ctrlPb = (ProgressBar) GetControl(_groupId, GroupType.Csk, GroupConfigType.Progress);
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
        }

        private void WifiFlash_Work(object? sender, DoWorkEventArgs e) {
            if (_wifiState == WorkerState.Processing) {
                return;
            }
            _wifiState = WorkerState.Processing;
        }

        private void WifiFlash_ProgressChanged(object? sender, ProgressChangedEventArgs e) {

        }

        private void WifiFlash_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e) {

        }

        private void StartProcess(string file, string args, DataReceivedEventHandler dataHandler, EventHandler exitHandler) {
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
    }
}
