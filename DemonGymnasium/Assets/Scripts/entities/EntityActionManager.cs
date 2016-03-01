using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Entity))]

public class EntityActionManager : MonoBehaviour {

    public Actions[] actions = new Actions[3];

    void Start()
    {
        Entity entity = GetComponent<Entity>();
        
        int i = 0;
        foreach(Actions a in actions)
        {
            GameObject obj = ((GameObject)Instantiate(a.gameObject, this.transform.position, new Quaternion()));
            obj.transform.parent = transform;
            Actions act = obj.GetComponent<Actions>();
            act.setEntity(entity);
            
            actions[i] = act;
            i++;
        }
    }
    
}
