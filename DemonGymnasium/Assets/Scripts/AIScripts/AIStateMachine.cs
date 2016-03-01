using UnityEngine;
using System.Collections.Generic;

public class AIStateMachine : MonoBehaviour {
    public int aiTeam = Tile.DEMON;
    public int enemyTeam = Tile.JANITOR;
    public List<MoveInfo> moveInfoList = new List<MoveInfo>();

    List<Entity> currentEntityList = new List<Entity>();
    GameManager gameManger;
    ActionManager actionManager;
    MapGenerator mapGenerator;

    void Start()
    {
        gameManger = GameObject.FindObjectOfType<GameManager>();
        actionManager = GameObject.FindObjectOfType<ActionManager>();
        //mapGenerator = GetComponent<MapGenerator>();
    }

    public void setAllCurrentEntities()
    {
        currentEntityList.Clear();
        List<Entity>[] aiEntityList = gameManger.getAllEntitiesTeam(aiTeam);
        foreach (List<Entity> l in aiEntityList)
        {
            foreach (Entity e in l)
            {
                currentEntityList.Add(e);
            }
        }
    }

    public void selectRandomActions()
    {
        //moveInfoList.Clear();
        setAllCurrentEntities();
        
        
        while (gameManger.currentTurn == aiTeam)
        {
            performActions(findValidMove());
        }
        
    }

    public void selectBestAction(int depth)
    {

    }

    void performActions(MoveInfo mInfo)
    {
        actionManager.setCurrentEntity(mInfo.entity);
        print(mInfo.actionSelected);
        actionManager.actionSelected(mInfo.actionSelected);
        actionManager.performAction(MapGenerator.getTileAtPoint(mInfo.tilePositionSelected));
        
    }

    MoveInfo findValidMove()
    {
        while (true)
        {
            MoveInfo mInfo = new MoveInfo();
            mInfo.entity = currentEntityList.ToArray()[Random.Range(0, currentEntityList.Count)];
            mInfo.actionSelected = Random.Range(0, 3);
            Actions action = mInfo.entity.getEntityActionManager().actions[mInfo.actionSelected];
            List<Point2> validActions = action.getValidMoves(mInfo.entity.getCurrentTile().getLocation(), mapGenerator);
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


    public GameManager getGameManager()
    {
        return gameManger;
    }

    public ActionManager getActionManager()
    {
        return actionManager;
    }
}
