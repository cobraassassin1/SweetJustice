using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;

[XmlRoot("CharacterCollection")]
public class CharacterCollection{
    [XmlArray("Players"), XmlArrayItem("Character")]
    public List<Character> Characters = new List<Character>();

    public CharacterCollection Load()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(CharacterCollection));
        StreamReader reader = new StreamReader(Application.dataPath + "/XML/Players.xml");
        CharacterCollection container = serializer.Deserialize(reader.BaseStream) as CharacterCollection;
        reader.Close();
        return container;
    }

    internal Character GetCharacterbyId(int id)
    {
        Load();
        if (id == int.Parse(Characters[id].ID))
        {
            return Characters[id];
        }
        else
        {
            Debug.LogError("Character Not Found");
            return null;
        }
        
    }
}
