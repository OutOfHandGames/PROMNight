using UnityEngine;
using System.Collections.Generic;

public class UndoManager : MonoBehaviour {
    LinkedList<TurnInfo> turnStack = new LinkedList<TurnInfo>();
    TurnInfo currentTurnInfo;
    GameManager gameManager;

    void Start()
    {
        currentTurnInfo = new TurnInfo();
        gameManager = GetComponent<GameManager>();
    }


    public void finishTurn()
    {
        turnStack.AddLast(currentTurnInfo);
        currentTurnInfo = new TurnInfo();

    }

    public void addAffectedTile(Tile affectedTile)
    {
        currentTurnInfo.addAffectedTile(affectedTile);
    }

    public void saveGameState()
    {
        currentTurnInfo.addGameState(gameManager);
    }

    public void onUndoClicked()
    {
        if (turnStack.Count <= 0)
        {
            return;
        }
        TurnInfo turnInfo = turnStack.Last.Value;
        turnStack.RemoveLast();
        gameManager.setTurnsLeft(turnInfo.turnsLeft);
        gameManager.currentTurn = turnInfo.currentTurn;
        


        Tile[] alteredTiles = turnInfo.alteredTiles.ToArray();
        Entity[] alteredEntities = turnInfo.alteredEntities.ToArray();
        int[] previousStates = turnInfo.oldTileState.ToArray();
        for (int i = 0; i < turnInfo.alteredTiles.Count; i++)
        {
            alteredTiles[i].setEntity(alteredEntities[i]);
            if (alteredEntities[i] != null)
            {
                alteredEntities[i].transform.position = alteredTiles[i].transform.position;
            }
            alteredTiles[i].setTileType(previousStates[i]);
        }
        MapGenerator.updateTileScore();
    }

    public void resetCurrentTurnInfo()
    {
        currentTurnInfo = new TurnInfo();
    }
}
