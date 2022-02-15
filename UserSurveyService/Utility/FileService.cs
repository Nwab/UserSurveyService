using NLog;
using System;
using System.IO;
using System.Xml.Serialization;
using UBN.Security;
using UserSurveyService.Utility;

namespace UserQuestionnaireService.Utility
{
    public class FileService : IDisposable
    {
        private string IPAddress;
        private NetworkShareAccesser connection;

        private bool _UseNetworkPath = true;

        private static Logger log = LogManager.GetCurrentClassLogger();


        public FileService(string ipaddress, bool useNtwrkPath)
        {
            this.IPAddress = ipaddress;
            this.IsConnected = false;
            this._UseNetworkPath = useNtwrkPath;
        }

        public bool IsConnected
        {
            get;
            private set;
        }

        public bool Connect(string username, string password)
        {
            try
            {
                if (this._UseNetworkPath)
                {
                    this.connection = NetworkShareAccesser.Access(IPAddress, username, password);
                    this.IsConnected = true;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                this.IsConnected = false;
            }
            return this.IsConnected;
        }


        public SurveySetting ReadFiles(string NetworkPath)
        {
            SurveySetting surveySetConfig = null;
            string networkFilePath = "";

            if (this.IsConnected && this._UseNetworkPath)
            {
                var fileList = Directory.GetDirectories(NetworkPath);

                foreach (var item in fileList)
                {
                    if (item.Contains("UserSurvey"))
                    {
                        networkFilePath = item; 
                    
                    }
                }
                networkFilePath = $@"{networkFilePath}\\UserSurveyConfig.xml";

                try
                {
                    surveySetConfig = DeserializeObject(networkFilePath);
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                }
            }
            else if (this._UseNetworkPath == false)
            {
                try
                {
                    string useLocalPath = $@"{ConfigHelper.BATCHFILE_PATH}\\UserSurveyConfig.xml";

                    surveySetConfig = DeserializeObject(useLocalPath);
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                }
            }

            return surveySetConfig;
        }

        private SurveySetting DeserializeObject(string filename)
        {
            Console.WriteLine("Reading with Stream");
            // Create an instance of the XmlSerializer.
            XmlSerializer serializer =
            new XmlSerializer(typeof(SurveySetting));

            // Declare an object variable of the type to be deserialized.
            SurveySetting surveySetting;

            using (Stream reader = new FileStream(filename, FileMode.Open))
            {
                // Call the Deserialize method to restore the object's state.
                surveySetting = (SurveySetting)serializer.Deserialize(reader);
            }

            return surveySetting;
        }

        public void Dispose()
        {
            if (connection != null)
            {
                connection.Dispose();
            }
        }
    }
}
