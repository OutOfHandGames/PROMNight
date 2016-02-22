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


    public void updateMapWeights(Tile tileAtPoint)
    {
        Point2 tileLocation = tileAtPoint.getLocation();
        if (tileAtPoint.getCurrentEntity() == null)
        {
            if (tileAtPoint.currentTileType == aiStateMachine.aiTeam)
            {
                mapWeights[tileLocation.x, tileLocation.y] = 0;
            }
            else
            {
                mapWeights[tileLocation.x, tileLocation.y] = 2;
            }

        }
        else
        {
            Entity tileEntity = tileAtPoint.getCurrentEntity();
            mapWeights[tileLocation.x, tileLocation.y] = 0;//fill in the blanks here
        }
    }
}
