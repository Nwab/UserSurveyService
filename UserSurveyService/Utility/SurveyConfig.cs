using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace UserQuestionnaireService.Utility
{
    class SurveyConfig
    {
    }

    [XmlRoot(ElementName = "DBCredential")]
    public class DBCredential
    {
        [XmlElement(ElementName = "ServerIP")]
        public string ServerIP { get; set; }
        [XmlElement(ElementName = "Database")]
        public string Database { get; set; }
        [XmlElement(ElementName = "User")]
        public string User { get; set; }
        [XmlElement(ElementName = "Password")]
        public string Password { get; set; }
        [XmlElement(ElementName = "Port")]
        public string Port { get; set; }
    }

    [XmlRoot(ElementName = "SurveySetting")]
    public class SurveySetting
    {
        [XmlElement(ElementName = "UserStatusQuery")]
        public string UserStatusQuery { get; set; }
        [XmlElement(ElementName = "DBCredential")]
        public DBCredential DBCredential { get; set; }
        [XmlElement(ElementName = "DurationInMins")]
        public int DurationInMins { get; set; }
    }

}
