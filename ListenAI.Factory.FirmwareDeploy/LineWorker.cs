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
            _state = WorkerState.Error;

            var baudRate = GetControl(_groupId, _groupType, GroupConfigType.BaudRate).Text;
            var port = GetControl(_groupId, _groupType, GroupConfigType.Port).Text;
            var file = Global.SelectedFirmwarePath;
            var burnArgs = $"-b {baudRate} -p {port} -c -t 10 -f \"{file}\" -c -m -d -a 0x0 -s";
            StartProcess(BurnToolPath, burnArgs, (sender, args) => {
                if (args.Data == null) {
                    return;
                }
                //data handler
                Debug.WriteLine(args.Data);
                //FLASH DATA SEND PROGESS:51% 
                if (args.Data.StartsWith("FLASH DATA SEND PROGESS:")) {
                    var prog = int.Parse(args.Data.Replace("FLASH DATA SEND PROGESS:", "").Replace("%", "").Trim());
                    var progCtrl = (ProgressBar)GetControl(_groupId, _groupType, GroupConfigType.Progress);
                    if (progCtrl.InvokeRequired) {
                        progCtrl.Invoke(() => {
                            progCtrl.Value = prog;
                        });
                    }
                } else if (args.Data.StartsWith("FLASH DOWNLOAD SUCCESS")) {
                    var resultCtrl = GetControl(_groupId, GroupType.Common, GroupConfigType.Result);
                    if (resultCtrl.InvokeRequired) {
                        resultCtrl.Invoke(() => {
                            resultCtrl.BackColor = ColorProcceed;
                        });
                    }
                }
            }, (sender, args) => {
                //exit handler
                _state = _process.ExitCode == 0 ? WorkerState.Success : WorkerState.Error;
                Debug.WriteLine($"Burn-tools exited with code {_process.ExitCode}");
            });
        }

        public void AbortFlash() {
            _state = WorkerState.Idle;
        }

        private void StartProcess(string file, string args, DataReceivedEventHandler dataHandler, EventHandler exitHandler) {
            var startInfo = new ProcessStartInfo(file, args) {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };

            _process = new Process();
            _process.OutputDataReceived += dataHandler;
            _process.Exited += exitHandler;
            _process.StartInfo = startInfo;
            _process.Start();
            _process.BeginOutputReadLine();
        }
    }
}
