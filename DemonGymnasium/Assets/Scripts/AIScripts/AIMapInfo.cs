using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIMapInfo : MapProperties
{
    int totalEnemies;
    int tilesControlled;
    List<Point2> friendlyPositions;
    List<Point2> enemyPositions;

    Tile[,] map;

    public Tile getTile(Point2 point)
    {
        return map[point.x, point.y];
    }

    public Tile getTile(int x, int y)
    {
        return map[x, y];
    }

    void resetAllStats()
    {
        totalEnemies = 0;
        tilesControlled = 0;
        friendlyPositions.Clear();
        enemyPositions.Clear();
    }

    public void resetMap(MapProperties currentMap)
    {
        resetAllStats();
        map = new Tile[MapGenerator.BoardWidth, MapGenerator.BoardHeight];
        for (int x = 0; x < MapGenerator.BoardWidth; x++)
        {
            for (int y = 0; y < MapGenerator.BoardHeight; y++)
            {
                Tile tileAtPoint = currentMap.getTile(x, y);
                Tile newTile = new Tile();
                newTile.setTileType(tileAtPoint.getCurrentTileType());
                if (tileAtPoint.getCurrentEntity() != null)
                {
                    Entity newEntity = null;
                    if (tileAtPoint.getCurrentEntity() is King)
                    {
                        newEntity = new King();
                    }
                    else
                    {
                        newEntity = new Minion();
                    }
                    newEntity.entityType = tileAtPoint.getCurrentEntity().entityType;
                    newTile.setEntity(newEntity);
                }
            }
        }
    }

    /// <summary>
    /// Pass in a list of moves that have been played in the game, in order to redo the map to its original state
    /// </summary>
    /// <param name="moveList"></param>
    public void redoMap(List<MoveInfo> moveList) 
    {
        for (int i = moveList.Count; i > 0; i--)
        {
            MoveInfo mInfo = moveList[i];
            getTile(mInfo.tilePositionSelected).setEntity(mInfo.entity);
        }
    }
}
