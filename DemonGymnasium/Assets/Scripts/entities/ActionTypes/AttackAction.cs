using UnityEngine;
using System.Collections;
using System;

public class AttackAction : Actions
{
    public GameObject projectile;

    public override void initializeLegalActions()
    {
        legalActions = new Point2[] { Point2.NORTH, Point2.NORTH * 2, Point2.SOUTH, Point2.SOUTH * 2, Point2.EAST, Point2.EAST * 2, Point2.WEST, Point2.WEST * 2 };

    }


    public override void OnActionClicked()
    {
        validPositions.Clear();
        Point2 origin = getEntity().getCurrentTile().getLocation();
        setActive(true);
        foreach(Point2 p in legalActions)
        {
            Point2 checkPoint = origin + p;
            if (!checkOutOfBoundsPoint(checkPoint))
            {
                validPositions.AddLast(checkPoint);
            }
        }
    }

    public override bool performAction(Tile tileClicked)
    {
        Point2 clickPoint = tileClicked.getLocation();
        Point2 origin = getEntity().getCurrentTile().getLocation();

        foreach (Point2 p in validPositions)
        {
            if (p == clickPoint)
            {
                Point2 direction = p - origin;
                int scale = (int)Mathf.Max(Mathf.Abs(direction.x), Mathf.Abs(direction.y));
                direction /= scale;
                Point2 checkPoint = origin + direction;
                while (checkPoint != clickPoint)
                {
                    setTile(checkPoint);
                    checkPoint += direction;
                }

                setTile(checkPoint);

                setActive(false);
                return true;
            }
        }

        return false;
    }

    void setTile(Point2 checkPoint)
    {
        if (checkEnemyPresent(MapGenerator.getTileAtPoint(checkPoint)))
        {
            MapGenerator.getTileAtPoint(checkPoint).getCurrentEntity().takeDamage();
        }
        MapGenerator.getTileAtPoint(checkPoint).setTileType(getEntity().entityType);
    }
}
