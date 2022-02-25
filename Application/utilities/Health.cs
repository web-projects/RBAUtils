using RBA_SDK;

namespace RBAUtils.utilities
{
    public class Health
    {
        public ERROR_ID RESULT { get; set; }
        public string MSR_SWIPES { get; set; }
        public string BAD_TRACK1_READS { get; set; }
        public string BAD_TRACK2_READS { get; set; }
        public string BAD_TRACK3_READS { get; set; }
        public string SIGNATURES { get; set; }
        public string REBOOT { get; set; }
        public string DEVICE_NAME { get; set; }
        public string SERIAL_NUMBER { get; set; }
        public string OS_VERSION { get; set; }
        public string APP_VERSION { get; set; }
        public string SECURITY_LIB_VERSION { get; set; }
        public string EFTL_VERSION { get; set; }
        public string EFTP_VERSION { get; set; }
        public string RAM_SIZE { get; set; }
        public string FLASH_SIZE { get; set; }
        public string MANUFACTURE_DATE { get; set; }
        public string CPEM_TYPE { get; set; }
        public string PEN_STATUS { get; set; }
        public string APP_NAME { get; set; }
        public string MANUFACTURE_ID { get; set; }
        public string DIGITIZER_VERSION { get; set; }
        public string MANUFACTURING_SERIAL_NUMBER { get; set; }

        public Health()
        {
            MSR_SWIPES = string.Empty;
            BAD_TRACK1_READS = string.Empty;
            BAD_TRACK2_READS = string.Empty;
            BAD_TRACK3_READS = string.Empty;
            SIGNATURES = string.Empty;
            REBOOT = string.Empty;
            DEVICE_NAME = string.Empty;
            SERIAL_NUMBER = string.Empty;
            OS_VERSION = string.Empty;
            APP_VERSION = string.Empty;
            SECURITY_LIB_VERSION = string.Empty;
            EFTL_VERSION = string.Empty;
            EFTP_VERSION = string.Empty;
            RAM_SIZE = string.Empty;
            FLASH_SIZE = string.Empty;
            MANUFACTURE_DATE = string.Empty;
            CPEM_TYPE = string.Empty;
            PEN_STATUS = string.Empty;
            APP_NAME = string.Empty;
            MANUFACTURE_ID = string.Empty;
            DIGITIZER_VERSION = string.Empty;
            MANUFACTURING_SERIAL_NUMBER = string.Empty;
        }

        public void GetDeviceHealth()
        {
            RESULT = RBA_API.SetParam(PARAMETER_ID.P08_REQ_REQUEST_TYPE, "0");
            RESULT = RBA_API.ProcessMessage(MESSAGE_ID.M08_HEALTH_STAT);
            MSR_SWIPES = RBA_API.GetParam(PARAMETER_ID.P08_RES_COUNT_MSR_SWIPES);
            BAD_TRACK1_READS = RBA_API.GetParam(PARAMETER_ID.P08_RES_COUNT_BAD_TRACK1_READS);
            BAD_TRACK2_READS = RBA_API.GetParam(PARAMETER_ID.P08_RES_COUNT_BAD_TRACK2_READS);
            BAD_TRACK3_READS = RBA_API.GetParam(PARAMETER_ID.P08_RES_COUNT_BAD_TRACK3_READS);
            SIGNATURES = RBA_API.GetParam(PARAMETER_ID.P08_RES_COUNT_SIGNATURES);
            REBOOT = RBA_API.GetParam(PARAMETER_ID.P08_RES_COUNT_REBOOT);
            DEVICE_NAME = RBA_API.GetParam(PARAMETER_ID.P08_RES_DEVICE_NAME);
            SERIAL_NUMBER = RBA_API.GetParam(PARAMETER_ID.P08_RES_SERIAL_NUMBER);
            OS_VERSION = RBA_API.GetParam(PARAMETER_ID.P08_RES_OS_VERSION);
            APP_VERSION = RBA_API.GetParam(PARAMETER_ID.P08_RES_APP_VERSION);
            SECURITY_LIB_VERSION = RBA_API.GetParam(PARAMETER_ID.P08_RES_SECURITY_LIB_VERSION);
            EFTL_VERSION = RBA_API.GetParam(PARAMETER_ID.P08_RES_EFTL_VERSION);
            EFTP_VERSION = RBA_API.GetParam(PARAMETER_ID.P08_RES_EFTP_VERSION);
            RAM_SIZE = RBA_API.GetParam(PARAMETER_ID.P08_RES_RAM_SIZE);
            FLASH_SIZE = RBA_API.GetParam(PARAMETER_ID.P08_RES_FLASH_SIZE);
            MANUFACTURE_DATE = RBA_API.GetParam(PARAMETER_ID.P08_RES_MANUFACTURE_DATE);
            CPEM_TYPE = RBA_API.GetParam(PARAMETER_ID.P08_RES_CPEM_TYPE);
            PEN_STATUS = RBA_API.GetParam(PARAMETER_ID.P08_RES_PEN_STATUS);
            APP_NAME = RBA_API.GetParam(PARAMETER_ID.P08_RES_APP_NAME);
            MANUFACTURE_ID = RBA_API.GetParam(PARAMETER_ID.P08_RES_MANUFACTURE_ID);
            DIGITIZER_VERSION = RBA_API.GetParam(PARAMETER_ID.P08_RES_DIGITIZER_VERSION);
            MANUFACTURING_SERIAL_NUMBER = RBA_API.GetParam(PARAMETER_ID.P08_RES_MANUFACTURING_SERIAL_NUMBER);
        }
    }

}
