using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIMapInfo : MonoBehaviour, MapProperties
{
    public int enemyTilesControlled;
    public int tilesControlled;

    AIStateMachine aiStateMachine;
    List<Entity> friendlyEntities = new List<Entity>();
    List<Entity> enemyEnities = new List<Entity>();

    Tile[,] map;

    void Start()
    {

    }

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
        tilesControlled = 0;
        enemyTilesControlled = 0;
        friendlyEntities.Clear();
        enemyEnities.Clear();
    }

    public void resetMap()
    {
        resetAllStats();
        map = new Tile[MapGenerator.BoardWidth, MapGenerator.BoardHeight];
        for (int x = 0; x < MapGenerator.BoardWidth; x++)
        {
            for (int y = 0; y < MapGenerator.BoardHeight; y++)
            {
                Tile tileAtPoint = MapGenerator.getTileAtPoint(x, y);
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
                    if (checkEntityIsFriendly(newEntity))
                    {
                        friendlyEntities.Add(newEntity);
                    }
                    else if (checkEntityIsEnemy(newEntity))
                    {
                        enemyEnities.Add(newEntity);
                    }
                    newTile.setEntity(newEntity);
                }
                if (checkEntityIsEnemy(tileAtPoint.currentTileType))
                {
                    tilesControlled++;
                }
                if (checkEntityIsFriendly(tileAtPoint.currentTileType))
                {
                    enemyTilesControlled++;
                }
            }
        }
    }

    public bool checkEntityIsFriendly(int entityType)
    {
        return entityType == aiStateMachine.aiTeam;
    }

    public bool checkEntityIsEnemy(int entityType)
    {
        return entityType == (aiStateMachine.aiTeam + 1) % 2;
    }

    public bool checkEntityIsFriendly(Entity entity)
    {
        return entity.entityType == aiStateMachine.aiTeam;
    }

    public bool checkEntityIsEnemy(Entity entity)
    {
        return entity.entityType == (aiStateMachine.aiTeam + 1) % 2;
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

    public List<Entity> getAllFriendlies()
    {
        return friendlyEntities;
    }

    public List<Entity> getAllEnemyEntities()
    {
        return enemyEnities;
    }
}
