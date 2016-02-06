﻿using UnityEngine;
using System.Collections;

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

    void Start()
    {
        playerSelectManager = GetComponent<PlayerSelectManager>();
        gameManager = GetComponent<GameManager>();
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
    }

    public void actionSelected(int actionID)
    {
        //print(playerSelectManager.currentCharacterSelected);
        Actions action = playerSelectManager.currentCharacterSelected.GetComponent<EntityActionManager>().actions[actionID];
        
        currentActionSelected = actionID;
        tileColorManager.colorValidSquares(action.getValidPosition());
        currentEntity = playerSelectManager.currentCharacterSelected;
        action.OnActionClicked(this);



    }

    public void performAction(Tile tileSelected)
    {
        print(currentActionSelected);
        Actions action = currentEntity.GetComponent<EntityActionManager>().actions[currentActionSelected];

        tileColorManager.resetValidSquares(action.getValidPosition());
        if (currentActionSelected >= 0 && action.getActive())
        {
            //playerSelectManager.mouseClicked();
            if (action.performAction(tileSelected))
            {
                gameManager.performAction();
                MapGenerator.updateTileScore();
            }
        }
        else
        {
            currentActionSelected = -1;
        }

    }

}
