using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarManager : MonoBehaviour {

    public GameObject currentBackground;
    public GameObject currentBar;
    public GameObject currentBartender;
    
    [Header("Management Variables")]
    public Transform backgroundContainer;
    public Transform characterContainer;

    [System.Serializable]
    private class Backgrounds
    {
        public GameObject background1;
        public GameObject bar1;
    }

    [System.Serializable]
    private class Characters
    {
        public GameObject bartender;
    }

    [SerializeField]
    private Backgrounds background = new Backgrounds();
    [SerializeField]
    private Characters character = new Characters();


    internal TheBartender bartender;

    internal bool autoBartender = true; //At the beginning of every bar encounter, you will meet the bartender.
    internal bool firstTime = false; //If true this is the start of the game
    internal int ID;

    private void Awake()
    {
        bartender = GameObject.FindGameObjectWithTag("GameController").GetComponent<TheBartender>();
        firstTime = bartender.firstTime;
        ID = bartender.ID;
    }

    void Start () {
        autoBartender = bartender.AutoBartender;
        CheckAutoBartender();
        LoadBar();
	}

    internal void LoadBar()
    {
        if(ID == 1000)
        {
            currentBackground = Instantiate(background.background1, background.background1.transform.position, Quaternion.identity) as GameObject;
            currentBar = Instantiate(background.bar1, background.bar1.transform.position, Quaternion.identity) as GameObject;
            currentBartender = Instantiate(character.bartender, character.bartender.transform.position, Quaternion.identity) as GameObject;
        }
        else
        {
            Debug.LogError("ID does not exist");
        }
        currentBackground.transform.parent = backgroundContainer;
        currentBar.transform.parent = backgroundContainer;
        currentBartender.transform.parent = characterContainer;
        //Load the background
        //Load the people

    }

    internal void CheckAutoBartender()
    {
        LevelManager level = GetComponent<LevelManager>();
        if(autoBartender == true)
        {
            bartender.AutoBartender = false;
            level.LoadLevel("2CConvo");
        }
    }

    public void LeaveBar()
    {
        LevelManager level = GetComponent<LevelManager>();
        bartender.AutoBartender = true;
        if (firstTime)
        {
            bartender.firstTime = false;
        }
        else
        {
            bartender.NextLevel();
        }
        level.LoadLevel("2BMap");
        
    }
}
