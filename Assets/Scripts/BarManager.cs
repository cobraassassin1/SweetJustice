using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarManager : MonoBehaviour {

    internal TheBartender bartender;

    internal bool autoBartender = true; //At the beginning of every bar encounter, you will meet the bartender.
    internal bool firstTime = false; //If true this is the start of the game

    private void Awake()
    {
        bartender = GameObject.FindGameObjectWithTag("GameController").GetComponent<TheBartender>();
        firstTime = bartender.firstTime;
    }

    void Start () {
        autoBartender = bartender.AutoBartender;
        CheckAutoBartender();
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
            bartender.levelID += 1;
        }
        level.LoadLevel("2BMap");
        
    }
}
