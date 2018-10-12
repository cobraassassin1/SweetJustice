using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheBartender : MonoBehaviour {

    
    public int fleetID = 0; //Default is Apple
    public int levelID = 1; //Default is LevelOne
    public MapType currentType = MapType.Bar; //Default is Bar

    [Header("Conversation Variables")]
    public int convoPersonID = 0; //Default is Bartender
    public int locationID = 0; //Default is Bar
    public bool firstTime = true; //Default is Start of Game

    [Header("Bar Variables")]
    public bool autoBartender = true; //Loads bartender first

    //Getter and Setter
    public bool AutoBartender { get { return this.autoBartender; } set { this.autoBartender = value; } }

    private static TheBartender instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // if instance is already set and this is not the same object, destroy it
            if (this != instance) { Destroy(gameObject); }
        }
    }

    private void generateFleet()
    {

    } 
}
