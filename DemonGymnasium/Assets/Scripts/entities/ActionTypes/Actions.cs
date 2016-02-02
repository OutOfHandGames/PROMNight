using UnityEngine;
using System.Collections;

public abstract class Actions : MonoBehaviour
{
    public int turnCost = 1;
    public string actionName = "Action";
    Entity entity;

    public abstract void performAction();

    public void setEntity(Entity entitySelected)
    {
        this.entity = entitySelected;
    }


    public abstract void OnActionClicked();

    public abstract Vector2[] legalActions();

    
}
