using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ActionPanel_Button : MonoBehaviour,  IPointerEnterHandler, IPointerUpHandler, IPointerExitHandler, IPointerClickHandler{

    public ActionPanel ap;
    public string buttonName;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("On Pointer Enter event");
        if (ap.selecting)
        {
            if (buttonName.Equals("Shoot"))
            {
                ap.ShootHovered();
            }
            else if (buttonName.Equals("Move"))
            {
                ap.MoveHovered();
            }
            else if (buttonName.Equals("Expand"))
            {
                ap.ExpandHovered();
            }
            else if (buttonName.Equals("Cancel"))
            {
                ap.CancelHovered();
            }
        }
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("On Pointer Click event");
        if (ap.selecting)
        {
            if (buttonName.Equals("Shoot"))
            {
                ap.ShootSelected();
            }
            else if (buttonName.Equals("Move"))
            {
                ap.MoveSelected();
            }
            else if (buttonName.Equals("Expand"))
            {
                ap.ExpandSelected();
            }
            else if (buttonName.Equals("Cancel"))
            {
                ap.CancelSelected();
            }
        }
        else
        {
            Debug.Log("not selecting");
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("On Pointer Up event");
        if (ap.selecting)
        {
            if (buttonName.Equals("Shoot"))
            {
                ap.ShootSelected();
            }
            else if (buttonName.Equals("Move"))
            {
                ap.MoveSelected();
            }
            else if (buttonName.Equals("Expand"))
            {
                ap.ExpandSelected();
            }
            else if (buttonName.Equals("Cancel"))
            {
                ap.CancelSelected();
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("On Pointer Exit event");
        if (ap.selecting)
        {
            if (buttonName.Equals("Shoot"))
            {
                ap.ShootExited();
            }
            else if (buttonName.Equals("Move"))
            {
                ap.MoveExited();
            }
            else if (buttonName.Equals("Expand"))
            {
                ap.ExpandExited();
            }
            else if (buttonName.Equals("Cancel"))
            {
                ap.CancelExited();
            }
        }
    }
    

}
