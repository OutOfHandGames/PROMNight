using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class AttackAction : Actions
{
    public int attackRange = 2;

    public override List<Point2> getAffectedTiles(Point2 origin, Point2 effectedTile, MapProperties mapProperties)
    {
        List<Point2> affectedTiles = new List<Point2>();
        Point2 dir = effectedTile - origin;
        dir.normalizeValues();
        Point2 checkPoint = origin;
        for (int i = 0; i < attackRange; i++)
        {
            checkPoint += dir;
            if (checkPointOutOfBounds(checkPoint))
            {
                break;
            }
            Tile tileAtPoint = mapProperties.getTile(checkPoint);
            if (checkTileContainsObstacle(tileAtPoint))
            {
                break;
            }
            affectedTiles.Add(tileAtPoint.getLocation());
        }
        return affectedTiles;
    }

    public override List<Point2> getValidMoves(Point2 origin, MapProperties mapProperties)
    {
        validPoints.Clear();
        Point2 checkPoint;
        foreach (Point2 dir in legalMoves)
        {
            checkPoint = origin;
            for (int i = 0; i < attackRange; i++)
            {
                checkPoint += dir;
                if (checkPointOutOfBounds(checkPoint))
                {
                    break;
                }
                Tile tileAtPoint = mapProperties.getTile(checkPoint);
                if (checkTileContainsObstacle(tileAtPoint))
                {
                    break;
                }
                validPoints.Add(checkPoint);
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
        foreach(Point2 p in validPoints)
        {
            if (p == tilePoint)
            {
                List<Point2> affectedTiles = getAffectedTiles(getEntity().getCurrentTile().getLocation(), tilePoint, mapProperties);
                foreach(Point2 t in affectedTiles)
                {
                    Tile tileAtPoint = mapProperties.getTile(t);
                    if (checkTileContainsEnemy(tileAtPoint))
                    {
                        tileAtPoint.getCurrentEntity().takeDamage();
                    }
                    tileAtPoint.setTileType(getEntity().entityType);
                }
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
