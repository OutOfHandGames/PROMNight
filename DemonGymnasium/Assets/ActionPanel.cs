using UnityEngine;
using System.Collections;

public class ActionPanel : MonoBehaviour {

    public float OffsetX;
    public float OffsetY;

    public bool selecting;

    PlayerSelectManager playerSelectManager;
    ActionManager actionManager;
    Animator animator;

    public bool outOfShoot;
    public bool outOfExpand;
    public bool outOfMove;
    public bool outOfCancel;

    // Use this for initialization
    void Awake()
    {
        playerSelectManager = GameObject.FindObjectOfType<PlayerSelectManager>();
        actionManager = GameObject.FindObjectOfType<ActionManager>();
        animator = GetComponent<Animator>();
    }

    void Start () {
        gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (selecting)
        {
            //if (Input.GetMouseButtonUp(0))
            //{
            //    Reset();
            //}
        }

        if (playerSelectManager.isMobile)
        {

        }
        else
        {
            if (Input.GetMouseButtonDown(0) && (!outOfShoot|!outOfMove|!outOfExpand|!outOfCancel))
            {
                Reset();
            }
        }
	}

    public void ShootSelected()
    {
        actionManager.actionSelected(1);
        Debug.Log("Action selected: shoot");
        Reset();
    }

    public void MoveSelected()
    {
        Reset();
        actionManager.actionSelected(0);
        Debug.Log("Action selected: move");

    }
    public void ExpandSelected()
    {
        Reset();
        actionManager.actionSelected(2);
        Debug.Log("Action selected: expand");

    }
    public void CancelSelected()
    {
        Reset();
        playerSelectManager.resetSelection();
        Debug.Log("Action selected: cancel");
    }

    public void ShootHovered()
    {
        outOfShoot = false;
        animator.SetBool("highlight_Shoot", true);
        animator.SetBool("highlight_Move", false);
        animator.SetBool("highlight_Expand", false);
        animator.SetBool("highlight_Cancel", false);
    }

    public void MoveHovered()
    {
        outOfMove = false;
        animator.SetBool("highlight_Shoot", false);
        animator.SetBool("highlight_Move", true);
        animator.SetBool("highlight_Expand", false);
        animator.SetBool("highlight_Cancel", false);
    }

    public void ExpandHovered()
    {
        outOfExpand = false;
        animator.SetBool("highlight_Shoot", false);
        animator.SetBool("highlight_Move", false);
        animator.SetBool("highlight_Expand", true);
        animator.SetBool("highlight_Cancel", false);
    }

    public void CancelHovered()
    {
        outOfCancel = false;
        animator.SetBool("highlight_Shoot", false);
        animator.SetBool("highlight_Move", false);
        animator.SetBool("highlight_Expand", false);
        animator.SetBool("highlight_Cancel", true);
    }

    public void ShootExited()
    {
        outOfShoot = true;
        animator.SetBool("highlight_Shoot", false);
    }

    public void MoveExited()
    {
        outOfMove = true;
        animator.SetBool("highlight_Move", false);
    }

    public void ExpandExited()
    {
        outOfExpand = true;
        animator.SetBool("highlight_Expand", false);
    }

    public void CancelExited()
    {
        outOfCancel = true;
        animator.SetBool("highlight_Cancel", false);
    }

    public void SetUIPos(Transform entityTrans)
    {
        RectTransform actionRt = GetComponent<RectTransform>();

        Vector3 viewPos = Camera.main.WorldToViewportPoint(entityTrans.position);
        viewPos = new Vector3(viewPos.x, viewPos.y);
        actionRt.anchorMin = viewPos;
        actionRt.anchorMax = viewPos;
        actionRt.anchoredPosition = new Vector2(OffsetX, OffsetY);
    }

    public void Initialize(Transform entityTrans)
    {
        //Debug.Log("Initialize");
        selecting = true;
        SetUIPos(entityTrans);
    }

    public void Reset()
    {
        selecting = false;
        animator.SetTrigger("Reset");
        Invoke("DisableGameObject", 1f);
    }

    public void DisableGameObject()
    {
        gameObject.SetActive(false);
    }


}
