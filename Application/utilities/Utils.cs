using RBAUtils.utilities;
using System.Collections.Generic;

namespace RBAUtils.Utilities
{
    class Utils
    {
        Ingenico _ingenicoDevice = new Ingenico();

        private bool Connected;
        private Dictionary<string, string> commPorts = new Dictionary<string, string>
        {
            { "iSC250", "COM109" },
            { "iPP350", "COM110" },
            { "iSC480", "COM111" },
            { "iPP320", "COM113" },
        };

        public Utils()
        {
            foreach (var commPort in commPorts.Values)
            {
                string result = _ingenicoDevice.Connect(commPort, null, Enums.IngenicoLoggingLevel.NONE);
                Connected = result.Contains("SUCCESS") ? true : false;
                if (Connected)
                {
                    break;
                }
            }
        }

        public bool IsConnected()
        {
            return Connected;
        }

        public string GetTerminalModel()
        {
            return _ingenicoDevice.GetTerminalModel();
        }

        public string GetTerminalSerialNumber()
        {
            return _ingenicoDevice.GetTerminalSerialNumber();
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
