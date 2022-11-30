using System.IO.Ports;

namespace ListenAI.Factory.FirmwareDeploy {
    public class LineWorker {
        /// <summary>
        /// Check if COM port(s) is/are available before everything starts.
        /// </summary>
        /// <returns></returns>
        public static bool CheckComPorts(Constants.GroupType groupType) {
            var allPorts = SerialPort.GetPortNames().ToHashSet();
            var availablePorts = 0;
            var errorPorts = 0;

            for (int i = 1; i <= Global.GroupCount; i++) {
                var targetCskControl = Constants.GetControl(i, groupType, Constants.GroupConfigType.Port);
                if (string.IsNullOrWhiteSpace(targetCskControl.Text)) {
                    continue;
                }

                if (!targetCskControl.Text.StartsWith("COM") || !allPorts.Contains(targetCskControl.Text)) {
                    errorPorts++;
                    MessageBox.Show($"模组{Utils.ConvertToChineseChars(i)}串口设置错误", "错误");
                    continue;
                }

                try {
                    var comPort = targetCskControl.Text;
                    var baudRate = int.Parse(Constants.GetControl(i, groupType, Constants.GroupConfigType.BaudRate).Text);
                    var dataBits = int.Parse(Constants.GetControl(i, groupType, Constants.GroupConfigType.Databits).Text);
                    var parity = int.Parse(Constants.GetControl(i, groupType, Constants.GroupConfigType.Parity).Text);
                    var stopBits = int.Parse(Constants.GetControl(i, groupType, Constants.GroupConfigType.Stopbits).Text);

                    var portOpenResult = IsComPortWorking(comPort, baudRate, dataBits, parity, stopBits);
                    if (!portOpenResult) {
                        errorPorts++;
                        MessageBox.Show($"模组{Utils.ConvertToChineseChars(i)}串口无法打开", "错误");
                        continue;
                    }
                } catch {
                    errorPorts++;
                    MessageBox.Show($"模组{Utils.ConvertToChineseChars(i)}串口无法打开", "错误");
                    continue;
                }
                
                availablePorts++;
            }

            return errorPorts == 0 && availablePorts > 0;
        }

        /// <summary>
        /// [INTERNAL USE ONLY] Check if com port could be opened
        /// </summary>
        /// <param name="comPort">com port</param>
        /// <param name="baudRate">baud rate</param>
        /// <param name="databits">data bits</param>
        /// <param name="parity">parity</param>
        /// <param name="stopBits">stop bits</param>
        /// <returns></returns>
        private static bool IsComPortWorking(string comPort, int baudRate, int databits, int parity, int stopBits) {
            try {
                var port = new SerialPort(comPort, baudRate, (Parity) parity, databits, (StopBits) stopBits);

                port.Open();
                port.Close();

                return true;
            }
            catch {
                return false;
            }
        }
    }
}
