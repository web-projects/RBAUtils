using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RBA_SDK;

namespace RBAUtils
{
    class RBAUtils
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

        public RBAUtils()
        {
            foreach(var commPort in commPorts.Values)
            { 
                string result = _ingenicoDevice.Connect(commPort, null, IngenicoLoggingLevel.NONE);
                Connected = result.Contains("SUCCESS") ? true : false;
                if(Connected)
                {
                    break;
                }
            }
        }

        public bool IsConnected()
        {
            return Connected;
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

    class Ingenico
    {
        private bool Connected;
        public int ComBaudRate = 115200;
        public int ComDataBits = 8;

        public bool OnDemandSet { get; set; }

        public string Connect(string port, LogHandler traceLog, IngenicoLoggingLevel logLevel)
        {
            //RBA_API.logHandler = new LogHandler(traceLog);
            //RBA_API.SetDefaultLogLevel((LOG_LEVEL)logLevel);

            RBA_API.Initialize();

            //RBA_API.SetNotifyRbaDisconnected(new DisconnectHandler(DeviceConnectionEvent));

            //RBA_API.pinpadHandler = new PinPadMessageHandler(pinpadHandler);

            ERROR_ID result = SetDeviceCommunications(port);

            //set configuration for on demand
            //TODO - make this configuration driven
            SetDeviceToOnDemand();
            SoftReset();

            if (result.ToString().Contains("SUCCESS") || result.ToString().Contains("RESULT_ERROR_ALREADY_CONNECTED"))
            {
                Connected = true;
                
                //make sure Encryption is on
                //if (!IsEncryptionOn(CheckKeys()))
                //{
                //    Connected = false;
                //}
            }

            return result.ToString();
        }

        private ERROR_ID SetDeviceCommunications(string port)
        {
            SETTINGS_COMMUNICATION commSet = new SETTINGS_COMMUNICATION();

            SETTINGS_COMM_TIMEOUTS commTimeouts;

            uint connectTimeout = 5000;

            commTimeouts.ConnectTimeout = connectTimeout;
            commTimeouts.ReceiveTimeout = connectTimeout;
            commTimeouts.SendTimeout = connectTimeout;

            RBA_API.SetCommTimeouts(commTimeouts);

            commSet.interface_id = (uint)COMM_INTERFACE.SERIAL_INTERFACE;
            commSet.rs232_config.ComPort = port;
            commSet.rs232_config.BaudRate = Convert.ToUInt32(ComBaudRate);
            commSet.rs232_config.DataBits = Convert.ToUInt32(ComDataBits);
            commSet.rs232_config.Parity = (uint)0;
            commSet.rs232_config.StopBits = Convert.ToUInt32(1);
            commSet.rs232_config.FlowControl = (uint)0;

            //Connect to pin pad
            ERROR_ID result = RBA_API.Connect(commSet);
            return result;
        }

        public ERROR_ID SoftReset()
        {
            RBA_API.SetParam(PARAMETER_ID.P15_REQ_RESET_TYPE, "9");
            ERROR_ID result = RBA_API.ProcessMessage(MESSAGE_ID.M15_SOFT_RESET);

            return result;
        }

        private void SetDeviceToOnDemand()
        {
            //check to see if the device is already in OnDemand
            RBA_API.SetParam(PARAMETER_ID.P61_REQ_GROUP_NUM, "7");
            RBA_API.SetParam(PARAMETER_ID.P61_REQ_INDEX_NUM, "15");
            RBA_API.ProcessMessage(MESSAGE_ID.M61_CONFIGURATION_READ);

            string dataValue = RBA_API.GetParam(PARAMETER_ID.P61_RES_DATA_CONFIG_PARAMETER);

            //Not Set - so set it
            if (!OnDemandSet || dataValue != "1")
            {
                RBA_API.SetParam(PARAMETER_ID.P60_REQ_GROUP_NUM, "7");
                RBA_API.SetParam(PARAMETER_ID.P60_REQ_INDEX_NUM, "15");
                RBA_API.SetParam(PARAMETER_ID.P60_REQ_DATA_CONFIG_PARAM, "1");
                RBA_API.ProcessMessage(MESSAGE_ID.M60_CONFIGURATION_WRITE);

                OnDemandSet = true;
            }
        }

        public ERROR_ID WriteConfiguration(string group, string index, string data)
        {
            RBA_API.SetParam(PARAMETER_ID.P60_REQ_GROUP_NUM, group);
            RBA_API.SetParam(PARAMETER_ID.P60_REQ_INDEX_NUM, index);
            RBA_API.SetParam(PARAMETER_ID.P60_REQ_DATA_CONFIG_PARAM, data);
            ERROR_ID result = RBA_API.ProcessMessage(MESSAGE_ID.M60_CONFIGURATION_WRITE);

            return result;
        }

        public string Get24RebootTime()
        {
            RBA_API.SetParam(PARAMETER_ID.P61_REQ_GROUP_NUM, "0007");
            RBA_API.SetParam(PARAMETER_ID.P61_REQ_INDEX_NUM, "0046");
            RBA_API.ProcessMessage(MESSAGE_ID.M61_CONFIGURATION_READ);

            string status = RBA_API.GetParam(PARAMETER_ID.P61_RES_DATA_CONFIG_PARAMETER);

            return status;
        }

        public void Offline()
        {
            RBA_API.ProcessMessage(MESSAGE_ID.M00_OFFLINE);
        }

        public ERROR_ID HardDeviceReset()
        {
            ERROR_ID result = RBA_API.ProcessMessage(MESSAGE_ID.M97_REBOOT);
            return result;
        }
    }

    public enum IngenicoLoggingLevel
    {
        NONE = -1,
        ERROR = 0,
        WARNING = 1,
        INFO = 2,
        TRACE = 3,
        DEBUG = 4
    };
}
