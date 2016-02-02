using UnityEngine;
using System.Collections;

public abstract class Entity : MonoBehaviour {
    /**
	 * Directions!!
	 * North is Z+
	 * South is Z-
	 * East is x+
	 * West is x-
	 * 
	 * Tile size is 1
	 */

    public int entityType;
	private bool isMoving;//true while the entity is moving from one space to another
	private Vector3 target;//the target space the entity is moving to
	private Tile currentTile;//the current tile this entity is on
	private bool hasPerformedAction;//true when this monster has attacked this turn
	public bool isPlayer;//true- player, false- monster

	// Use this for initialization
	public void Start () {
		isMoving = false;
		target = Vector3.zero;
		hasPerformedAction = false;
	}
	
	// Update is called once per frame
	public void Update () {
		//this.transform.eulerAngles = GameManager.manager.getSpriteRotation();

		if (isMoving) {
			this.transform.position += (target - this.transform.position).normalized * 0.1f;

			//check target reached
			if ((target - this.transform.position).magnitude < 0.1) {
				isMoving = false;
			}
		}
	}

    public bool getIsPlayer()
    {
        return isPlayer;
    }

    public void setIsPlayer(bool isPlayer)
    {
        this.isPlayer = isPlayer;
    }

    public virtual void takeDamage()
    {
        //TODO death animation

        GameObject.Destroy(this.gameObject);
    }

	/**
	 * resets entity state at end of turn
	 */
	public void reset() {
		hasPerformedAction = false;
	}

	/**
	 * Stub for primary actions of entities, should be called via base.act of overriding method in subclass
	 */
	public void act () {
		hasPerformedAction = true;
	}

	public bool getHasActed() {
		return hasPerformedAction;
	}

	public void setCurrentTile(Tile tile) {
		currentTile = tile;
	}

	public Tile getCurrentTile() {
		return currentTile;
	}

	protected bool move(Vector3 dir, int units) {
		if (isMoving) {
			return false;
		}

		isMoving = true;
		target = this.transform.position + dir * units;
		return true;
	}

	public bool moveNorth (int units) {
		return move(new Vector3 (0, 0, 1), units);
	}

	public bool moveSouth (int units) {
		return move(new Vector3 (0, 0, -1), units);
	}

	public bool moveEast (int units) {
		return move(new Vector3 (1, 0, 0), units);
	}

	public bool moveWest (int units) {
		return move(new Vector3 (-1, 0, 0), units);
	}

    public bool getIsMoving()
    {
        return isMoving;
    }
}
