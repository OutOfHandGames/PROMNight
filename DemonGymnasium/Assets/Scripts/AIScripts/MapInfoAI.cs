using UnityEngine;
using System.Collections;

public class MapInfoAI : MonoBehaviour {

    public const int JANITOR = 0;
    public const int DEMON = 1;
    public const int NEUTRAL = 2;
    public const int JANITOR_PAWN = 3;
    public const int DEMON_PAWN = 4;
    public const int JANITOR_KING = 5;
    public const int DEMON_KING = 6;

    AIStateMachine aiStateMachine;
    int[,] mapInfo;
    float[,] mapWeights;

    void Start()
    {
        mapInfo = new int[MapGenerator.BoardWidth, MapGenerator.BoardHeight];
        mapWeights = new float[MapGenerator.BoardWidth, MapGenerator.BoardHeight];
        updateMapProperties();
    }

    public void updateMapProperties()
    {
        for (int x = 0; x < mapInfo.GetLength(0); x++)
        {
            for (int y = 0; y < mapInfo.GetLength(1); y++)
            {
                Tile tileAtPoint = MapGenerator.getTileAtPoint(x, y);
                if (tileAtPoint.getCurrentEntity() == null)
                {
                    mapInfo[x, y] = tileAtPoint.currentTileType;
                }
                else
                {
                    Entity checkEntity = tileAtPoint.getCurrentEntity();
                    if (checkEntity is King)
                    {
                        if (checkEntity.entityType == GameManager.JANITOR)
                        {
                            mapInfo[x, y] = JANITOR_KING;
                        }
                        else
                        {
                            mapInfo[x, y] = DEMON_KING;
                        }
                    }
                    else
                    {
                        if (checkEntity.entityType == GameManager.DEMON)
                        {
                            mapInfo[x, y] = JANITOR_PAWN;
                        }
                        else
                        {
                            mapInfo[x, y] = DEMON_PAWN;
                        }
                    }
                }
            }
        }
    }


    public float getWeightAtLocation (Point2 tilePosition)
    {
        int tileValue = mapInfo[tilePosition.x, tilePosition.y];
        int weight = 0;
        if (tileValue == aiStateMachine.aiTeam)
        {
            weight += 0;
        }
        else if (tileValue == aiStateMachine.enemyTeam)
        {
            weight += 3;
        }
        else if (tileValue == NEUTRAL)
        {
            weight += 1;
        }
        
        if (tileValue == getEnemyID("King"))
        {
            weight += 50;
        }
        else if (tileValue == getEnemyID("Pawn"))
        {
            weight += 15;
        }


        return weight;
    }

    int getEnemyID(string type)
    {
        switch (type)
        {
            case "King":
                return JANITOR_KING;
            case "Pawn":
                return JANITOR_PAWN;
            default:
                return -1;
        }

    }

    public float getWeightAtLocation(int x, int y)
    {
        return getWeightAtLocation(new Point2(x, y));
    }
}
