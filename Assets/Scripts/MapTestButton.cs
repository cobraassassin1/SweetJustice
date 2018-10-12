using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTestButton : MonoBehaviour {

    public MapType ID;

    internal MapManager map;

    internal void setID(MapType par1, MapManager par2)
    {
        ID = par1;
        map = par2;
    }

    public void LoadMapType()
    {
        map.ToArea(ID);
    }
}
