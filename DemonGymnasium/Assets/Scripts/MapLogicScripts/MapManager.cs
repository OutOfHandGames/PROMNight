using UnityEngine;
using System.Collections;

public class MapManager : MonoBehaviour {
	MapGenerator generator;

	void Start() {
		generator = GetComponent<MapGenerator> ();
	}


}
