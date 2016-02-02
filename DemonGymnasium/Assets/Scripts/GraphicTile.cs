using UnityEngine;
using System.Collections;

public class GraphicTile : MonoBehaviour {
    string[] triggerNames = { "Janitor", "Demon", "Neutral"};
    public Animator anim;


	// Use this for initialization
	void Start () {
        //anim = GetComponent<Animator>();
        
	}



    public void selectTileType(int tileType)
    {
        foreach(string n in triggerNames)
        {
           anim.ResetTrigger(n);
        }
        anim.SetTrigger(triggerNames[tileType]);
    }

    public void setAnim()
    {
        anim = GetComponent<Animator>();
    }


}
