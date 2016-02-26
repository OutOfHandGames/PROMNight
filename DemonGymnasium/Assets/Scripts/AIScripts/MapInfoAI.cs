using UnityEngine;
using System.Collections;

public class MapInfoAI : MonoBehaviour {
    public const int JANITOR_OWNED = 0;
    public const int DEMON_OWNED = 1;
    public const int NEUTRAL_OWNED = 2;
    public const int JANITOR_PAWN = 3;
    public const int JANITOR_KING = 4;
    public const int DEMON_PAWN = 5;
    public const int DEMON_KING = 6;

    static int[,] currentMapLayout;
    static int[,] alteredMapLayout;

    void Start()
    {
        currentMapLayout = new int[MapGenerator.BoardWidth, MapGenerator.BoardHeight];
        alteredMapLayout = new int[MapGenerator.BoardWidth, MapGenerator.BoardHeight];
    }

    public static void updateCurrentMapLayout()
    {
        Tile tileAtPoint = null;
        for (int x = 0; x < currentMapLayout.GetLength(0); x++)
        {
            for (int y = 0; y < currentMapLayout.GetLength(1); y++)
            {
                tileAtPoint = MapGenerator.getTileAtPoint(x, y);
                if (tileAtPoint.getCurrentEntity() != null)
                {
                    Entity checkEntity = tileAtPoint.getCurrentEntity();
                    if (checkEntity is King)
                    {
                        if (checkEntity.entityType == GameManager.JANITOR)
                        {
                            currentMapLayout[x, y] = JANITOR_KING;
                        }
                        else
                        {
                            currentMapLayout[x, y] = DEMON_KING;
                        }
                    }
                    else if (checkEntity is Minion)
                    {
                        if (checkEntity.entityType == GameManager.JANITOR)
                        {
                            currentMapLayout[x, y] = JANITOR_PAWN;
                        }
                        else
                        {
                            currentMapLayout[x, y] = DEMON_PAWN;
                        }
                    }
                }
                else
                {
                    currentMapLayout[x, y] = tileAtPoint.currentTileType;
                }
            }
        }
        resetAlteredToCurrentState();
    }


    public static void changeTileProperty (int x, int y, int property)
    {
        alteredMapLayout[x, y] = property;
    }

    public static void changeTileProperty(Point2 point, int property)
    {
        changeTileProperty(point.x, point.y, property);
    }

    public static void resetAlteredToCurrentState()
    {
        for (int x = 0; x < currentMapLayout.GetLength(0); x++)
        {
            for (int y = 0; y < currentMapLayout.GetLength(1); y++)
            {
                changeTileProperty(x, y, currentMapLayout[x, y]);
            }
        }
    }


}
