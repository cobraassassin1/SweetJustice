using System.Xml;
using System.Xml.Serialization;

public class Character {
    [XmlAttribute("id")]
    public string ID;

    public string Name;
}
