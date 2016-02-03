using UnityEngine;
using System.Collections.Generic;


public abstract class Actions : MonoBehaviour
{
    public int turnCost = 1;
    public string actionName = "Action";
    public Point2[] legalActions;
    protected LinkedList<Point2> validPositions = new LinkedList<Point2>();//All valid options that the player can click on
    bool actionActive;
    Entity entity;

    public abstract bool performAction(Tile tileClicked);

    public abstract void initializeLegalActions();

    public void setEntity(Entity entitySelected)
    {
        this.entity = entitySelected;
    }

    public Entity getEntity()
    {
        return entity;
    }

    public void setActive(bool active)
    {
        this.actionActive = active;
    }

    public bool getActive()
    {
        return this.actionActive;
    }

     //public abstract void setValidPositions();

    public LinkedList<Point2> getValidPosition()
    {
        return validPositions;
    }
    /**
    Returns true if the tile is out of bounds of the map.
    */
    public bool checkOutOfBoundsPoint(Point2 p)
    {
        Point2 checkPosition = p;
        return checkPosition.x < 0 || checkPosition.y < 0 || checkPosition.x >= MapGenerator.BoardWidth || checkPosition.y > MapGenerator.BoardHeight;
    }

    public bool checkOutOfBoundsPoint(Point2 origin, Point2 offset)
    {
        return checkOutOfBoundsPoint(origin + offset);
    }

    public bool checkEnemyPresent(Tile tile)
    {
        if (tile.getCurrentEntity() == null)
        {
            return false;
        }
        return entity.entityType != tile.getCurrentEntity().entityType;
    }

    public abstract void OnActionClicked();

    
}
