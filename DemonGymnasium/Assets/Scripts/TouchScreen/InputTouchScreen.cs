using UnityEngine;
using System.Collections;

public class InputTouchScreen : MonoBehaviour {



    public static Vector2[] touchPositions = new Vector2[5];
    static Vector2[] oldTouchPositions = new Vector2[5];

    public enum TouchInputState {Idle, Pan, Zoom, Action};

    public TouchInputState touchState;


    //New Touch
    public static Vector2 newTouchPosition;
    public static int newTouchFingerId;
    

    void Start()
    {
        touchState = TouchInputState.Idle;
    }

    void Update()
    {
        foreach(Touch t in Input.touches)
        {
            if (t.phase == TouchPhase.Began)
            {

            }

            touchPositions[t.fingerId] = t.position;
        }

        CheckCurrentState();
        
        

    }

    public static bool OnTouchDrag(int index)
    {
        return Input.GetTouch(index).phase == TouchPhase.Moved;
    }

    public static bool GetNewTouchDown()
    {
        foreach (Touch t in Input.touches)
        {
            if (t.phase == TouchPhase.Began)
            {
                newTouchPosition = t.position;
                newTouchFingerId = t.fingerId;
                return true;
            }
        }
        return false;
    }


    public void CheckCurrentState()
    {
        if (Input.touchCount == 2)
        {
            touchState = TouchInputState.Zoom;
        }
        else if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                touchState = TouchInputState.Action;
                Debug.Log("Touch began");
            }
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
