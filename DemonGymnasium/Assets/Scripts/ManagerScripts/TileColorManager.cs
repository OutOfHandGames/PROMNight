using UnityEngine;

using System.Collections.Generic;

public class TileColorManager : MonoBehaviour {
    public static Color janitorControlledTile = Color.blue;
    public static Color demonControlledTile = Color.green;
    public static Color validTileSelection = Color.yellow;
    public static Color hoverTileColor = Color.red;
    public static Color neutralTileColor = Color.white;

    Camera mainCamera;
    bool colorValidTilesOn;
    List<Point2> validPoints;

    void Start()
    {
        colorAllTiles();
        mainCamera = GameObject.FindObjectOfType<Camera>();
    }

    void Update()
    {
        if (colorValidTilesOn)
        {
            colorValidSquares(validPoints);
        }
        highlightMouseTile();

    }

    void highlightMouseTile()
    {
        Tile tile = null;

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            tile = hit.collider.GetComponent<Tile>();
            if (tile != null)
            {
                tile.GetComponentInChildren<Renderer>().material.color = hoverTileColor;
            }
        }
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

    public void colorValidSquares(List<Point2> validPoints)
    {
        foreach (Point2 p in validPoints)
        {
            MapGenerator.getTileAtPoint(p).GetComponentInChildren<Renderer>().material.color = validTileSelection;
        }
        this.validPoints = validPoints;
        colorValidTilesOn = true;
    }

    public void resetValidSquares(List<Point2> validPoints)
    {
        foreach (Point2 p in validPoints)
        {
            resetTileColor(MapGenerator.getTileAtPoint(p));
        }
        colorValidTilesOn = false;
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
