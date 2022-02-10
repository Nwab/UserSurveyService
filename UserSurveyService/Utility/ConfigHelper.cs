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
        public static string WEB_URL()
        {
            return "WEB_URL".GetKeyValue();
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

        public static string Database()
        {
            return "SQL_DATABASE".GetKeyValue();
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
                ConfigHelper.Database(),
                ConfigHelper.Username(),
                ConfigHelper.Password()
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetLoggedInUserDetails()
        {
            string cipherText = string.Empty;
            try
            {
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "\\TxtQuery\\UserDetailsQry.txt";
                cipherText = File.ReadAllText(filePath);
            }
            catch (Exception value)
            {
                ConfigHelper.log.Error<Exception>(value);
            }

            return cipherText;
        }

        private static Logger log = LogManager.GetCurrentClassLogger();
    }
}
