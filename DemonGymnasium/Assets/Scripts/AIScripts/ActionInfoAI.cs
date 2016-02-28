using UnityEngine;
using System.Collections;

public class ActionInfoAI {
    const string AttackName = "Attack";
    const string ExpandName = "Expand";
    const string MoveName = "Move";

    public void alterMapWithAction(string actionName, Point2 origin, Point2 destination)
    {
        switch (actionName)
        {
            case AttackName:
                alterMapAttack(origin, destination);
                return;
            case ExpandName:
                alterMapExpand(origin);
                return;
            case MoveName:
                alterMapMove(origin, destination);
                return;
        }
    }

    void alterMapAttack(Point2 origin, Point2 destination)
    {

    }

    void alterMapMove(Point2 origin, Point2 destination)
    {

    }

    void alterMapExpand(Point2 origin)
    {

    }
}
