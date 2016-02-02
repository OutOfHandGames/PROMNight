﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour {
	private Text currentScore;

	void Awake(){
		currentScore = GetComponent<RectTransform> ().Find ("CurrentScore").GetComponent<Text>();
	}

    void Update()
    {
        int janitorScore = MapGenerator.currentTileTypes[Tile.JANITOR];

		currentScore.text = janitorScore + " : " + MapGenerator.currentTileTypes[Tile.DEMON];

    }
}
