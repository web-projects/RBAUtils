using RBA_SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBAUtils.utilities
{
    public class Info
    {
        public ERROR_ID RESULT { get; set; }
        public string MANUFACTURE { get; set; }
        public string DEVICE { get; set; }
        public string UNIT_SERIAL_NUMBER { get; set; }
        public string RAM_SIZE { get; set; }
        public string FLASH_SIZE { get; set; }
        public string DIGITIZER_VERSION { get; set; }
        public string SECURITY_MODULE_VERSION { get; set; }
        public string OS_VERSION { get; set; }
        public string APPLICATION_VERSION { get; set; }
        public string EFTL_VERSION { get; set; }
        public string EFTP_VERSION { get; set; }
        public string MANUFACTURING_SERIAL_NUMBER { get; set; }
        public string EMV_DC_KERNEL_TYPE { get; set; }
        public string EMV_ENGINE_KERNEL_TYPE { get; set; }
        public string CLESS_DISCOVER_KERNEL_TYPE { get; set; }
        public string CLESS_EXPRESSPAY_V3_KERNEL_TYPE { get; set; }
        public string CLESS_EXPRESSPAY_V2_KERNEL_TYPE { get; set; }
        public string CLESS_PAYPASS_V3_KERNEL_TYPE { get; set; }
        public string CLESS_PAYPASS_V3_APP_TYPE { get; set; }
        public string CLESS_VISA_PAYWAVE_KERNEL_TYPE { get; set; }
        public string CLESS_INTERAC_KERNEL_TYPE { get; set; }

        public Info()
        {
            MANUFACTURE = string.Empty;
            DEVICE = string.Empty;
            UNIT_SERIAL_NUMBER = string.Empty;
            RAM_SIZE = string.Empty;
            FLASH_SIZE = string.Empty;
            DIGITIZER_VERSION = string.Empty;
            SECURITY_MODULE_VERSION = string.Empty;
            OS_VERSION = string.Empty;
            APPLICATION_VERSION = string.Empty;
            EFTL_VERSION = string.Empty;
            EFTP_VERSION = string.Empty;
            MANUFACTURING_SERIAL_NUMBER = string.Empty;
            EMV_DC_KERNEL_TYPE = string.Empty;
            EMV_ENGINE_KERNEL_TYPE = string.Empty;
            CLESS_DISCOVER_KERNEL_TYPE = string.Empty;
            CLESS_EXPRESSPAY_V2_KERNEL_TYPE = string.Empty;
            CLESS_EXPRESSPAY_V3_KERNEL_TYPE = string.Empty;
            CLESS_PAYPASS_V3_KERNEL_TYPE = string.Empty;
            CLESS_PAYPASS_V3_APP_TYPE = string.Empty;
            CLESS_VISA_PAYWAVE_KERNEL_TYPE = string.Empty;
            CLESS_INTERAC_KERNEL_TYPE = string.Empty;
        }

        public void GetDeviceInfo()
        {
            // Enable Extended Info
            SetDeviceExtendedInfo(true);

            ERROR_ID result = RBA_API.ProcessMessage(MESSAGE_ID.M07_UNIT_DATA);
            MANUFACTURE = RBA_API.GetParam(PARAMETER_ID.P07_RES_MANUFACTURE);
            DEVICE = RBA_API.GetParam(PARAMETER_ID.P07_RES_DEVICE);//iMP350
            UNIT_SERIAL_NUMBER = RBA_API.GetParam(PARAMETER_ID.P07_RES_UNIT_SERIAL_NUMBER);
            RAM_SIZE = RBA_API.GetParam(PARAMETER_ID.P07_RES_RAM_SIZE);
            FLASH_SIZE = RBA_API.GetParam(PARAMETER_ID.P07_RES_FLASH_SIZE);
            DIGITIZER_VERSION = RBA_API.GetParam(PARAMETER_ID.P07_RES_DIGITIZER_VERSION);
            SECURITY_MODULE_VERSION = RBA_API.GetParam(PARAMETER_ID.P07_RES_SECURITY_MODULE_VERSION);
            OS_VERSION = RBA_API.GetParam(PARAMETER_ID.P07_RES_OS_VERSION);
            APPLICATION_VERSION = RBA_API.GetParam(PARAMETER_ID.P07_RES_APPLICATION_VERSION);
            EFTL_VERSION = RBA_API.GetParam(PARAMETER_ID.P07_RES_EFTL_VERSION);
            EFTP_VERSION = RBA_API.GetParam(PARAMETER_ID.P07_RES_EFTP_VERSION);
            MANUFACTURING_SERIAL_NUMBER = RBA_API.GetParam(PARAMETER_ID.P07_RES_MANUFACTURING_SERIAL_NUMBER);
            EMV_DC_KERNEL_TYPE = RBA_API.GetParam(PARAMETER_ID.P07_RES_EMV_DC_KERNEL_TYPE);
            EMV_ENGINE_KERNEL_TYPE = RBA_API.GetParam(PARAMETER_ID.P07_RES_EMV_ENGINE_KERNEL_TYPE);
            CLESS_DISCOVER_KERNEL_TYPE = RBA_API.GetParam(PARAMETER_ID.P07_RES_CLESS_DISCOVER_KERNEL_TYPE);
            CLESS_EXPRESSPAY_V3_KERNEL_TYPE = RBA_API.GetParam(PARAMETER_ID.P07_RES_CLESS_EXPRESSPAY_V3_KERNEL_TYPE);
            CLESS_EXPRESSPAY_V2_KERNEL_TYPE = RBA_API.GetParam(PARAMETER_ID.P07_RES_CLESS_EXPRESSPAY_V2_KERNEL_TYPE);
            CLESS_PAYPASS_V3_KERNEL_TYPE = RBA_API.GetParam(PARAMETER_ID.P07_RES_CLESS_PAYPASS_V3_KERNEL_TYPE);
            CLESS_PAYPASS_V3_APP_TYPE = RBA_API.GetParam(PARAMETER_ID.P07_RES_CLESS_PAYPASS_V3_APP_TYPE);
            CLESS_VISA_PAYWAVE_KERNEL_TYPE = RBA_API.GetParam(PARAMETER_ID.P07_RES_CLESS_VISA_PAYWAVE_KERNEL_TYPE);
            CLESS_INTERAC_KERNEL_TYPE = RBA_API.GetParam(PARAMETER_ID.P07_RES_CLESS_INTERAC_KERNEL_TYPE);

            // Check for Proper DEVICE name (iPP320, iPP350, iSC250, iSC480, etc.)
            RESULT = RBA_API.SetParam(PARAMETER_ID.P08_REQ_REQUEST_TYPE, "0");
            RESULT = RBA_API.ProcessMessage(MESSAGE_ID.M08_HEALTH_STAT);
            string deviceName = RBA_API.GetParam(PARAMETER_ID.P08_RES_DEVICE_NAME) ?? string.Empty;
            if (deviceName.Length > 0 && (DEVICE == null || DEVICE.IndexOf(deviceName, StringComparison.CurrentCultureIgnoreCase) == -1))
            {
                DEVICE = deviceName;
            }
        }

        public int SetDeviceExtendedInfo(bool enabled)
        {
            RBA_API.SetParam(PARAMETER_ID.P60_REQ_GROUP_NUM, "0013");
            RBA_API.SetParam(PARAMETER_ID.P60_REQ_INDEX_NUM, "0023");
            RBA_API.SetParam(PARAMETER_ID.P60_REQ_DATA_CONFIG_PARAM, enabled ? "1" : "0");
            RBA_API.ProcessMessage(MESSAGE_ID.M60_CONFIGURATION_WRITE);
            int.TryParse(RBA_API.GetParam(PARAMETER_ID.P60_RES_STATUS), out int resultOut);
            return resultOut;
        }
    }
}
