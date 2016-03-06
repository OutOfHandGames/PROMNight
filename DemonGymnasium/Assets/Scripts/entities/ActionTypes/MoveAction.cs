using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class MoveAction : Actions
{
    bool isMoving = false;
    float speed = 2;
    Vector3 goalLocation;
    Animator anim;

    void Start()
    {
        Point2 eLocation = getEntity().getCurrentLocation();
        goalLocation = new Vector3(eLocation.x, 0, eLocation.y);
        anim = getEntity().GetComponentInChildren<Animator>();
    }

    void Update()
    {
        anim.SetBool("Movement", isMoving);
        if (isMoving)
        {

            if ((goalLocation - getEntity().transform.position).magnitude < .001f)
            {
                isMoving = false;
            }
            else
            {
                getEntity().transform.position = Vector3.MoveTowards(getEntity().transform.position, goalLocation, Time.deltaTime * speed);
            }
        }
    }

    public override List<Point2> getAffectedTiles(Point2 origin, Point2 affectedTile, MapProperties mapProperties)
    {
        List<Point2> affectedTiles = new List<Point2>();
        affectedTiles.Add(origin);
        affectedTiles.Add(affectedTile);
        return affectedTiles;
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
        isMoving = true;
        goalLocation = new Vector3(getEntity().getCurrentLocation().x, 0, getEntity().getCurrentLocation().y);
    }
}
