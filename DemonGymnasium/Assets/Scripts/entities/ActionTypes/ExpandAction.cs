using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class ExpandAction : Actions
{
    Animator anim;

    void Start()
    {
        anim = getEntity().GetComponentInChildren<Animator>();
    }

    public override List<Point2> getAffectedTiles(Point2 origin, Point2 effectedTile, MapProperties mapProperties)
    {
        Point2 checkPoint = origin;
        List<Point2> affectedTiles = new List<Point2>();
        foreach (Point2 p in legalMoves)
        {
            checkPoint = origin + p;
            if (!checkPointOutOfBounds(checkPoint))
            {
                Tile tileAtPoint = mapProperties.getTile(checkPoint);
                if (!checkTileContainsObstacle(tileAtPoint))
                {
                    affectedTiles.Add(checkPoint);
                }
            }
        }
        return affectedTiles;
    }

    public override List<Point2> getValidMoves(Point2 origin, MapProperties mapProperties)
    {
        validPoints.Clear();
        return validPoints;
    }

    public override void onActionClicked(MapProperties mapProperties)
    {
        throw new NotImplementedException();
    }

    public override bool performAction(Point2 tilePoint, MapProperties mapProperties)
    {
        List<Point2> affectedTiles = getAffectedTiles(getEntity().getCurrentTile().getLocation(), tilePoint, mapProperties);
        foreach (Point2 p in affectedTiles)
        {
            Tile tileAtPoint = mapProperties.getTile(p);
            if (checkTileContainsEnemy(tileAtPoint))
            {
                tileAtPoint.getCurrentEntity().takeDamage();
            }
            tileAtPoint.setTileType(getEntity().entityType);

        }
        return true;
    }

    public override void performAnimations()
    {
        anim.SetTrigger("Expand");
    }
}
