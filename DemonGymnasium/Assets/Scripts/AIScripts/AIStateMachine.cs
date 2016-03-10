using UnityEngine;
using System.Collections.Generic;

public class AIStateMachine : MonoBehaviour {
    public int aiTeam = Tile.DEMON;

    GameManager gameManager;
    ActionManager actionManager;
    StateAI currentState = new BasicStateAI();
    public AIMapInfo mapInfo;

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        actionManager = GameObject.FindObjectOfType<ActionManager>();
        //mapInfo = GetComponent<AIMapInfo>();
    }

    public void performActions()
    {
        List<MoveInfo> allMoves = new List<MoveInfo>();
        allMoves = currentState.getBestMoves(this);
        
    }

}
