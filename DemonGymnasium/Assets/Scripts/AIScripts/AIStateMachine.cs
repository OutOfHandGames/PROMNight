using UnityEngine;
using System.Collections.Generic;

public class AIStateMachine : MonoBehaviour {
    public int aiTeam = Tile.DEMON;
    public List<MoveInfo> moveInfoList = new List<MoveInfo>();

    List<Entity> currentEntityList = new List<Entity>();
    GameManager gameManger;
    ActionManager actionManager;

    void Start()
    {
        gameManger = GameObject.FindObjectOfType<GameManager>();
        actionManager = GameObject.FindObjectOfType<ActionManager>();
    }

    public void setAllCurrentEntities()
    {
        currentEntityList.Clear();
        Entity[] allEntities = GameObject.FindObjectsOfType<Entity>();
        foreach (Entity e in allEntities)
        {
            if (e.entityType == aiTeam)
            {
                currentEntityList.Add(e);
            }
        }
    }

    public void selectRandomActions()
    {
        moveInfoList.Clear();
        setAllCurrentEntities();
        
        
        for (int i = 0; i < gameManger.turnsPerPlayer; i++)
        {
            moveInfoList.Add(findValidMove());
        }
        performActions();
    }

    void performActions()
    {
        foreach (MoveInfo mInfo in moveInfoList)
        {
            actionManager.setCurrentEntity(mInfo.entity);
            print(mInfo.actionSelected);
            actionManager.actionSelected(mInfo.actionSelected);
            actionManager.performAction(MapGenerator.getTileAtPoint(mInfo.tilePositionSelected));
        }
    }

    MoveInfo findValidMove()
    {
        while (true)
        {
            MoveInfo mInfo = new MoveInfo();
            mInfo.entity = currentEntityList.ToArray()[Random.Range(0, currentEntityList.Count)];
            mInfo.actionSelected = Random.Range(0, 3);
            Actions action = mInfo.entity.getEntityActionManager().actions[mInfo.actionSelected];
            List<Point2> validActions = action.findValidPositions(mInfo.entity.getCurrentTile().getLocation());
            if (validActions.Count > 1)
            {
                mInfo.tilePositionSelected = validActions.ToArray()[Random.Range(0, validActions.Count)];
                return mInfo;
            }
            if (validActions.Count == 1 && validActions.ToArray()[0] != mInfo.entity.getCurrentTile().getLocation())
            {
                mInfo.tilePositionSelected = validActions.ToArray()[0];
                return mInfo;
            }
        }
        
    }

}
