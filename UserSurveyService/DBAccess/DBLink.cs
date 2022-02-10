using NLog;
using UserSurveyService.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserSurveyService.DBAccess
{
    public class DBLink
    {
        private static NLog.Logger log = LogManager.GetCurrentClassLogger();

        public static int LoggedInUserDetails(string username)
        {
            string text = ConfigHelper.GetLoggedInUserDetails();
            int result = 0;
            DBLink.log.Trace(string.Format("ValidateNairaPANExists : {0}", text));
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection())
                {
                    sqlConnection.ConnectionString = ConfigHelper.GetFEPConnectionStr();
                    SqlCommand sqlCommand = new SqlCommand(text, sqlConnection);
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.Parameters.AddWithValue("@username", username);
                    sqlCommand.Connection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            result = 1;
                        }
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {

                DBLink.log.Error(string.Format("{0} +------------------------+ {1}", ex.StackTrace, ex.Message));
                return -1;
            }
            return result;
        }
    }
}
