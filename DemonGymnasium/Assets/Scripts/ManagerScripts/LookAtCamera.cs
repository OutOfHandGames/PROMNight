using UnityEngine;
using System.Collections;

public class LookAtCamera : MonoBehaviour {
    Transform cameraPosition;

	// Use this for initialization
	void Start () {
        cameraPosition = GameObject.FindGameObjectWithTag("MainCamera").transform;
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(cameraPosition.position, cameraPosition.up);
	}
}
