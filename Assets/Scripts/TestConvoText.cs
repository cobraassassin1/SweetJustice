using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestConvoText : MonoBehaviour {

    public Text text;

	// Use this for initialization
	internal void ChangeText(string par1) {
        text.text = "Character Speaking To: " + par1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
