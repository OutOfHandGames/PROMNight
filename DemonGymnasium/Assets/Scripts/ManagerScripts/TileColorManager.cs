using UnityEngine;

using System.Collections.Generic;

public class TileColorManager : MonoBehaviour {
    public static Color janitorControlledTile = Color.blue;
    public static Color demonControlledTile = Color.green;
    public static Color validTileSelection = Color.yellow;
    public static Color hoverTileColor = Color.red;
    public static Color neutralTileColor = Color.white;

    Camera mainCamera;
    Renderer mouseOverTile;
    bool colorValidTilesOn;
    List<Point2> validPoints;
    UIManager uiManager;

    void Start()
    {
        colorAllTiles();
        uiManager = GameObject.FindObjectOfType<UIManager>();
        mainCamera = GameObject.FindObjectOfType<Camera>();
        validPoints = new List<Point2>();

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
        Renderer tile = null;
        if (mouseOverTile != null)
        {
            resetTileColor(mouseOverTile.transform.parent.GetComponent<Tile>());
        }
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (uiManager.getActionPanelActive())
        {
            mouseOverTile = null;
        }
        else if (Physics.Raycast(ray, out hit))
        {
            tile = hit.collider.GetComponentInChildren<Renderer>();
            mouseOverTile = tile;
            if (tile != null)
            {

                tile.material.color = hoverTileColor;
            }
        }
        else
        {
            if (mouseOverTile != null)
            {
                resetTileColor(mouseOverTile.transform.parent.GetComponent<Tile>());
                mouseOverTile = null;
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
        print(validPoints.Count);
        foreach (Point2 p in validPoints)
        {
            MapGenerator.getTileAtPoint(p).GetComponentInChildren<Renderer>().material.color = validTileSelection;
        }
        this.validPoints = validPoints;
        colorValidTilesOn = true;
    }

    public void resetValidSquares()
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
