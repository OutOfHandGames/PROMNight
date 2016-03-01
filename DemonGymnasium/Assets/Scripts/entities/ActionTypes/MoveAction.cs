using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class MoveAction : Actions
{
    public float speed = 10;
    bool isMoving;
    Vector3 goalLocation;

    void Start()
    {
        goalLocation = getEntity().transform.position;
    }

    void Update()
    {
        if (isMoving)
        {
            getEntity().transform.position = Vector3.MoveTowards(getEntity().transform.position, goalLocation, Time.deltaTime * speed);
            if ((getEntity().transform.position - goalLocation).magnitude < .001f)
            {
                isMoving = false;
            }
        }
    }

    public override List<Point2> getAffectedTiles(Point2 origin, Point2 effectedTile, MapProperties mapProperties)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// This method should always be called before perform action. That is just a thing. If something goes wrong, make sure you checked this
    /// first.
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="mapProperties"></param>
    /// <returns></returns>
    public override List<Point2> getValidMoves(Point2 origin, MapProperties mapProperties)
    {
        validPoints.Clear();
        foreach (Point2 dir in legalMoves)
        {
            Point2 checkPoint = origin + dir;
            while (!checkPointOutOfBounds(checkPoint) && checkTileOwned(checkPoint, mapProperties) && !checkTileContainsEntity(checkPoint, mapProperties))
            {
                validPoints.Add(checkPoint);
                checkPoint += dir;
            }
        }
        return validPoints;
    }


    public override void performAction(Point2 tilePoint, MapProperties mapProperties, bool makeAction = true)
    {
        foreach(Point2 p in validPoints)
        {
            if (p == tilePoint)
            {
                //List<Point2> affectedTiles = getAffectedTiles(getEntity().getCurrentTile().getLocation(), p)
                isMoving = true;
                goalLocation = new Vector3(p.x, 0, p.y);
            }
        }
    }
}
