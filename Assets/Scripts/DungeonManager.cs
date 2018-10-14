using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour {

    private TheBartender bartender;

    private void Awake()
    {
        bartender = GameObject.FindGameObjectWithTag("GameController").GetComponent<TheBartender>();
    }

    public void FinishDungeon()
    {
        bartender.NextLevel();
        LevelManager level = GetComponent<LevelManager>();
        level.LoadLevel("2BMap");
    }
}
