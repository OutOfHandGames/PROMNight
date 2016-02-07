using UnityEngine;
using System.Collections.Generic;


public abstract class Actions : MonoBehaviour
{
    public int turnCost = 1;
    public string actionName = "Action";
    public Point2[] legalActions;
    protected List<Point2> validPositions = new List<Point2>();//All valid options that the player can click on
    Animator anim;
    bool actionActive;
    Entity entity;

    protected virtual void Start()
    {
        anim = transform.parent.GetComponentInChildren<Animator>();
    }

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

    public abstract List<Point2> findValidPositions();

    public List<Point2> getValidPosition()
    {
        return validPositions;
    }
    /**
    Returns true if the tile is out of bounds of the map.
    */
    public bool checkOutOfBoundsPoint(Point2 p)
    {
        Point2 checkPosition = p;
        return checkPosition.x < 0 || checkPosition.y < 0 || checkPosition.x >= MapGenerator.mapTiles.GetLength(0) || checkPosition.y >= MapGenerator.mapTiles.GetLength(1);
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
        if (tile.getCurrentEntity().entityType == Tile.NEUTRAL)
        {
            return false;
        }
        return entity.entityType != tile.getCurrentEntity().entityType;
    }

    public bool checkFriendlyPresent(Tile tile)
    {
        if (tile.getCurrentEntity() == null)
        {
            return false;
        }
        return tile.getCurrentEntity().entityType == getEntity().entityType;
    }

    public bool checkObstaclePresent(Tile tile)
    {
        if (tile.getCurrentEntity() == null)
        {
            return false;
        }

        return tile.getCurrentEntity().entityType == Tile.NEUTRAL;
    }

    public bool checkTileInControl(Tile tile)
    {
        return tile.getCurrentTileType() == getEntity().entityType;
    }

    public bool checkTileControlledEnemy(Tile tile)
    {
        if (tile.getCurrentTileType() == Tile.NEUTRAL)
        {
            return false;
        }
        return tile.getCurrentTileType() != getEntity().entityType;
    }

    public abstract void OnActionClicked(ActionManager actionManager);

    public void setAnimationTrigger()
    {
        anim.SetTrigger(actionName);
    }

    public void setAnimationBool(bool isActive)
    {
        anim.SetBool(actionName, isActive);
    } 
    
}
