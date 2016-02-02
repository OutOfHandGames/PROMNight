using UnityEngine;
using System.Collections;

public abstract class Actions : MonoBehaviour
{
    public int turnCost = 1;
    public string actionName = "Action";
    public Point2[] legalActions;
    Entity entity;

    public abstract void performAction();

    public void setEntity(Entity entitySelected)
    {
        this.entity = entitySelected;
    }

    public Entity getEntity()
    {
        return entity;
    }

    public abstract void OnActionClicked();

    public abstract Point2[] getLegalActions();

    
}
