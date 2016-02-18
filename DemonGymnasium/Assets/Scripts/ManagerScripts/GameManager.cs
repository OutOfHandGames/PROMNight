using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
    public const int JANITOR = 0;
    public const int DEMON = 1;

    private const int KING = 0;
    private const int PAWN = 1;

	public int turnsCompleted;

    public int turnsPerPlayer;

    public int currentTurn;
    public LinkedList<Entity> currentPlayers;
    public bool gameInProgress = true;
    public static GameManager gameManager;

    int turnsLeft;
    int winner = -1;
    CameraManager cameraManager;
    UndoManager undoManager;
    UIManager uiManager;
    List<Entity>[] janitorEntities = new List<Entity>[2];
    List<Entity>[] demonEntities = new List<Entity>[2];



    void Start()
    {
        initializeEntityLists();
		turnsCompleted = 0;
        gameManager = this;
        currentPlayers = new LinkedList<Entity>();
        currentTurn = JANITOR;
        cameraManager = GetComponent<CameraManager>();
        undoManager = GetComponent<UndoManager>();
        uiManager = GameObject.FindObjectOfType<UIManager>();
        turnsLeft = turnsPerPlayer;
        updateEntitiesPresent();
    }

    void initializeEntityLists()
    {
        for (int i = 0; i < janitorEntities.Length; i++)
        {
            janitorEntities[i] = new List<Entity>();
            demonEntities[i] = new List<Entity>();
        }
    }

    public void clearEntityLists()
    {
        for (int i = 0; i < janitorEntities.Length; i++)
        {
            janitorEntities[i].Clear();
            demonEntities[i].Clear();
        }
    }

    public void updateEntitiesPresent()
    {
        Entity[] allEntities = GameObject.FindObjectsOfType<Entity>();
        clearEntityLists();

        foreach (Entity e in allEntities)
        {
            switch (e.entityType)
            {
                case JANITOR:
                    if (e is King)
                    {
                        janitorEntities[KING].Add(e);
                    }
                    else
                    {
                        janitorEntities[PAWN].Add(e);
                    }
                    break;
                case DEMON:
                    if (e is King)
                    {
                        demonEntities[KING].Add(e);
                    }
                    else
                    {
                        demonEntities[PAWN].Add(e);
                    }
                    break;
            }
        }
    }

    public int getKingCount(int side)
    {
        switch(side)
        {
            case JANITOR:
                return janitorEntities[KING].Count;
            case DEMON:
                return demonEntities[KING].Count;
        }

        return -1;
    }

    public int getPawnCount(int side)
    {
        switch (side)
        {
            case JANITOR:
                return janitorEntities[PAWN].Count;
            case DEMON:
                return demonEntities[PAWN].Count;
        }

        return -1;
    }

    public bool gebPlayerTurn()
    {
        return currentTurn == JANITOR;
    }

    void Update()
    {
        if (MapGenerator.currentTileTypes[Tile.NEUTRAL] <= 0)
        {
            if (MapGenerator.currentTileTypes[Tile.JANITOR] > MapGenerator.currentTileTypes[Tile.DEMON]) {
                endGame(DEMON);
                
            }
            else
            {
                endGame(JANITOR);
            }
        }
    }

    public void performAction()
    {
        turnsLeft--;
        if (turnsLeft <= 0)
        {
            changeTurns();
        }
        
        updateEntitiesPresent();


       // undoManager.finishTurn();
    }

    public int getTurnsBeforeNextRound()
    {
        return turnsLeft;
    }

    public void endGame(int idLoser)
    {
        if (idLoser == JANITOR)
        {
            uiManager.GameEnds(DEMON);
        }
        uiManager.GameEnds(JANITOR);
    }

    public void setTurnsLeft(int turnsLeft)
    {
        this.turnsLeft = turnsLeft;
    }

   

    void changeTurns()
    {
		turnsCompleted++;
        turnsLeft = turnsPerPlayer;
        if (currentTurn == JANITOR)
        {
            currentTurn = DEMON;
        }
        else
        {
            currentTurn = JANITOR;
        }
        cameraManager.shiftCameraDelay(currentTurn);
    }


	void checkLocks(){
		GameObject[] locks = GameObject.FindGameObjectsWithTag ("Lock");
		for(int i = 0; i < locks.Length; i ++){
			if (locks [i].GetComponent<LockUI>().turnsTilDestroyed <= 0) {
				locks [i].GetComponent<LockUI>().DestroySelf ();
			} else {
				locks [i].GetComponent<LockUI>().turnsTilDestroyed--;
			}
		}
	}
}
