using RBA_SDK;
using System;

namespace RBAUtils.utilities
{
    public class Ingenico
    {
        private Health DeviceHealth { get; set; }
        private bool Connected;

        public int ComBaudRate = 115200;
        public int ComDataBits = 8;

        public bool OnDemandSet { get; set; }

        public string Connect(string port, LogHandler traceLog, Enums.IngenicoLoggingLevel logLevel)
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

                GetDeviceHealth();
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

        private void GetDeviceHealth()
        {
            DeviceHealth = new Health();
            DeviceHealth.GetDeviceHealth();
        }

        #region    --- RBA API ---
        public string GetVariable_29(string varId)
        {
            RBA_API.SetParam(PARAMETER_ID.P29_REQ_VARIABLE_ID, varId);
            RBA_API.ProcessMessage(MESSAGE_ID.M29_GET_VARIABLE);

            string status = RBA_API.GetParam(PARAMETER_ID.P29_RES_STATUS);
            string variable = RBA_API.GetParam(PARAMETER_ID.P29_RES_VARIABLE_ID);
            string value = RBA_API.GetParam(PARAMETER_ID.P29_RES_VARIABLE_DATA);
            return value;
        }
        public ERROR_ID SetVarible28(string varId, string varData)
        {
            RBA_API.SetParam(PARAMETER_ID.P28_REQ_RESPONSE_TYPE, "1");
            RBA_API.SetParam(PARAMETER_ID.P28_REQ_VARIABLE_ID, varId);
            RBA_API.SetParam(PARAMETER_ID.P28_REQ_VARIABLE_DATA, varData);
            ERROR_ID result = RBA_API.ProcessMessage(MESSAGE_ID.M28_SET_VARIABLE);
            return result;

        }
        #endregion --- RBA API ---

        private void SetDeviceDate()
        {
            string currentDate = DateTime.UtcNow.ToString("MMddyy");
            string deviceDate = GetVariable_29("202");

            if (currentDate != deviceDate)
            {
                SetVarible28("202", currentDate);
            }
        }

        private void SetDeviceTime()
        {
            string currentTime = DateTime.UtcNow.ToString("HHmmss");
            string deviceTime = GetVariable_29("201");

            if (currentTime.Substring(0, 4) != deviceTime.Substring(0, 4))
            {
                SetVarible28("201", currentTime);
            }
        }

        public string GetTerminalModel()
        {
            return DeviceHealth.DEVICE_NAME;
        }

        public string GetTerminalSerialNumber()
        {
              return DeviceHealth.MANUFACTURING_SERIAL_NUMBER;
        }

        public string GetTerminalTimeStamp()
        {
            // MMddyy
            string deviceDate = GetVariable_29("202");
            // HHmmss
            string deviceTime = GetVariable_29("201");

            return $"{deviceDate}-{deviceTime}";
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
}
