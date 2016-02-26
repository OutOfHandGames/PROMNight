using UnityEngine;
using System.Collections;

public class InputTouchScreen : MonoBehaviour {
    static Vector2[] touchPositions = new Vector2[5];
    static Vector2[] oldTouchPositions = new Vector2[5];

    void Update()
    {
        foreach(Touch t in Input.touches)
        {
            touchPositions[t.fingerId] = t.position;
        }
    }


    public static float getInputSeparation()
    {
        if (Input.touchCount != 2)
        {
            return 0;
        }
        else
        {
            Touch[] allTouches = Input.touches;
            foreach (Touch t in allTouches)
            {
                
            }
            return 0;
        }
    }

    public static float getDragRate()
    {
        return 0;
    }
}
