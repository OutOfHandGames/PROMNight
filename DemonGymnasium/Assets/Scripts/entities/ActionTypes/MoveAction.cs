using UnityEngine;
using System.Collections;
using System;

public class MoveAction : Actions
{
    public float movementSpeed = 5;
    bool isMoving;
    Vector3 goalLocation;

    void Update()
    {
        if (!isMoving)
        {
            return;
        }

        getEntity().transform.position = Vector3.MoveTowards(getEntity().transform.position, goalLocation, Time.deltaTime * movementSpeed);
    }

    void Start()
    { 
    }

    

    public override void initializeLegalActions()
    {
        legalActions = new Point2[] { Point2.NORTH, Point2.SOUTH, Point2.EAST, Point2.WEST};
    }

    public override void OnActionClicked()
    {
        getAllValidTiles();
    }

    void getAllValidTiles()
    {
        validPositions.Clear();
        Point2 o = getEntity().getCurrentTile().getLocation();
        foreach (Point2 direction in legalActions)
        {
            Point2 checkPosition = o + direction;
            //print(checkPosition);
            while(!checkOutOfBoundsPoint(checkPosition))
            {
                Tile tileAtPoint = MapGenerator.getTileAtPoint(checkPosition);
                if (checkFriendlyPresent(tileAtPoint))
                {
                    break;
                }
                if (checkObstaclePresent(tileAtPoint))
                {
                    break;
                }
                if (checkTileControlledEnemy(tileAtPoint))
                {
                    break;
                }
                
                validPositions.AddLast(checkPosition);

                if (tileAtPoint.getCurrentTileType() == Tile.NEUTRAL)
                {
                    break;
                }
                if (getEntity().getCurrentTile().getCurrentTileType() == Tile.NEUTRAL)
                {
                    break;
                }
                checkPosition += direction;
                //print(checkOutOfBoundsPoint(checkPosition));
            }
        }
        setActive(true);
    }

    public override bool performAction(Tile tileClicked)
    {
        foreach (Point2 p in validPositions)
        {
            if (tileClicked.getLocation() == p)
            {
                goalLocation = new Vector3(p.x, 0, p.y);
                tileClicked.setEntity(getEntity());
                isMoving = true;
                setActive(false);
                return true;
            }
        }
        return false;
    }
}
