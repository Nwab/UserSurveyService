using NLog;
using System;
using System.Linq;
using System.Management;
using System.ServiceProcess;
using UserQuestionnaireService.Utility;
using UserSurveyService.DBAccess;
using UserSurveyService.Utility;

namespace UserSurveyService
{
    public partial class Service1 : ServiceBase
    {
        private static NLog.Logger log = LogManager.GetCurrentClassLogger();

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            StartUp startUp = new StartUp();

            log.Info($"Service Started..{DateTime.Now}");
            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromMinutes(startUp.GetSurveySetting.DurationInMins);

            string lggedInUser = Environment.UserName;

            string user_status = DBLink.LoggedInUserStatus(startUp, lggedInUser);
            Console.WriteLine("UserName: {0}", Environment.UserName);

            log.Info("UserName: {0}", Environment.UserName);

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT UserName FROM Win32_ComputerSystem");
            ManagementObjectCollection mgmtCollection = searcher.Get();
            string username = (string)mgmtCollection.Cast<ManagementBaseObject>().First()["UserName"];

            string dataname = System.Environment.GetEnvironmentVariable("UserName");

            log.Info("UserName>........>: {0}", username);
            log.Info("UserName>........>: {0}", dataname);
            if (user_status.ToUpper().Equals("PENDING"))
            {
                log.Info($"{user_status.ToUpper()}>>{ConfigHelper.BATCHFILE_PATH}..{DateTime.Now}");
                var timer = new System.Threading.Timer((e) =>
                {
                    System.Diagnostics.Process.Start($@"{ConfigHelper.BATCHFILE_PATH}\bat_survey.bat");

                }, null, startTimeSpan, periodTimeSpan);
               
            }
            log.Info("UserName>>: {0}", Environment.UserName);
            System.Diagnostics.Process.Start(@"C:\Users\nwaob\source\repos\app_data\bat_survey.bat");
        }

        protected override void OnStop()
        {
            log.Info($"Service Stopped..{DateTime.Now}");
        }
    }
}
