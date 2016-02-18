using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class AttackAction : Actions
{
    public GameObject projectile;
    public int attackRange = 2;

    public override void initializeLegalActions()
    {
        legalActions = new Point2[] { Point2.NORTH, Point2.SOUTH, Point2.EAST, Point2.WEST };

    }


    public override void OnActionClicked(ActionManager actionManager)
    {
        findValidPositions(getEntity().getCurrentTile().getLocation());
    }

    public override List<Point2> getAffectedTiles()
    {
        return null;
    }

    public override List<Point2> findValidPositions(Point2 origin)
    {
        validPositions.Clear();
        setActive(true);
        foreach (Point2 p in legalActions)
        {
            Point2 checkPoint = origin;
            for (int i = 0; i < attackRange; i++)
            {
                checkPoint += p;
                if (checkOutOfBoundsPoint(checkPoint))
                {
                    break;
                }
                Tile tileAtPoint = MapGenerator.getTileAtPoint(checkPoint);
                if (checkObstaclePresent(tileAtPoint))
                {
                    break;
                }
                if (checkFriendlyPresent(tileAtPoint))
                {
                    break;
                }
                validPositions.Add(checkPoint);
            }
        }
        return validPositions;
    }

    public override bool performAction(Tile tileClicked, UndoManager undoManager)
    {
        Point2 clickPoint = tileClicked.getLocation();
        Point2 origin = getEntity().getCurrentTile().getLocation();
        setActive(false);

        foreach (Point2 p in validPositions)
        {
            //print("P" + p + "   click" + clickPoint );
            if (p == clickPoint)
            {
                Point2 direction = p - origin;
                direction.normalizeValues();
                Point2 checkPoint = origin;
                GameObject obj = (GameObject)Instantiate(projectile, new Vector3(origin.x, 0, origin.y), new Quaternion());
                obj.GetComponent<Projectile>().setGoalPosition(tileClicked.transform.position);

                for (int i = 0; i < attackRange; i++)
                {
                    checkPoint += direction;
                    if (checkOutOfBoundsPoint(checkPoint))
                    {
                        break;
                    }
                    Tile tileAtPoint = MapGenerator.getTileAtPoint(checkPoint);
                    undoManager.addAffectedTile(tileAtPoint);
                    tileAtPoint.setTileType(getEntity().entityType);
                    if (checkEnemyPresent(tileAtPoint))
                    {
                        tileAtPoint.getCurrentEntity().takeDamage();
                    }
                }
                setAnimationTrigger();
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
