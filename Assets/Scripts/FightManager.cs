using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour {

    TheBartender bartender;

    private void Awake()
    {
        bartender = GameObject.FindGameObjectWithTag("GameController").GetComponent<TheBartender>();
    }

    public void WinFight()
    {
        LevelManager level = GetComponent<LevelManager>();
        if (bartender.levelID == 15)
        {
            level.LoadLevel("3AScoreboard");
        }
        else
        {
            bartender.levelID += 1;
            level.LoadLevel("2BMap");
        }
        
    }
}
