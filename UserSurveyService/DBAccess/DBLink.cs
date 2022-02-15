using NLog;
using UserSurveyService.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using UserQuestionnaireService.Utility;

namespace UserSurveyService.DBAccess
{
    public class DBLink
    {
        private static NLog.Logger log = LogManager.GetCurrentClassLogger();

      
        public static string LoggedInUserStatus(StartUp startUp, string lggedInUser)
        {

           
            string questionStatus = "";
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection())
                { 
                    mySqlConnection.ConnectionString = startUp.GetConnectionStr();
                    log.Info($"{lggedInUser} : DB Connected {startUp.GetConnectionStr()}");
                    MySqlCommand mySqlCommand = new MySqlCommand(startUp.GetSurveySetting.UserStatusQuery, mySqlConnection);
                    mySqlCommand.CommandType = CommandType.Text;
                    mySqlCommand.Parameters.AddWithValue("@username", lggedInUser);
                    mySqlCommand.Connection.Open();
                    log.Info($"DB Connected for :: {startUp.GetSurveySetting.UserStatusQuery}");
                    MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

                    if (mySqlDataReader.HasRows)
                    {
                        log.Info($"Rows found for user");
                        while (mySqlDataReader.Read())
                        {
                            questionStatus = mySqlDataReader[0].ToString();
                            log.Info($"User: {lggedInUser} Status: {questionStatus}");
                        }
                    }
                    else
                    {
                        log.Info($"No rows found for user");
                    }
                    mySqlConnection.Close();
                    log.Info($"Close connection");
                }
            }
            catch (Exception ex)
            {
                DBLink.log.Error(string.Format("{0} +------------------------+ {1}", ex.StackTrace, ex.Message));
                return questionStatus;
            }
            return questionStatus;
        }
    }
}
