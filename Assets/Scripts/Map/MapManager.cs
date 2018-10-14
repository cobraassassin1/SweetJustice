using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour {

    private TheBartender bartender;

    public int numOfAreas;
    public List<MapArea> areas = new List<MapArea>();
    public Level currentLevel; //Default is first Level
    public int fleetNum = 1; //Default is first fleet

    [Header("Test Variables")]
    public Text[] testTexts;
    public MapTestButton[] testButtons;

    private void Awake()
    {
        bartender = GameObject.FindGameObjectWithTag("GameController").GetComponent<TheBartender>();
        
        
    }

    // Use this for initialization
    void Start () {
        if (bartender.levels.Count > 0)
        {
            currentLevel = bartender.levels[0];
        }
        else
        {
            Debug.LogError("No Level Found");
        }
        GenerateLevel();
	}

    private void GenerateLevel()
    {
        int var1 = 0;
        List<MapArea> areas = currentLevel.getAreas();
        for (int i = 0; i < areas.Count; i++)
        {
            testButtons[i].setID(areas[i], this);
            testTexts[i].text = "Name: " + areas[i].GetName() + "\nType: " + areas[i].GetMapTypeString() + "\nGuarded: " + areas[i].isGuarded;
            var1++;
        }
        for(int i = var1; i < testTexts.Length; i++)
        {
            testTexts[i].text = "No Area Generated";
        }
    }

    public void ToArea(MapArea area)
    {
        MapType type = area.GetMapType();
        string var1 = "Nothing";
        if (type == MapType.Nothing)
        {
            Debug.LogError("Not a compatible scene");
        }
        else
        {
            switch (type)
            {
                case MapType.Bar:
                    var1 = "2DBar";
                    break;
                case MapType.Boss:
                    var1 = "2AFight";
                    break;
                case MapType.Casino:
                    var1 = "Casino";
                    break;
                case MapType.Fight:
                    var1 = "2AFight";
                    break;
                case MapType.Dungeon:
                    var1 = "2EDungeon";
                    break;
                case MapType.Station:
                    var1 = "Station";
                    break;
            }
        }
        bartender.currentType = type;
        bartender.ID = area.ID;
        LevelManager level = GetComponent<LevelManager>();
        level.LoadLevel(var1);
    }
}
