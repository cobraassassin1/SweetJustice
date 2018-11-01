using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheBartender : MonoBehaviour {

    
    public int fleetID = 0; //Default is Apple
    public int currentLevel = 1; //Default is LevelOne
    public int ID = 0000;
    public MapType currentType = MapType.Bar; //Default is Bar

    [Header("Map Variables")]
    public List<Level> levels = new List<Level>();
    public int nextMiniBoss;

    [Header("Conversation Variables")]
    public int convoPersonID = 0; //Default is Bartender
    public int locationID = 0; //Default is Bar
    public int startDialogueID = 0; //See dialogue.json
    public bool firstTime = true; //Default is Start of Game

    [Header("Bar Variables")]
    public bool autoBartender = true; //Loads bartender first

    [Header("Test Variables")]
    public static bool generateFleet = true;

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

        TestGenerateFleet();
    }

    private void TestGenerateFleet()
    {
        if (generateFleet)
        {
            GenerateFleet();
            generateFleet = false;
        }
    }

    internal Level getLevel(int par1)
    {
        return levels[par1 - currentLevel];
    }

    internal void SetLevels(int par1)
    {
        for(int i = 0; i < par1; i++)
        {
            levels.Add(new Level());
        }
    }

    internal void NextLevel()
    {
        currentLevel += 1;
        levels.RemoveAt(0);
    }

    private void GenerateFleet()
    {
        SetStoryStarts();
    }

    private void SetStoryStarts()
    {
        SetLevels(15);
        GenerateNextMiniBoss(5, 6);
        getLevel(1).AddArea(new MapArea("Deep Space", MapType.Fight, 0001));
        //Generate Bar and Cider Woods as they are interchangable
        float rand = Tools.GenerateRand();
        Debug.Log("rand Bar and Cider: " + rand);
        if (rand > 50)
        {
            if(rand > 20)
            {
                getLevel(2).AddArea(new MapArea("Space Bar", MapType.Bar, 1000));
                getLevel(3).AddArea(new MapArea("Cider Wood City", MapType.Dungeon, 2000));
            }
            else
            {

                getLevel(2).AddArea(new MapArea("Cider Wood City", MapType.Dungeon, 2000));
                getLevel(3).AddArea(new MapArea("Space Bar", MapType.Bar, 1000));
            }
        }
        else
        {
            getLevel(2).AddArea(new MapArea("Space Bar", MapType.Bar, 1000));
            getLevel(2).AddArea(new MapArea("Cider Wood City", MapType.Dungeon, 2000));
        }
        //Generate the possiblity of the Cored Planet appearing without hearing of it
        if(nextMiniBoss == 5)
        {
            if (Tools.GenerateRand() < 30)
            {
                GenerateCorePlanet();
            }
        }else if(nextMiniBoss == 6)
        {
            if(Tools.GenerateRand() < 40)
            {
                GenerateCorePlanet();
            }
        }
        

    }

    private void GenerateNextMiniBoss(int start, int end)
    {
        float rand = Tools.GenerateRand();
        int var1 = end - start + 1;
        float interval = 100 / var1;
        float min = 100;
        for (int i = 0; i < var1; i++)
        {
            min -= interval;
            if (rand > min)
            {
                nextMiniBoss = start + i;
                Debug.Log("MiniBoss set to " + nextMiniBoss);
                return;
            }
        }
    }

    //Story specific generators
    private void GenerateCorePlanet()
    {
        if(Tools.GenerateRand() > 50)
        {
            MapArea area = new MapArea("Cored Planet", MapType.Dungeon, 2001);
            area.SetIsGuarded(85);
            getLevel(nextMiniBoss - 2).AddArea(area);
            Debug.Log("Added Core Planet to " + (nextMiniBoss - 2));
        }
    }
}
