using UnityEngine;
using System.Collections.Generic;

public class ActionManager : MonoBehaviour
{ 
    public GameObject Demon_Expand_FX;
    public GameObject Janitor_Expand_FX;
    public GameObject LockUI;

    GameManager gameManager;
    int currentActionSelected;
    Entity currentEntity;
    PlayerSelectManager playerSelectManager;
    TileColorManager tileColorManager;
    UndoManager undoManager;
    MapGenerator mapGenerator;

    void Start()
    {
        playerSelectManager = GetComponent<PlayerSelectManager>();
        gameManager = GetComponent<GameManager>();
        undoManager = GetComponent<UndoManager>();
        mapGenerator = GetComponent<MapGenerator>();
        currentActionSelected = -1;
        tileColorManager = GetComponent<TileColorManager>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            onActionClicked();
        }
    }

    public void onActionClicked()
    {
        
        if (currentActionSelected >= 0)
        {
            playerSelectManager.mouseClicked();
            performAction(playerSelectManager.currentTileSelected);
        }
        else if (gameManager.getIsHumanPlayer())
        {
            playerSelectManager.mouseClicked();
            currentEntity = playerSelectManager.currentCharacterSelected;
        }
    }

    public void actionSelected(int actionID)
    {
        //print(playerSelectManager.currentCharacterSelected);
        
        currentActionSelected = actionID;
        Actions action = currentEntity.GetComponent<EntityActionManager>().actions[actionID];
        List<Point2> validTiles = action.getValidMoves(currentEntity.getCurrentLocation(), mapGenerator);
        if (validTiles.Count > 0)
        {
            tileColorManager.colorValidSquares(validTiles);
        }
        else
        {
            if (action is ExpandAction)
            { 
                performAction(mapGenerator.getTile(0, 0));
            }
            currentActionSelected = -1;

        }



    }

    public void performAction(Tile tileSelected)
    {
        if (currentActionSelected < 0)
        {
            return;
        }
        Actions action = currentEntity.GetComponent<EntityActionManager>().actions[currentActionSelected];
        tileColorManager.resetValidSquares();
        if (tileSelected != null && currentActionSelected >= 0)
        {
            //playerSelectManager.mouseClicked();
            //undoManager.setGameState();
            //print(action.performAction(tileSelected.getLocation(), mapGenerator));
            if (action.performAction(tileSelected.getLocation(), mapGenerator))
            {
                action.performAnimations();
                gameManager.performAction();
                MapGenerator.updateTileScore();
                /*undoManager.saveGameState();
                undoManager.finishTurn();
                gameManager.performAction();
                MapGenerator.updateTileScore();*/
            }
        }
        else
        {
            undoManager.resetCurrentTurnInfo();
        }
        currentActionSelected = -1;


    }

    public void setCurrentEntity(Entity currentEntity)
    {
        this.currentEntity = currentEntity;
    }

}
