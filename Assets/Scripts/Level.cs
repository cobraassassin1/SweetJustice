using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level {
    [SerializeField]
    private List<MapArea> maps = new List<MapArea>();

    internal Level()
    {

    }

    internal Level(List<MapArea> par1)
    {
        maps = par1;
    }

    internal void AddArea(MapArea par1)
    {
        maps.Add(par1);
    }

    internal void AddAreas(List<MapArea> par1)
    {
        for (int i = 0; i < par1.Count; i++)
        {
            maps.Add(par1[i]);
        }
    }

    internal List<MapArea> getAreas()
    {
        return maps;
    }
}
