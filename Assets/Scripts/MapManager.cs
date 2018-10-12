using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour {

    private TheBartender bartender;

    public int numOfAreas;
    public List<MapArea> areas = new List<MapArea>();
    public int currentLevel = 1; //Default is first Level
    public int fleetNum = 1; //Default is first fleet

    [Header("Probabilty Variables")]
    public int[] probNum = { 30, 40, 20, 10 };
    public float[] probType = { 5, 5, 0, 0, 0, 0 };

    [Header("Test Variables")]
    public Text[] testTexts;
    public Button[] testButtons;

    private void Awake()
    {
        bartender = GameObject.FindGameObjectWithTag("GameController").GetComponent<TheBartender>();
        currentLevel = bartender.levelID;
    }

    // Use this for initialization
    void Start () {
        CheckProb();
        GetAreas();
        GenerateAreas();
	}

    private void CheckProb()
    {
        if(totalProbNum(probNum) != 100)
        {
            Debug.LogError("Incorrect Probabilty for Amount of Areas");
        }
        if (bartender.currentType == MapType.Bar)
        {
            probType[0] = 0;
        }
    }

    private int totalProbNum(int[] par1)
    {
        int total = 0;
        for (int i = 0; i < par1.Length; i++)
        {
            total += par1[i];
        }
        return total;
    }

    private void GetAreas()
    {
        if (currentLevel == 15)
        {
            numOfAreas = 1;
        }
        else
        {
            float rand = UnityEngine.Random.value * 100;
            int var1 = 100;
            for (int i = 0; i < probNum.Length; i++)
            {
                var1 -= probNum[i];
                if (rand > var1)
                {
                    numOfAreas = i + 2;
                    return;
                }

            }

        }

        
    }

    private void GenerateAreas()
    {
        int var1 = 0;
        for (int i = 0; i < numOfAreas; i++)
        {
            MapArea area = new MapArea("Area" + i, GenerateType());
            testTexts[i].text = "Name: " + area.GetName() + "\nType: " + area.GetMapTypeString();
            areas.Add(area);
            var1++;
        }
        for (int i = var1; i < testTexts.Length; i++)
        {
            testTexts[i].text = "No Area Generated";
        }
        for (int i = 0; i < areas.Count; i++)
        {
            MapTestButton var2 = testButtons[i].GetComponent<MapTestButton>();
            var2.setID(areas[i].GetMapType(), this);
        }
    }

    private MapType GenerateType()
    {
        float rand = UnityEngine.Random.value * 100;
        float[] prob = new float[probType.Length];
        prob = CreateProbType();

        MapType current = MapType.Nothing;

        if (currentLevel == 1 && fleetNum == 1)
        {
            current = MapType.Fight;
        }else if(currentLevel == 15)
        {
            current = MapType.Boss;
        }
        else
        {
            float var1 = 100f;
            for (int i = 0; i < probType.Length; i++)
            {
                var1 -= prob[i];
                if (rand > var1 && prob[i] != 0)
                {
                    current = (MapType)i;
                    goto End; 
                }
            }
        }
    End:
        switch (current)
        {
            case MapType.Bar:
                dropProb((int)current);
                break;
        }
        return current;
    }

    private float[] CreateProbType()
    {
        float[] prob = new float[probType.Length];
        for (int i = 0; i < probType.Length; i++)
        {
            prob[i] = (probType[i] / TotalTypeProb())* 100f;
        }
        String var1 = "{";
        for(int i = 0; i < prob.Length; i++)
        {
            var1 += prob[i];
            if(i != prob.Length - 1)
            {
                var1 += ",";
            }
        }
        var1 += "}";
        Debug.Log(var1);
        return prob;
    }

    private float TotalTypeProb()
    {
        float var1 = 0;
        for (int i = 0; i < probType.Length; i++)
        {
            var1 += probType[i];
        }
        return var1;
    }

    public void ToArea(MapType par1)
    {
        string var1 = "Nothing";
        if (par1 == MapType.Nothing)
        {
            Debug.LogError("Not a compatible scene");
        }
        else
        {
            switch (par1)
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
                case MapType.Prison:
                    var1 = "Prison";
                    break;
                case MapType.Station:
                    var1 = "Station";
                    break;
            }
        }
        bartender.currentType = par1;
        LevelManager level = GetComponent<LevelManager>();
        level.LoadLevel(var1);
    }

    private void dropProb(int i)
    {
        probType[i] = 0;
    }
}
