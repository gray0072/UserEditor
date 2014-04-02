namespace UserEditor.Models.XML
{
    using System.Xml.Serialization;

    [XmlRoot("user")]
    public class User : IUser
    {
        [XmlAttribute("id")]
        public int Id { get; set; }

        [XmlElement("firstname")]
        public string Firstname { get; set; }

        [XmlElement("lastname")]
        public string Lastname { get; set; }

        [XmlElement("status")]
        public string Status { get; set; }

        [XmlArray("pages")]
        [XmlArrayItem("page")]
        public string[] Pages { get; set; }

        [XmlElement("admin")]
        public bool IsAdmin { get; set; }
    }
}
