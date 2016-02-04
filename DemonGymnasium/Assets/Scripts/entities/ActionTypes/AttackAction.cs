using UnityEngine;
using System.Collections;
using System;

public class AttackAction : Actions
{
    public GameObject projectile;
    public int attackRange = 2;

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
        setActive(false);

        foreach (Point2 p in validPositions)
        {
            print("P" + p + "   click" + clickPoint );
            if (p == clickPoint)
            {
                Point2 direction = p - origin;
                direction.normalizeValues();
                Point2 checkPoint = origin;
                for (int i = 0; i < attackRange; i++)
                {
                    checkPoint += direction;
                    Tile tileAtPoint = MapGenerator.getTileAtPoint(checkPoint);
                    tileAtPoint.setTileType(getEntity().entityType);
                    if (checkEnemyPresent(tileAtPoint))
                    {
                        tileAtPoint.getCurrentEntity().takeDamage();
                    }
                }
                
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
