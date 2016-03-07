using UnityEngine;
using System.Collections.Generic;

public class AIStateMachine : MonoBehaviour {

    GameManager gameManager;
    ActionManager actionManager;
    StateAI currentState = new BasicStateAI();
    AIMapInfo mapInfo;
    int aiTeam = Tile.DEMON;

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        actionManager = GameObject.FindObjectOfType<ActionManager>();
        mapInfo = GetComponent<AIMapInfo>();
    }

    public void performActions()
    {
        List<MoveInfo> allMoves = new List<MoveInfo>();
        allMoves = currentState.getBestMoves(this);
        
    }

}
