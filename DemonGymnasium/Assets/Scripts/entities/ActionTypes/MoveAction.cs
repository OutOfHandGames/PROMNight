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
        legalActions = new Point2[] { Point2.NORTH, Point2.SOUTH, Point2.EAST, Point2.WEST};
    }


    public override Point2[] getLegalActions()
    {
        throw new NotImplementedException();
    }

    public override void OnActionClicked()
    {
        throw new NotImplementedException();
    }

    public override void performAction(Tile tileClicked)
    {
        throw new NotImplementedException();
    }
}
