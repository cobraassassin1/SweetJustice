using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvoManager : MonoBehaviour {

    public TestConvoText text;

    internal TheBartender bartender;
    internal Character currentcharacter = new Character();


    private void Awake()
    {
        bartender = GameObject.FindGameObjectWithTag("GameController").GetComponent<TheBartender>();
    }

    void Start () {
        CharacterCollection characters = new CharacterCollection();
        int personID = bartender.convoPersonID;
        characters = characters.Load();
        currentcharacter = characters.Characters[personID];
        text.ChangeText(currentcharacter.Name);
	}
	
	public void BackToLocation()
    {
        int locationID = bartender.locationID;
        LevelManager level = GetComponent<LevelManager>();
        if(locationID == 0)
        {
            level.LoadLevel("2DBar");
        }
        else
        {
            Debug.LogError("No location correctly selected");
        }
    }
}
