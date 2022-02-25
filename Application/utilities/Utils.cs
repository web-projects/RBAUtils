using RBAUtils.utilities;
using System.Collections.Generic;

namespace RBAUtils.Utilities
{
    class Utils
    {
        Ingenico _ingenicoDevice = new Ingenico();

        private bool Connected;
        private string devicePort;

        private List<string> commPorts = new List<string>
        {
             "COM109" , "COM110" , "COM111", "COM112", "COM113","COM35"
        };

        public Utils()
        {
            foreach (var commPort in commPorts)
            {
                string result = _ingenicoDevice.Connect(commPort, null, Enums.IngenicoLoggingLevel.NONE);
                Connected = result.Contains("SUCCESS") ? true : false;
                if (Connected)
                {
                    devicePort = commPort;
                    break;
                }
            }
        }

        public bool IsConnected()
        {
            return Connected;
        }

        public string GetDeviceConnectedPort()
        {
            return devicePort;
        }

        public string GetTerminalModel()
        {
            return _ingenicoDevice.GetTerminalModel();
        }

        public string GetTerminalSerialNumber()
        {
            return _ingenicoDevice.GetTerminalSerialNumber();
        }

        public string GetDevicePartNumber()
        {
            return _ingenicoDevice.GetDevicePartNumber();
        }
        public string GetTerminalTimeStamp()
        {
            string terminalTime = _ingenicoDevice.GetTerminalTimeStamp();
            return terminalTime;
        }

        public string Get24RebootTime()
        {
            string deviceRebootTime = _ingenicoDevice.Get24RebootTime();
            return deviceRebootTime;
        }

        public void Set24RebootTime(string configRebootTime)
        {
            string deviceRebootTime = Get24RebootTime();

            //if the time needs to be set - then the device must reboot so that it stored the updated configuration
            if (deviceRebootTime != configRebootTime)
            {
                _ingenicoDevice.WriteConfiguration("0007", "0046", configRebootTime);
                _ingenicoDevice.Offline();
                _ingenicoDevice.HardDeviceReset();
            }
        }
    }
}
