using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {
    public Transform janitorCamera;
    public Transform demonCamera;

    public float cameraMovementTime = 2;

    public float movementSpeed;
    public float rotationSpeed;

    public float cameraDelayShift = 1;

    public float minZoom = -10f;
    public float maxZoom = 10f;

    float currentCameraZoom;

    float cameraDelayShiftTimer;

    bool cameraInMotion;
    float cameraMovementTimer;
    float defaultCameraZoom;
    GameManager gameManager;
    Vector3 goalPoistion;
    Quaternion goalRotation;
    Camera mainCamera;
    Vector3[] cameraPositions;
    Quaternion[] cameraRotations;


    void Start()
    {
        gameManager = GetComponent<GameManager>();
        mainCamera = GameObject.FindObjectOfType<Camera>();
        cameraPositions = new Vector3[] { janitorCamera.position, demonCamera.position };
        cameraRotations = new Quaternion[] { janitorCamera.rotation, demonCamera.rotation };
        shiftCamera(gameManager.currentTurn);
        defaultCameraZoom = mainCamera.orthographicSize;
        currentCameraZoom = defaultCameraZoom;
    }

    void Update()
    {
        if (cameraInMotion && cameraDelayShiftTimer > 0)
        {
            cameraDelayShiftTimer = Mathf.MoveTowards(cameraDelayShiftTimer, 0, Time.deltaTime);
            cameraMovementTimer = 0;
            //print(2);
            //print(cameraMovementTimer);
        }
        else if (cameraMovementTimer > 0)
        {
            //print(2);
            cameraInMotion = false;
            cameraMovementTimer = Mathf.MoveTowards(cameraMovementTimer, 0, Time.deltaTime);
            mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, goalPoistion, Time.deltaTime * movementSpeed);
            mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, goalRotation, Time.deltaTime * rotationSpeed);
        }
        else if (cameraInMotion)
        {
            shiftCamera(gameManager.currentTurn);
            //print(3);
        }
        else
        {
            cameraInMotion = false;
            float hInput = Input.GetAxisRaw("Horizontal");
            float vInput = Input.GetAxisRaw("Vertical");

            Vector3 fwd = mainCamera.transform.forward - Vector3.up * mainCamera.transform.forward.y;
            Vector3 right = mainCamera.transform.right - Vector3.up * mainCamera.transform.right.y;
            goalPoistion = mainCamera.transform.position + fwd * vInput * 100 + right * hInput * 100;
            updateCameraZoom();
            mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, goalPoistion, Time.deltaTime * movementSpeed);
        }

        
    }

    public void updateCameraZoom()
    {
        float zInput = Input.GetAxisRaw("Mouse ScrollWheel");
        //print("I am here");
        currentCameraZoom += zInput;
        if (currentCameraZoom > maxZoom)
        {
            currentCameraZoom = maxZoom;
        }
        if (currentCameraZoom < minZoom)
        {
            currentCameraZoom = minZoom;
        }

        mainCamera.orthographicSize = currentCameraZoom;
    }

    public void shiftCamera(int playerTurn)
    {
        goalPoistion = cameraPositions[playerTurn];
        goalRotation = cameraRotations[playerTurn];
        cameraInMotion = true;
        cameraMovementTimer = cameraMovementTime;
    }

    public void shiftCameraDelay(int playerTurn)
    {
        cameraDelayShiftTimer = cameraDelayShift;
        cameraInMotion = true;
    }

    public bool getCameraInMotion()
    {
        return cameraMovementTimer > 0;
    }

    
}
