using UnityEngine;
using System.Collections;

public class TileColorManager : MonoBehaviour {
    public Color janitorControlledTile = Color.blue;
    public Color demonControlledTile = Color.green;
    public Color validTileSelection = Color.yellow;
    public Color hoverTileColor = Color.red;
    public Color neutralTileColor = Color.white;

    void Update()
    {
        highlightMouseTile();
    }

    void highlightMouseTile()
    {

    }

    public void colorValidSquares(Point2[] validPoints)
    {

    }

    public void resetValidSquares(Point2[] validPoints)
    {

    }

    public void resetTileColor(Point2 tile)
    {

    }
}
