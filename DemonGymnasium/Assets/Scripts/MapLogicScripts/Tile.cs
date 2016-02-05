﻿using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour
{
    public const int JANITOR = 0;
    public const int DEMON = 1;
    public const int NEUTRAL = 2;
    //public Color janitorColor = Color.blue;
    //public Color demonColor = Color.green;

    // GraphicTile graphicTile;
    public int currentTileType = NEUTRAL;
    Entity entityPresent;
    Point2 location;

    Renderer rend;
    public bool locked;


    public Material[] floorMaterials = new Material[5];

    void Start()
    {
        rend = GetComponentInChildren<Renderer>();
        PickRandomMaterialForNeutral();

        if (entityPresent)
        {
            currentTileType = entityPresent.entityType;
        }
        else
        {
            currentTileType = 2;
        }
        if (entityPresent == null)
        {
            setTileType(NEUTRAL);
        }
    }

    void PickRandomMaterialForNeutral()
    {
        int randomNum = (int)Random.Range(0, 100);
        int resultIndex;
        if (randomNum >= 0 && randomNum < 60)
        {
            resultIndex = 0;
        }
        else if (randomNum >= 60 && randomNum < 65)
        {
            resultIndex = 1;
        }
        else if (randomNum >= 65 && randomNum < 90)
        {
            resultIndex = 2;
        }
        else if (randomNum >= 90 && randomNum < 95)
        {
            resultIndex = 3;
        }
        else
        {
            resultIndex = 4;
        }

        rend.material = floorMaterials[resultIndex];

    }

    public bool getIsObstructed()
    {
        return entityPresent != null;
    }

    public Entity getCurrentEntity()
    {
        return this.entityPresent;
    }

    public void setEntity(Entity entity)
    {
        removeEntity();
        this.entityPresent = entity;

        if (entity != null)
        {
            entity.getCurrentTile().setEntity(null);
            this.entityPresent.setCurrentTile(this);
        }

    }

    public void setInitialEntity(Entity entity)
    {
        removeEntity();
        entity.transform.parent = this.transform.parent;
        entity.transform.position = transform.position;
        this.entityPresent = entity;
        entity.setCurrentTile(this);
        if (entity.GetType() != typeof(Obstacle))
        {
            int type = (entity.getIsPlayer() ? 0 : 1);
            // graphicTile = GetComponent<GraphicTile>();
            // graphicTile.setAnim();
            setTileType(type);
        }
    }

    public void setTileType(int tileType)
    {
        if (!locked)
        {
            //graphicTile.selectTileType(tileType);

            this.currentTileType = tileType;
            TileColorManager.resetTileColor(this);
        }

    }

    public int getCurrentTileType()
    {
        return currentTileType;
    }

    public void removeEntity()
    {
        if (entityPresent == null)
        {
            return;
        }
        this.entityPresent.transform.parent = null;
        this.entityPresent = null;
    }



    public void setLocation(int x, int y)
    {
        this.location = new Point2(x, y);
        //print(location.x + "  " + location.y);
        transform.position = new Vector3(x, 0, y);
    }

    public int getX()
    {
        return location.x;
    }

    public int getY()
    {
        return location.y;
    }

    public Point2 getLocation()
    {
        return location;
    }

    void OnMouseExit()
    {
        TileColorManager.resetTileColor(this);
    }

}
