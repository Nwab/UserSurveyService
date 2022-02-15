using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserSurveyService.Utility
{
    public class ConfigHelper
    {
        


        public static string BATCHFILE_PATH
        {
            get
            {
                return "BATCHFILE_PATH".GetKeyValue();
            }
        }

        public static int SCHEDULE_MINS()
        {
            int scheduleMin =  Convert.ToInt32("SCHEDULE_MINS".GetKeyValue());

            return scheduleMin;
        }

        public static string DataSource()
        {
            return "SQL_DATA_SOURCE".GetKeyValue();
        }

        public static bool UseNtwrkPath()
        {
            string ntwr_satus =  "USENTWRK".GetKeyValue();

            if (ntwr_satus == "True")
                return true;
            else
                return false;
        }

        public static string Username()
        {
            return "SQL_USRNAME".GetKeyValue();
        }

        public static string Password()
        {
            string cipherText = "SQL_PASSWORD".GetKeyValue();
            return cipherText;
        }

        public static string GetFEPConnectionStr()
        {
            return string.Format("Data Source={0};Persist Security Info=True;Initial Catalog={1};User id={2};Password={3};", new object[]
            {
                ConfigHelper.DataSource(),
                ConfigHelper.Username(),
                ConfigHelper.Password()
            });
        }

       
        private static Logger log = LogManager.GetCurrentClassLogger();
    }
}
