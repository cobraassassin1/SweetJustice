using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTestButton : MonoBehaviour {

    public MapArea area;

    internal MapManager map;

    internal void setID(MapArea par1, MapManager par2)
    {
        area = par1;
        map = par2;
    }

    public void LoadMapType()
    {
        map.ToArea(area);
    }
}
