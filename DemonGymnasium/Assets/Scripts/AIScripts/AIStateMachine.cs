using UnityEngine;
using System.Collections.Generic;

public class AIStateMachine : MonoBehaviour {
    public int aiTeam = Tile.DEMON;
    public List<MoveInfo> moveInfoList = new List<MoveInfo>();

    List<Entity> currentEntityList = new List<Entity>();
    GameManager gameManger;

    void Start()
    {
        gameManger = GameObject.FindObjectOfType<GameManager>();
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

        MoveInfo mInfo = null;
        for (int i = 0; i < gameManger.turnsPerPlayer; i++)
        {
            mInfo = new MoveInfo();
            mInfo.entity = currentEntityList.ToArray()[Random.Range(0, currentEntityList.Count)];

        }
        
    }

}
