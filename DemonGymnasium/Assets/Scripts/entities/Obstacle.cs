using UnityEngine;
using System.Collections;

public class Obstacle : Entity {

	new void Start() {
		base.Start();
		//GetComponentInChildren<Renderer> ().material.color = Color.black;
	}

    new void Update()
    {
		base.Update();
    }
}
