using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TeamPanel : MonoBehaviour {

    public GameManager gm;

    public Outline outline;

    public int teamNum;

    public Image TeamLogoImage;
    public Image KingIconImage;
    public Image MinionIconImage;
    public Text APText;
    public Image[] actionPoints = new Image[4];

	public Text kingNum;
	public Text minionNum;



    public Sprite JanitorLogo;
    public Sprite DemonLogo;

    public Sprite ActionPowerText;

    public Sprite kingIcon;
    public Sprite janitorIcon;

	public Sprite demonKingIcon;
	public Sprite demonIcon;

    public Sprite teamBackground;
    public Sprite actionPointJanitor;
    public Sprite actionPointDemon;

	// Use this for initialization
	void Start () {
		SetupUI(teamNum);
	}
	
	// Update is called once per frame
	void Update () {
        HandleHighlight();
        HandleEntityNumbers();
    }


	/// <summary>
	/// Updates the number of minions and kings by turn. Call this method when a turn ends
	/// </summary>
	void UpdateByTurn(int minionNum, int kingNum, int teamNum){
		if(this.teamNum == teamNum){
			this.minionNum.text = "x" + minionNum;
			this.kingNum.text = "x" + kingNum;
		}
	}

	void SetupUI(int teamID)
    {
		if (teamID == 0) {
			TeamLogoImage.sprite = JanitorLogo;
			KingIconImage.sprite = kingIcon;
			MinionIconImage.sprite = janitorIcon;
			APText.color = Color.blue;
			for(int i = 0; i < actionPoints.Length; i++){
				actionPoints [i].sprite = actionPointJanitor;
			}
		}
		else {
			TeamLogoImage.sprite = DemonLogo;
			KingIconImage.sprite = demonKingIcon;
			MinionIconImage.sprite = demonIcon;
			APText.color = Color.red;
			for(int i = 0; i < actionPoints.Length; i++){
				actionPoints [i].sprite = actionPointDemon;
			}
		}
			
    }

    void HandleEntityNumbers()
    {
        kingNum.text = "*" + GameManager.getKingCount(teamNum).ToString();
        minionNum.text = "*" + GameManager.getPawnCount(teamNum).ToString();
    }

    void HandleHighlight()
    {
        if (gm.currentTurn == teamNum)
        {
            outline.enabled = true;
        }
        else
        {
            outline.enabled = false;
        }
    }

    //This method is only called after an action is taken
    void HandleActionPoint()
    {
        int currentActionPoint = gm.turnsPerPlayer - gm.turnsCompleted;
        if (currentActionPoint > 4)
        {
            actionPoints[0].enabled = true;
            actionPoints[1].enabled = false;
            actionPoints[2].enabled = false;
            actionPoints[3].enabled = false;
        }
        else
        {
            for (int i = 0; i < currentActionPoint; i++)
            {
                actionPoints[i].enabled = true;
            }
        }
    }
}
