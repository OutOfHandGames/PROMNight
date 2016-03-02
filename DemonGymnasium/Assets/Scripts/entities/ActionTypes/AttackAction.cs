using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class AttackAction : Actions
{
    public override List<Point2> getAffectedTiles(Point2 origin, Point2 effectedTile, MapProperties mapProperties)
    {
        throw new NotImplementedException();
    }

    public override List<Point2> getValidMoves(Point2 origin, MapProperties mapProperties)
    {
        throw new NotImplementedException();
    }

    public override void onActionClicked(MapProperties mapProperties)
    {
        throw new NotImplementedException();
    }

    public override bool performAction(Point2 tilePoint, MapProperties mapProperties)
    {
        throw new NotImplementedException();
    }

    public override void performAnimations()
    {
        throw new NotImplementedException();
    }
}
