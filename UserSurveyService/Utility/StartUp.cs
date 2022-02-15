using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserSurveyService.Utility;

namespace UserQuestionnaireService.Utility
{
    public class StartUp
    {

        private SurveySetting surveySetting;
       

        public StartUp()
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            FileService serv = new FileService("S_IP".GetKeyValue(), ConfigHelper.UseNtwrkPath());
            bool isCnnected = serv.Connect("S_User".GetKeyValue(), "S_Pwrd".GetKeyValue());
            surveySetting = serv.ReadFiles("S_NTWRKPATH".GetKeyValue());
        }

        public SurveySetting GetSurveySetting
        {
            get
            {
                return surveySetting;
            }
        }

        public string GetConnectionStr()
        {
            string connectStr = $"server={surveySetting.DBCredential.ServerIP};uid={surveySetting.DBCredential.User};pwd={surveySetting.DBCredential.Password};database={surveySetting.DBCredential.Database}";

            return connectStr;
        }
    }
}
