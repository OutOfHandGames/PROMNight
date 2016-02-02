using UnityEngine;
using System.Collections;

public class ActionManager : MonoBehaviour
{
    public const int MOVING = 0;
    public const int SHOOT = 1;
    public const int EXPAND = 2;

    public GameObject holyWater;
    public GameObject fireBall;

    public GameObject Demon_Expand_FX;
    public GameObject Janitor_Expand_FX;
    public GameObject LockUI;
    int currentActionSelected;
    Entity currentEntity;
    PlayerSelectManager playerSelectManager;

    void Start()
    {
        currentActionSelected = -1;
        playerSelectManager = GetComponent<PlayerSelectManager>();
    }


    public void sestActionType(int actionType)
    {
        this.currentActionSelected = actionType;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && currentActionSelected != -1)
        {
            playerSelectManager.mouseClicked();
            bool success = false;
            if (currentActionSelected == SHOOT)
            {
                success = handleAttackLogic();
                if (success)
                {
                    shootProjectile();
                }
            }

            if (currentActionSelected == MOVING)
            {
                success = handleMovementLogic();
            }

            if (success)
            {
                GameManager.gameManager.performAction();
            }
        }

    }

    void shootProjectile()
    {
        if (currentEntity.entityType == Tile.JANITOR)
        {
            Projectile pro = ((GameObject)Instantiate(holyWater, currentEntity.transform.position, new Quaternion())).GetComponent<Projectile>();
            pro.setGoalPosition(playerSelectManager.currentTileSelected.transform.position);
        }
        else {
            Projectile pro = ((GameObject)Instantiate(fireBall, currentEntity.transform.position, new Quaternion())).GetComponent<Projectile>();
            pro.setGoalPosition(playerSelectManager.currentTileSelected.transform.position);
        }
    }

    public void selectMovement(Entity entity)
    {
        currentEntity = entity;
        currentActionSelected = MOVING;
    }

    public void selectAttack(Entity entity)
    {
        currentEntity = entity;
        currentActionSelected = SHOOT;
    }

    public void selectExpand(Entity entity)
    {
        currentEntity = entity;
        currentActionSelected = EXPAND;

        handleExpandLogic();
        GameManager.gameManager.performAction();
    }

    public bool handleMovementLogic()
    {
        currentActionSelected = -1;
        bool obstructed = false;
        int type = (GameManager.gameManager.getPlayerTurn() ? 0 : 1);
        Tile goalTile = playerSelectManager.currentTileSelected;
        int sourceX = currentEntity.getCurrentTile().getX();
        int sourceY = currentEntity.getCurrentTile().getY();
        int x = goalTile.getX();
        int y = goalTile.getY();
        bool isKing = (currentEntity.GetType() == typeof(King));
        if (sourceX == x && sourceY < y)
        {
            for (int i = 1; i <= y - sourceY; i++)
            {
                Tile tile = MapGenerator.mapTiles[x, sourceY + i];
                if (tile.getIsObstructed() || (!(isKing && i == 1) && tile.getCurrentTileType() != type))
                {
                    obstructed = true;
                }
            }

            if (!obstructed)
            {
                currentEntity.moveNorth(y - sourceY);
            }
        }
        else if (sourceX == x && sourceY > y)
        {
            for (int i = 0; i < sourceY - y; i++)
            {
                Tile tile = MapGenerator.mapTiles[x, y + i];
                if (tile.getIsObstructed() || (!(isKing && i == sourceY - y - 1) && tile.getCurrentTileType() != type))
                {
                    obstructed = true;
                }
            }

            if (!obstructed)
            {
                currentEntity.moveSouth(sourceY - y);
            }
        }
        else if (sourceY == y && sourceX < x)
        {
            for (int i = 1; i <= x - sourceX; i++)
            {
                Tile tile = MapGenerator.mapTiles[sourceX + i, y];
                if (tile.getIsObstructed() || (!(isKing && i == 1) && tile.getCurrentTileType() != type))
                {
                    obstructed = true;
                }
            }

            if (!obstructed)
            {
                currentEntity.moveEast(x - sourceX);
            }
        }
        else if (sourceY == y && sourceX > x)
        {
            for (int i = 0; i < sourceX - x; i++)
            {
                Tile tile = MapGenerator.mapTiles[x + i, y];
                if (tile.getIsObstructed() || (!(isKing && i == sourceX - x - 1) && tile.getCurrentTileType() != type))
                {
                    obstructed = true;
                }
            }

            if (!obstructed)
            {
                currentEntity.moveWest(sourceX - x);
            }
        }
        else if (currentEntity.GetType() == typeof(King) && Mathf.Abs(sourceX - x) == Mathf.Abs(sourceY - y))
        {
            King king = (King)currentEntity;
            if (sourceY > y && sourceX > x)
            {
                for (int i = 0; i < sourceX - x; i++)
                {
                    Tile tile = MapGenerator.mapTiles[x + i, y + i];
                    if (tile.getIsObstructed() || (i != sourceX - x - 1 && tile.getCurrentTileType() != type))
                    {
                        obstructed = true;
                    }
                }

                if (!obstructed)
                {
                    king.moveSouthWest(sourceX - x);
                }
            }
            else if (sourceY < y && sourceX > x)
            {
                for (int i = 0; i < sourceX - x; i++)
                {
                    Tile tile = MapGenerator.mapTiles[x + i, y - i];
                    if (tile.getIsObstructed() || (i != sourceX - x - 1 && tile.getCurrentTileType() != type))
                    {
                        obstructed = true;
                    }
                }

                if (!obstructed)
                {
                    king.moveNorthWest(sourceX - x);
                }
            }
            else if (sourceY > y && sourceX < x)
            {
                for (int i = 1; i <= x - sourceX; i++)
                {
                    Tile tile = MapGenerator.mapTiles[sourceX + i, sourceY - i];
                    if (tile.getIsObstructed() || (i != x - sourceX && tile.getCurrentTileType() != type))
                    {
                        obstructed = true;
                    }
                }

                if (!obstructed)
                {
                    king.moveSouthEast(x - sourceX);
                }
            }
            else if (sourceY < y && sourceX < x)
            {
                for (int i = 1; i <= x - sourceX; i++)
                {
                    Tile tile = MapGenerator.mapTiles[sourceX + i, sourceY + i];
                    if (tile.getIsObstructed() || (i != x - sourceX && tile.getCurrentTileType() != type))
                    {
                        obstructed = true;
                    }
                }

                if (!obstructed)
                {
                    king.moveNorthEast(x - sourceX);
                }
            }
        }
        else
        {
            obstructed = true;
        }

        if (!obstructed)
        {
            currentEntity.getCurrentTile().setEntity(null);
            currentEntity.setCurrentTile(goalTile);
            goalTile.setEntity(currentEntity);
        }

        return !obstructed;
    }


    public bool handleAttackLogic()
    {
        currentActionSelected = -1;
        if (playerSelectManager.currentTileSelected == null)
        {
            return false;
        }

        Tile tile = playerSelectManager.currentTileSelected;
        Tile playerTile = currentEntity.getCurrentTile();
        int sourceX = playerTile.getX();
        int sourceY = playerTile.getY();
        int x = tile.getX();
        int y = tile.getY();
        if (checkLineofSight(playerTile.getX(), playerTile.getY(), tile.getX(), tile.getY()))
        {
            if (tile.getCurrentEntity() != null && tile.getCurrentEntity().GetType() != typeof(Obstacle))
            {
                damageIfEnemy(tile.getCurrentEntity());
            }

            int type = (GameManager.gameManager.getPlayerTurn() ? 0 : 1);

            if (sourceX == x && sourceY < y)
            {
                for (int i = 1; i <= y - sourceY; i++)
                {
                    MapGenerator.mapTiles[x, sourceY + i].setTileType(type);
                    if (currentEntity.entityType != MapGenerator.mapTiles[x, sourceY + i].currentTileType)
                    {
                        MapGenerator.mapTiles[x, sourceY + i].locked = true;
                        GameObject lockUI = (GameObject)Instantiate(LockUI);
                        lockUI.GetComponent<LockUI>().Setup(currentEntity.transform);
                    }
                }
                return true;
            }
            else if (sourceX == x && sourceY > y)
            {
                for (int i = 0; i < sourceY - y; i++)
                {
                    MapGenerator.mapTiles[x, y + i].setTileType(type);
                    if (currentEntity.entityType != MapGenerator.mapTiles[x, y + i].currentTileType)
                    {
                        MapGenerator.mapTiles[x, y + i].locked = true;
                        GameObject lockUI = (GameObject)Instantiate(LockUI);
                        lockUI.GetComponent<LockUI>().Setup(currentEntity.transform);
                    }
                }
                return true;

            }
            else if (sourceY == y && sourceX < x)
            {
                for (int i = 1; i <= x - sourceX; i++)
                {
                    MapGenerator.mapTiles[sourceX + i, y].setTileType(type);
                    if (currentEntity.entityType != MapGenerator.mapTiles[sourceX + i, y].currentTileType)
                    {
                        MapGenerator.mapTiles[sourceX + i, y].locked = true;
                        GameObject lockUI = (GameObject)Instantiate(LockUI);
                        lockUI.GetComponent<LockUI>().Setup(currentEntity.transform);
                    }
                }
                return true;

            }
            else if (sourceY == y && sourceX > x)
            {
                for (int i = 0; i < sourceX - x; i++)
                {
                    MapGenerator.mapTiles[x + i, y].setTileType(type);
                    if (currentEntity.entityType != MapGenerator.mapTiles[x + i, y].currentTileType)
                    {
                        MapGenerator.mapTiles[x + i, y].locked = true;
                        GameObject lockUI = (GameObject)Instantiate(LockUI);
                        lockUI.GetComponent<LockUI>().Setup(currentEntity.transform);
                    }
                }
                return true;

            }

        }
        return false;
    }

    private void damageIfEnemy(Entity other)
    {
        if (other != null && other.getIsPlayer() != GameManager.gameManager.getPlayerTurn())
        {
            other.takeDamage();
        }
    }

    public void handleExpandLogic()
    {
        //0 is janitor; 1 is demon
        GameObject newFX;
        if (currentEntity.entityType == 0)
        {
            newFX = (GameObject)Instantiate(Janitor_Expand_FX);

        }
        else {
            newFX = (GameObject)Instantiate(Demon_Expand_FX);
        }
        newFX.GetComponent<AbilityFX>().SetupAndPlay(currentEntity.transform);


        Tile[,] mapTiles = MapGenerator.mapTiles;
        int x = currentEntity.getCurrentTile().getX();
        int y = currentEntity.getCurrentTile().getY();

        mapTiles[x, y].setTileType(currentEntity.entityType);

        if (currentEntity.entityType != mapTiles[x, y].currentTileType)
        {
            mapTiles[x, y].locked = true;
            GameObject lockUI = (GameObject)Instantiate(LockUI);
            lockUI.GetComponent<LockUI>().Setup(currentEntity.transform);
        }

        if (x - 1 >= 0)
        {
            mapTiles[x - 1, y].setTileType(currentEntity.entityType);

            if (currentEntity.entityType != mapTiles[x - 1, y].currentTileType)
            {
                mapTiles[x - 1, y].locked = true;
                GameObject lockUI = (GameObject)Instantiate(LockUI);
                lockUI.GetComponent<LockUI>().Setup(currentEntity.transform);
            }


            if (mapTiles[x - 1, y].getCurrentEntity() != null && currentEntity.entityType != mapTiles[x - 1, y].getCurrentEntity().entityType)
            {
                checkKillEnemy(mapTiles[x - 1, y]);
            }
        }
        if (y - 1 >= 0)
        {
            mapTiles[x, y - 1].setTileType(currentEntity.entityType);
            if (currentEntity.entityType != mapTiles[x, y - 1].currentTileType)
            {
                mapTiles[x, y - 1].locked = true;
                GameObject lockUI = (GameObject)Instantiate(LockUI);
                lockUI.GetComponent<LockUI>().Setup(currentEntity.transform);
            }

            if (mapTiles[x, y - 1].getCurrentEntity() != null && currentEntity.entityType != mapTiles[x, y - 1].getCurrentEntity().entityType)
            {
                checkKillEnemy(mapTiles[x, y - 1]);
            }
        }
        if (x + 1 < mapTiles.GetLength(0))
        {
            mapTiles[x + 1, y].setTileType(currentEntity.entityType);

            if (currentEntity.entityType != mapTiles[x + 1, y].currentTileType)
            {
                mapTiles[x + 1, y].locked = true;
                GameObject lockUI = (GameObject)Instantiate(LockUI);
                lockUI.GetComponent<LockUI>().Setup(currentEntity.transform);
            }

            if (mapTiles[x + 1, y].getCurrentEntity() != null && currentEntity.entityType != mapTiles[x + 1, y].getCurrentEntity().entityType)
            {
                checkKillEnemy(mapTiles[x + 1, y]);
            }
        }
        if (y + 1 < mapTiles.GetLength(1))
        {
            mapTiles[x, y + 1].setTileType(currentEntity.entityType);

            if (currentEntity.entityType != mapTiles[x, y + 1].currentTileType)
            {
                mapTiles[x, y + 1].locked = true;
                GameObject lockUI = (GameObject)Instantiate(LockUI);
                lockUI.GetComponent<LockUI>().Setup(currentEntity.transform);
                checkKillEnemy(mapTiles[x, y + 1]);
            }

            if (mapTiles[x, y + 1].getCurrentEntity() != null && currentEntity.entityType != mapTiles[x, y + 1].getCurrentEntity().entityType)
            {
                checkKillEnemy(mapTiles[x, y + 1]);

            }
        }


    }

    void checkKillEnemy(Tile tile)
    {
        Entity entity = tile.getCurrentEntity();
        if (entity == null)
        {
            return;
        }
        if (entity.entityType == currentEntity.entityType)
        {
            return;
        }
        if (entity.entityType == Tile.NEUTRAL)
        {
            return;
        }
        entity.takeDamage();
    }

    private bool checkLineofSight(int sourceX, int sourceY, int x, int y)
    {
        bool lineOfSight = true;
        int enemyType = (GameManager.gameManager.getPlayerTurn() ? 1 : 0);
        if (sourceX == x && sourceY < y && y - sourceY <= 2)
        {
            for (int i = 1; i <= y - sourceY; i++)
            {
                if (MapGenerator.mapTiles[x, sourceY + i].getIsObstructed() && MapGenerator.mapTiles[x, sourceY + i].getCurrentEntity().entityType != enemyType)
                {
                    lineOfSight = false;
                }
            }
        }
        else if (sourceX == x && sourceY > y && sourceY - y <= 2)
        {
            for (int i = 0; i < sourceY - y; i++)
            {
                if (MapGenerator.mapTiles[x, y + i].getIsObstructed() && MapGenerator.mapTiles[x, y + i].getCurrentEntity().entityType != enemyType)
                {
                    lineOfSight = false;
                }
            }
        }
        else if (sourceY == y && sourceX < x && x - sourceX <= 2)
        {
            for (int i = 1; i <= x - sourceX; i++)
            {
                if (MapGenerator.mapTiles[sourceX + i, y].getIsObstructed() && MapGenerator.mapTiles[sourceX + i, y].getCurrentEntity().entityType != enemyType)
                {
                    lineOfSight = false;
                }
            }
        }
        else if (sourceY == y && sourceX > x && sourceX - x <= 2)
        {
            for (int i = 0; i < sourceX - x; i++)
            {
                if (MapGenerator.mapTiles[x + i, y].getIsObstructed() && MapGenerator.mapTiles[x + i, y].getCurrentEntity().entityType != enemyType)
                {
                    lineOfSight = false;
                }
            }
        }
        else
        {
            lineOfSight = false;
        }
        return lineOfSight;
    }

}
