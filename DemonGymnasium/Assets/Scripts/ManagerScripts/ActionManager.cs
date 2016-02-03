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

    void Start()
    {
        playerSelectManager = GetComponent<PlayerSelectManager>();
        currentActionSelected = -1;
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
        Actions action = playerSelectManager.currentCharacterSelected.GetComponent<EntityActionManager>().actions[actionID];
        action.OnActionClicked();
        if (action.getActive())
        {
            currentActionSelected = actionID;
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
        print(action.name);
        action.performAction(playerSelectManager.currentTileSelected);
    }

}
