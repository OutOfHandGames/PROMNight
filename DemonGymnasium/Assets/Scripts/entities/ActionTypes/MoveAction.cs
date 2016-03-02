using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class MoveAction : Actions
{
    public override List<Point2> getAffectedTiles(Point2 origin, Point2 effectedTile, MapProperties mapProperties)
    {
        throw new NotImplementedException();
    }

    public override List<Point2> getValidMoves(Point2 origin, MapProperties mapProperties)
    {
        validPoints.Clear();
        foreach (Point2 dir in legalMoves)
        {
            Point2 checkPoint = origin + dir;
            if (!checkPointOutOfBounds(checkPoint))
            {
                Tile tileAtPoint = mapProperties.getTile(checkPoint);
                if (checkTileNeutral(tileAtPoint))
                {
                    validPoints.Add(checkPoint);
                }
                else
                {
                    while (checkTileOwned(tileAtPoint) && !checkTileContainsEntity(tileAtPoint))
                    {
                        validPoints.Add(checkPoint);
                        checkPoint += dir;
                        if (checkPointOutOfBounds(checkPoint))
                        {
                            break;
                        }
                        tileAtPoint = mapProperties.getTile(checkPoint);
                    }
                }
            }
        }
        return validPoints;
    }

    public override void onActionClicked(MapProperties mapProperties)
    {
        throw new NotImplementedException();
    }

    public override bool performAction(Point2 tilePoint, MapProperties mapProperties)
    {
        foreach (Point2 p in validPoints)
        {
            if (tilePoint == p)
            {
                List<Point2> affectedTiles = getAffectedTiles(getEntity().getCurrentTile().getLocation(), p, mapProperties);
                mapProperties.getTile(affectedTiles[0]).setEntity(null);
                mapProperties.getTile(affectedTiles[1]).setEntity(getEntity());
                return true;
            }
        }
        return false;
    }

    public override void performAnimations()
    {
        throw new NotImplementedException();
    }
}
