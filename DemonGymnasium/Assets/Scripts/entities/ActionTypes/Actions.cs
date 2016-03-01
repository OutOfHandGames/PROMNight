using UnityEngine;
using System.Collections.Generic;


public abstract class Actions : MonoBehaviour
{
    public int turnCost = 1;
    public string actionName = "Action";
    public Point2[] legalMoves;

    protected List<Point2> validPoints;
    Entity entity;

    public abstract List<Point2> getValidMoves(Point2 origin, MapProperties mapProperties);
    public abstract List<Point2> getAffectedTiles(Point2 origin, Point2 effectedTile, MapProperties mapProperties);
    public abstract void performAction(Point2 tilePoint, MapProperties mapProperties, bool trueAction = true);

    public bool checkTileContainsEnemy(Point2 p, MapProperties mapProperties)
    {
        Tile tileAtPoint = mapProperties.getTile(p);
        if (tileAtPoint.getCurrentEntity() != null && tileAtPoint.getCurrentEntity().entityType != entity.entityType)
        {
            return true;
        }
        return false;
    }

    public bool checkTileContainsObstacle(Point2 p, MapProperties mapProperties) 
    {
        Tile tileAtPoint = mapProperties.getTile(p);
        if (tileAtPoint.getCurrentEntity() == null)
        {
            return false;
        }
        if (tileAtPoint.getCurrentEntity().entityType == Tile.NEUTRAL)
        {
            return true;
        }
        return false;
    }

    public bool checkTileContainsFriendly(Point2 p, MapProperties mapProperties)
    {
        Tile tileAtPoint = mapProperties.getTile(p);
        if (tileAtPoint.getCurrentEntity() == null)
        {
            return false;
        }
        if (tileAtPoint.getCurrentEntity().entityType == entity.entityType)
        {
            return true;
        }
        return false;

    }

    public bool checkTileOwned(Point2 p, MapProperties mapProperties)
    {
        Tile tileAtPoint = mapProperties.getTile(p);
        if (tileAtPoint.currentTileType == entity.entityType)
        {
            return true;
        }
        return false;
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

    public bool checkTileContainsEntity(Point2 p, MapProperties mapProperties)
    {
        Tile tileAtPoint = mapProperties.getTile(p);
        return tileAtPoint.getCurrentEntity() != null;
    }

    public Entity getEntity()
    {
        return entity;
    }



    public void setEntity(Entity entity)
    {
        this.entity = entity;
    }
}
