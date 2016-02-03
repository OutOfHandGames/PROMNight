using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Entity))]

public class EntityActionManager : MonoBehaviour {

    public Actions[] actions = new Actions[3];

    void Start()
    {
        Entity entity = GetComponent<Entity>();
        foreach(Actions a in actions)
        {
            a.setEntity(entity);
            a.initializeLegalActions();
        }
    }
    
}
