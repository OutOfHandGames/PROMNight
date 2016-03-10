using UnityEngine;
using System.Collections;

public class SelfDestruct : MonoBehaviour {

    public float timer = 0.2f;

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	    if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            Destroy(this.gameObject);
        }
	}
}
