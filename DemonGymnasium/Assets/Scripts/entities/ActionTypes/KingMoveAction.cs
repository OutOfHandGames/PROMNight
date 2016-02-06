using UnityEngine;
using System.Collections;

public class KingMoveAction : MoveAction {

    public override void initializeLegalActions()
    {
        //legalActions = new Point2[]{ Point2.NORTH, Point2.SOUTH, Point2.EAST, Point2.WEST,
         //   Point2.NORTH + Point2.WEST, Point2.NORTH + Point2.EAST, Point2.SOUTH + Point2.EAST, Point2.SOUTH + Point2.WEST};
    }
}
