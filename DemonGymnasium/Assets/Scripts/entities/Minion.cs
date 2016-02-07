using UnityEngine;
using System.Collections;

public class Minion : Entity {
    //Animator anim;

	// Use this for initialization
	new void Start () {
		base.Start ();
        //anim = GetComponentInChildren<Animator>();
		//GetComponentInChildren<Renderer> ().material.color = Color.yellow;
	}
	
	// Update is called once per frame
	new void Update () {
		base.Update ();
        
	}

	public new void act() {
		base.act ();
		//TODO attack animation
		Debug.Log ("Attack!");
	}
}
