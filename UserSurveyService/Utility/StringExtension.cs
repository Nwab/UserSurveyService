using System.Configuration;


namespace UserSurveyService.Utility
{
    public static class StringExtension
    {
        public static string GetKeyValue(this string Key)
        {
            return ConfigurationManager.AppSettings[Key];
        }


    }
}
