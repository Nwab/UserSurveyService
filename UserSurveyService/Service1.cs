using System;
using System.ServiceProcess;
using UserSurveyService.Utility;

namespace UserSurveyService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            var periodTimeSpan = TimeSpan.FromMinutes(ConfigHelper.SCHEDULE_MINS());
            Console.WriteLine("UserName: {0}", Environment.UserName);
            System.Diagnostics.Process.Start(ConfigHelper.WEB_URL());
        }

        protected override void OnStop()
        {
        }
    }
}
