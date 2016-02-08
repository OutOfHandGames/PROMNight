using UnityEngine;
using System.Collections.Generic;

public class TurnInfo  {
    public int currentTurn;
    public int turnsLeft;
    public List<Tile> alteredTiles = new List<Tile>();
    public List<Entity> alteredEntities = new List<Entity>();
    public List<int> oldTileState = new List<int>();

    

    public void addAffectedTile(Tile tile)
    {
        alteredTiles.Add(tile);
        oldTileState.Add(tile.getCurrentTileType());
        alteredEntities.Add(tile.getCurrentEntity());
    }

    public void addGameState(GameManager gameManager)
    {
        currentTurn = gameManager.currentTurn;
        turnsLeft = gameManager.getTurnsBeforeNextRound();
    }
}
