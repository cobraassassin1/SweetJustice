using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestResponse : MonoBehaviour {

    private int ID;
    ConvoManager convo;
    private bool setUp = false;


    internal void setTestResponse(ConvoManager par1, int par2)
    {
        convo = par1;
        ID = par2;
        setUp = true;
    }

    private void OnMouseEnter()
    {
        if (setUp)
        {
            convo.RequestArrow(ID);
        }
    }
}
