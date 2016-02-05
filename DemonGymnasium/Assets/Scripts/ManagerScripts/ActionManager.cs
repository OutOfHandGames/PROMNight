using UnityEngine;
using System.Collections;

public class ActionManager : MonoBehaviour
{ 
    public GameObject Demon_Expand_FX;
    public GameObject Janitor_Expand_FX;
    public GameObject LockUI;
    int currentActionSelected;
    Entity currentEntity;
    PlayerSelectManager playerSelectManager;
    TileColorManager tileColorManager;

    void Start()
    {
        playerSelectManager = GetComponent<PlayerSelectManager>();
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
            performAction();
        }
    }

    public void actionSelected(int actionID)
    {
        //print(playerSelectManager.currentCharacterSelected);
        Actions action = playerSelectManager.currentCharacterSelected.GetComponent<EntityActionManager>().actions[actionID];
        action.OnActionClicked();
        if (action.getActive())
        {
            currentActionSelected = actionID;
            tileColorManager.colorValidSquares(action.getValidPosition());
            currentEntity = playerSelectManager.currentCharacterSelected;

        }
        else
        {
            currentActionSelected = -1;
        }
    }

    public void performAction()
    {
        Actions action = currentEntity.GetComponent<EntityActionManager>().actions[currentActionSelected];

        tileColorManager.resetValidSquares(action.getValidPosition());
        if (currentActionSelected >= 0 && action.getActive())
        {
            playerSelectManager.mouseClicked();
            action.performAction(playerSelectManager.currentTileSelected);
        }
        else
        {
            currentActionSelected = -1;
        }

    }

}
