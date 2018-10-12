using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MapType {Bar, Fight, Station, Casino, Prison, Boss, CrashedShip, Nothing};

[System.Serializable]
public class MapArea: ScriptableObject {

    internal string areaName;
    internal MapType type;
    internal int ID;
    internal bool isGuarded = false;

    internal MapArea(string var1, MapType var2)
    {
        areaName = var1;
        type = var2;
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
            case MapType.Prison:
                var1 = "Prison";
                break;
            case MapType.Station:
                var1 = "Station";
                break;
        }
        return var1;
    }
}
