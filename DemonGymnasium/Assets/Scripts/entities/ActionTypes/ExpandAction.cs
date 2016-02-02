using UnityEngine;
using System.Collections;
using System;

public class ExpandAction : Actions {
    

    void Start()
    {
        legalActions = new Point2[]{ Point2.NORTH, Point2.SOUTH, Point2.EAST, Point2.WEST };
    }


    public override Point2[] getLegalActions()
    {
        return legalActions;
    }

    public override void OnActionClicked()
    {
        performAction();
        
    }

    public override void performAction()
    {

        foreach (Point2 p in legalActions)
        {

        }
    }
}
