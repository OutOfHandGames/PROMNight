using UnityEngine;
using System.Collections;

public class TileColorManager : MonoBehaviour {
    public static Color janitorControlledTile = Color.blue;
    public static Color demonControlledTile = Color.green;
    public static Color validTileSelection = Color.yellow;
    public static Color hoverTileColor = Color.red;
    public static Color neutralTileColor = Color.white;

    void Start()
    {
        colorAllTiles();
    }

    void Update()
    {
        highlightMouseTile();
    }

    void highlightMouseTile()
    {

    }

    public void colorAllTiles()
    {
        for (int x = 0; x < MapGenerator.BoardWidth; x++)
        {
            for (int y = 0; y < MapGenerator.BoardHeight; y++)
            {
                resetTileColor(MapGenerator.getTileAtPoint(new Point2(x, y)));
            }
        }
    }

    public void colorValidSquares(Point2[] validPoints)
    {

    }

    public void resetValidSquares(Point2[] validPoints)
    {

    }

    public static void resetTileColor(Tile tile)
    {
        if (tile.getCurrentTileType() == Tile.JANITOR)
        {
            tile.GetComponentInChildren<Renderer>().material.color = janitorControlledTile;
        }
        else if (tile.getCurrentTileType() == Tile.DEMON)
        {
            tile.GetComponentInChildren<Renderer>().material.color = demonControlledTile;
        }
        else if (tile.getCurrentTileType() == Tile.NEUTRAL)
        {
            tile.GetComponentInChildren<Renderer>().material.color = neutralTileColor;
        }
    }
}
