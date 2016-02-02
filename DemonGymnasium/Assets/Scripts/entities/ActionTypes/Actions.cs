using UnityEngine;
using System.Collections;

public abstract class Actions : MonoBehaviour
{
    public int turnCost = 1;
    public string actionName = "Action";
    public Point2[] legalActions;
    Entity entity;

    public abstract void performAction(Tile tileClicked);

    public void setEntity(Entity entitySelected)
    {
        this.entity = entitySelected;
    }

    public Entity getEntity()
    {
        return entity;
    }

    /**
    Returns true if the tile is out of bounds of the map.
    */
    public bool checkOutOfBoundsPoint(Point2 origin, Point2 offset)
    {
        Point2 checkPosition = origin + offset;
        return checkPosition.x < 0 || checkPosition.y < 0;
    }

    public abstract void OnActionClicked();

    public abstract Point2[] getLegalActions();

    
}
