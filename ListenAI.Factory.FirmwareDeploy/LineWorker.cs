using System.Diagnostics;
using static ListenAI.Factory.FirmwareDeploy.Constants;

namespace ListenAI.Factory.FirmwareDeploy {
    public class LineWorker {
        private int _groupId;
        private GroupType _groupType;
        private WorkerState _state;
        private Process _process;

        public WorkerState State => _state;

        public LineWorker(int groupId, GroupType groupType) { 
            _groupId = groupId;
            _groupType = groupType;
        }

        public void Flash() {
            if (_state == WorkerState.Processing) {
                return;
            }
            _state = WorkerState.Processing;

            var baudRate = GetControl(_groupId, _groupType, GroupConfigType.BaudRate).Text;
            var port = GetControl(_groupId, _groupType, GroupConfigType.Port).Text;
            var fwPackPath = Global.SelectedFirmware.FullPath;
            var fwFile = Global.SelectedFirmware.GetFirmware(GroupType.Csk);
            if (fwFile == null) {
                return;
            }
            var burnArgs = $"-b {baudRate} -p {port} -c -t 10 -f \"{Path.Combine(fwPackPath, fwFile.Name)}\" -c -m -d -a 0x{fwFile.Offset:x} -s";
            var timeoutCount = 0;
            var colorChangedToProcessing = false;
            StartProcess(BurnToolPath, burnArgs, (sender, args) => {
                if (args.Data == null) {
                    return;
                }
                //data handler
                Debug.WriteLine(args.Data);
                if (args.Data.StartsWith("FLASH DATA SEND PROGESS:")) {
                    try {
                        var prog = int.Parse(args.Data.Replace("FLASH DATA SEND PROGESS:", "").Replace("%", "").Trim());
                        var progCtrl = (ProgressBar)GetControl(_groupId, _groupType, GroupConfigType.Progress);
                        progCtrl.Invoke(() => { progCtrl.Value = prog; });
                        timeoutCount = 0;

                        if (!colorChangedToProcessing) {
                            var resultCtrl = GetControl(_groupId, GroupType.Common, GroupConfigType.Result);
                            resultCtrl.Invoke(() => { resultCtrl.BackColor = ColorProcessing; });
                            colorChangedToProcessing = true;
                        }
                    }
                    catch {
                        timeoutCount++;
                    }
                } else if (args.Data.StartsWith("FLASH DOWNLOAD SUCCESS")) {
                    var resultCtrl = GetControl(_groupId, GroupType.Common, GroupConfigType.Result);
                    resultCtrl.Invoke(() => {
                        resultCtrl.BackColor = ColorProcceed;
                    });
                } else if (args.Data.StartsWith("RECEIVE OVERTIME") ||
                           args.Data.StartsWith("CONNECT CHIP OVERTIME")) {
                    timeoutCount++;
                    if (timeoutCount >= 10) {
                        _process.Kill();
                        _state = WorkerState.Error;
                        Debug.WriteLine("Too many timeout exception, flash aborted!");
                    }
                } else if (args.Data.StartsWith("SERILA PORT NUMBER ERROR") ||
                           args.Data.StartsWith("SYNC FORMAT ERROR") ||
                           args.Data.StartsWith("MD5 CHECK ERROR")) {
                    _process.Kill();
                    _state = WorkerState.Error;
                    Debug.WriteLine("Too many timeout exception, flash aborted!");
                }
            }, (sender, args) => {
                //exit handler
                _state = _process.ExitCode == 0 ? WorkerState.Success : WorkerState.Error;
                Debug.WriteLine($"Burn-tools exited with code {_process.ExitCode}");
                if (_state == WorkerState.Error) {
                    var resultCtrl = GetControl(_groupId, GroupType.Common, GroupConfigType.Result);
                    resultCtrl.Invoke(() => { resultCtrl.BackColor = ColorBlock; });
                }
            });
        }

        public void AbortFlash() {
            _state = WorkerState.Idle;
            _process.Kill();
            Debug.WriteLine("Too many timeout exception, flash aborted!");
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
