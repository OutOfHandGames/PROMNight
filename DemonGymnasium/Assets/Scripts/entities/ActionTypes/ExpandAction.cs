using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class ExpandAction : Actions {


    void Start()
    {
    }

    public override void initializeLegalActions()
    {
        legalActions = new Point2[] { Point2.NORTH, Point2.SOUTH, Point2.EAST, Point2.WEST, Point2.ZERO };
    }

    public override void OnActionClicked()
    {
        performAction(getEntity().getCurrentTile());
        
    }

    public override List<Point2> findValidPositions()
    {
        foreach (Point2 p in legalActions)
        {
            Point2 checkPoint = getEntity().getCurrentTile().getLocation() + p;

            if (!checkOutOfBoundsPoint(checkPoint))
            {
                Tile tileAtPoint = MapGenerator.getTileAtPoint(checkPoint);
                if (!checkObstaclePresent(tileAtPoint))
                {
                    validPositions.Add(checkPoint);
                }
            }
        }
        return validPositions;
    }

    public override bool performAction(Tile tileSelected)
    {
        findValidPositions();
        foreach (Point2 p in validPositions)
        {
            Tile tileAtPoint = MapGenerator.getTileAtPoint(p);
            if (checkEnemyPresent(tileAtPoint))
            {
                tileAtPoint.getCurrentEntity().takeDamage();
            }
            tileAtPoint.setTileType(getEntity().entityType);
        }
        return true;
    }
}
