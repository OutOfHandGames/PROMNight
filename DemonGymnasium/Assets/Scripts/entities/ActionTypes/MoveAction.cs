﻿using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class MoveAction : Actions
{
    public float movementSpeed = 5;
    bool isMoving;
    Vector3 goalLocation;

    void Update()
    {
        setAnimationBool(isMoving);
        if (!isMoving)
        {
            return;
        }

        getEntity().transform.position = Vector3.MoveTowards(getEntity().transform.position, goalLocation, Time.deltaTime * movementSpeed);
        if ((getEntity().transform.position - goalLocation).magnitude < .001f)
        {
            isMoving = false;
        }
    }


    

    public override void initializeLegalActions()
    {
        legalActions = new Point2[] { Point2.NORTH, Point2.SOUTH, Point2.EAST, Point2.WEST};
    }

    public override void OnActionClicked(ActionManager actionManager)
    {
        findValidPositions();
    }

    public override List<Point2> findValidPositions()
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
                if (tileAtPoint.getCurrentTileType() == Tile.NEUTRAL && tileAtPoint.getLocation() - direction != getEntity().getCurrentTile().getLocation())
                {
                    break;
                }
                validPositions.Add(checkPosition);

                if (tileAtPoint.getCurrentTileType() == Tile.NEUTRAL)
                {
                    break;
                }
                if (getEntity().getCurrentTile().getCurrentTileType() == Tile.NEUTRAL)//Checks if player is on a neutral tile. In which case they can't slide
                {
                    break;
                }
                checkPosition += direction;
                //print(checkOutOfBoundsPoint(checkPosition));
            }
        }
        setActive(true);
        return validPositions;
    }

    public override bool performAction(Tile tileClicked, UndoManager undoManager)
    {
        foreach (Point2 p in validPositions)
        {
            if (tileClicked.getLocation() == p)
            {
                undoManager.addAffectedTile(getEntity().getCurrentTile());
                undoManager.addAffectedTile(tileClicked);
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
