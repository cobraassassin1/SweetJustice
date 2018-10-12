using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateManager : MonoBehaviour {

	public void PlayAnimation(int par1)
    {
        Debug.Log("GOT IT!!!");
        if(par1 == 0)
        {
            GetComponent<Animator>().SetBool("MoveUp", true);
        }else if(par1 == 1)
        {
            GetComponent<Animator>().SetBool("MoveDown", true);
        }else if (par1 == 2)
        {
            GetComponent<Animator>().SetBool("MoveLeft", true);
        }else if (par1 == 3)
        {
            GetComponent<Animator>().SetBool("MoveRight", true);
        }
        else {
            Debug.LogError("Stop your hurting me!!!");
        }
        
    }
}
