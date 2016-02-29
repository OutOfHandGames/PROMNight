using UnityEngine;
using System.Collections;

public class ActionPanel : MonoBehaviour {

    public float OffsetX;
    public float OffsetY;

    public bool selecting;

    PlayerSelectManager playerSelectManager;
    ActionManager actionManager;
    Animator animator;

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
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                Reset();
            }
        }
	}

    public void ShootSelected()
    {
        Reset();
        actionManager.actionSelected(1);
    }

    public void MoveSelected()
    {
        Reset();
        actionManager.actionSelected(0);

    }
    public void ExpandSelected()
    {
        Reset();
        actionManager.actionSelected(2);

    }
    public void CancelSelected()
    {
        Reset();
        playerSelectManager.resetSelection();
    }

    public void ShootHovered()
    {
        animator.SetBool("highlight_Shoot", true);
        animator.SetBool("highlight_Move", false);
        animator.SetBool("highlight_Expand", false);
        animator.SetBool("highlight_Cancel", false);
    }

    public void MoveHovered()
    {
        animator.SetBool("highlight_Shoot", false);
        animator.SetBool("highlight_Move", true);
        animator.SetBool("highlight_Expand", false);
        animator.SetBool("highlight_Cancel", false);
    }

    public void ExpandHovered()
    {
        animator.SetBool("highlight_Shoot", false);
        animator.SetBool("highlight_Move", false);
        animator.SetBool("highlight_Expand", true);
        animator.SetBool("highlight_Cancel", false);
    }

    public void CancelHovered()
    {
        animator.SetBool("highlight_Shoot", false);
        animator.SetBool("highlight_Move", false);
        animator.SetBool("highlight_Expand", false);
        animator.SetBool("highlight_Cancel", true);
    }

    public void ShootExited()
    {
        animator.SetBool("highlight_Shoot", false);
    }

    public void MoveExited()
    {
        animator.SetBool("highlight_Move", false);
    }

    public void ExpandExited()
    {
        animator.SetBool("highlight_Expand", false);
    }

    public void CancelExited()
    {
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
        Debug.Log("Initialize");
        selecting = true;
        SetUIPos(entityTrans);
    }

    public void Reset()
    {
        selecting = false;
        animator.SetTrigger("Reset");
        Invoke("DisableGameObject", 0.5f);
    }

    public void DisableGameObject()
    {
        gameObject.SetActive(false);
    }


}
