using UnityEngine;
using System.Collections.Generic;


public abstract class Actions : MonoBehaviour
{
    public int turnCost = 1;
    public string actionName = "Action";
    public Point2[] legalMoves;

    public abstract List<Point2> getValidMoves(Point2 origin, MapGenerator mapGenerator);
    public abstract List<Point2> getAffectedTiles();
    public abstract void performAction();
}
