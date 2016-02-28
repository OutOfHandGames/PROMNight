using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class AttackAction : Actions
{
    public override void performAction()
    {
        throw new NotImplementedException();
        
    }

    public override List<Point2> getValidMoves(Point2 origin, MapGenerator mapGenerator)
    {
        List<Point2> allValidMoves = new List<Point2>();
        return allValidMoves;
    }

    public override List<Point2> getAffectedTiles()
    {
        throw new NotImplementedException();
    }


}
