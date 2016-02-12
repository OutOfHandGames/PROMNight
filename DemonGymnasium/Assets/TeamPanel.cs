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

    public Sprite JanitorLogo;
    public Sprite DemonLogo;

    public Sprite ActionPowerText;

    public Sprite kingIcon;
    public Sprite minionIcon;

    public Sprite teamBackground;
    public Sprite actionPointJanitor;
    public Sprite actionPointDemon;

	// Use this for initialization
	void Start () {
        SetupUI();
	}
	
	// Update is called once per frame
	void Update () {
        HandleHighlight();
	}

    void SetupUI()
    {

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
