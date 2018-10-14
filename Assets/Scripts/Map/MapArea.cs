using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MapType {Bar, Fight, Station, Casino, Dungeon, Boss, CrashedShip, Nothing};

[System.Serializable]
public class MapArea {

    internal string areaName;
    internal MapType type;
    internal int ID;
    internal bool isGuarded = false;

    private static Tools TOOLS = new Tools();

    internal MapArea(string var1, MapType var2, int var3)
    {
        areaName = var1;
        type = var2;
        ID = var3;
    }

    internal string GetName()
    {
        return areaName;
    }

    internal MapType GetMapType()
    {
        return type;
    }

    internal string GetMapTypeString()
    {
        string var1 = "";
        switch (type)
        {
            case MapType.Bar:
                var1 = "Bar";
                break;
            case MapType.Boss:
                var1 = "Boss";
                break;
            case MapType.Casino:
                var1 = "Casino";
                break;
            case MapType.Fight:
                var1 = "Fight";
                break;
            case MapType.Dungeon:
                var1 = "Dungeon";
                break;
            case MapType.Station:
                var1 = "Station";
                break;
        }
        return var1;
    }

    internal bool GetIsGuarded()
    {
        return isGuarded;
    }

    internal void SetIsGuarded(int percent)
    {
        if(TOOLS.GenerateRand() < percent)
        {
            isGuarded = true;
        }
    }
}
