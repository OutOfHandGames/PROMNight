using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class ExpandAction : Actions {

    public override void initializeLegalActions()
    {
        legalActions = new Point2[] { Point2.NORTH, Point2.SOUTH, Point2.EAST, Point2.WEST, Point2.ZERO };
    }

    public override void OnActionClicked(ActionManager actionManager)
    {
        setActive(true);
        actionManager.performAction(getEntity().getCurrentTile());
        
    }

    public override List<Point2> getAffectedTiles()
    {

        return null;
    }

    public override List<Point2> findValidPositions(Point2 origin)
    {
        foreach (Point2 p in legalActions)
        {
            Point2 checkPoint = origin + p;

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

    public override bool performAction(Tile tileSelected, UndoManager undoManager)
    {
        findValidPositions(getEntity().getCurrentTile().getLocation());
        foreach (Point2 p in validPositions)
        {
            Tile tileAtPoint = MapGenerator.getTileAtPoint(p);
            undoManager.addAffectedTile(tileAtPoint);
            if (checkEnemyPresent(tileAtPoint))
            {
                tileAtPoint.getCurrentEntity().takeDamage();
            }
            tileAtPoint.setTileType(getEntity().entityType);
        }
        MapGenerator.updateTileScore();
        setActive(false);
        setAnimationTrigger();
        validPositions.Clear();
        return true;
    }
}
