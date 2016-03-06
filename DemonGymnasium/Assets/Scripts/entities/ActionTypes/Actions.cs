using UnityEngine;
using System.Collections.Generic;


public abstract class Actions : MonoBehaviour
{
    public int turnCost = 1;
    public string actionName = "Action";
    public Point2[] legalMoves;

    protected List<Point2> validPoints = new List<Point2>();
    bool actionActive = false;
    Entity entity;


    public abstract List<Point2> getValidMoves(Point2 origin, MapProperties mapProperties);
    public abstract List<Point2> getAffectedTiles(Point2 origin, Point2 effectedTile, MapProperties mapProperties);
    public abstract bool performAction(Point2 tilePoint, MapProperties mapProperties);
    public abstract void performAnimations();
    public abstract void onActionClicked(MapProperties mapProperties);

    public bool checkTileContainsEnemy(Tile tile)
    {
        if (tile.getCurrentEntity() != null && tile.getCurrentEntity().entityType != entity.entityType)
        {
            return true;
        }
        return false;
    }

    public bool checkTileContainsObstacle(Tile tile) 
    {
        if (tile.getCurrentEntity() == null)
        {
            return false;
        }
        if (tile.getCurrentEntity().entityType == Tile.NEUTRAL)
        {
            return true;
        }
        return false;
    }

    public bool checkTileContainsFriendly(Tile tile)
    {
        if (tile.getCurrentEntity() == null)
        {
            return false;
        }
        if (tile.getCurrentEntity().entityType == entity.entityType)
        {
            return true;
        }
        return false;

    }

    public bool checkTileOwned(Tile tile)
    {
        if (tile.currentTileType == entity.entityType)
        {
            return true;
        }
        return false;
    }

    public bool checkTileNeutral(Tile tile)
    {
        return tile.currentTileType == Tile.NEUTRAL;
    }

    public bool checkPointOutOfBounds(Point2 p)
    {
        if (p.x < 0 || p.x >= MapGenerator.BoardWidth)
        {
            return true;
        }
        if (p.y < 0 || p.y >= MapGenerator.BoardHeight)
        {
            return true;
        }
        return false;
    }

    public bool checkTileContainsEntity(Tile t)
    {
        return t.getCurrentEntity() != null;
    }

    public Entity getEntity()
    {
        return entity;
    }



    public void setEntity(Entity entity)
    {
        this.entity = entity;
    }

    public bool getActionActive()
    {
        return actionActive;
    }

    public void setActionActive(bool actionActive)
    {
        this.actionActive = actionActive;
    }
}
