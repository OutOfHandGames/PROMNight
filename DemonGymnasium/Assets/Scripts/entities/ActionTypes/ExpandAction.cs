using UnityEngine;
using System.Collections;
using System;

public class ExpandAction : Actions {
    

    void Start()
    {
        legalActions = new Point2[]{ Point2.NORTH, Point2.SOUTH, Point2.EAST, Point2.WEST, Point2.ZERO };
    }


    public override Point2[] getLegalActions()
    {
        return legalActions;
    }

    public override void OnActionClicked()
    {
        performAction(getEntity().getCurrentTile());
        
    }

    public override void performAction(Tile tileSelected)
    {
        Tile[,] map = MapGenerator.mapTiles;
        Point2 originPosition = tileSelected.getLocation();
        Point2 checkPosition = null;
        foreach (Point2 p in legalActions)
        {
            if (!checkOutOfBoundsPoint(originPosition, p))
            {
                checkPosition = new Point2(originPosition.x + p.x, originPosition.y + p.y);
                Tile tile = map[checkPosition.x, checkPosition.y];
                tile.setTileType(getEntity().entityType);
                if (checkEnemyPresent(tile))
                {
                    tile.getCurrentEntity().takeDamage();
                }
            }
        }
    }
}
