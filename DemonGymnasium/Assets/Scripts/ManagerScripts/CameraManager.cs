using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {
    public Transform janitorCamera;
    public Transform demonCamera;

    public float cameraMovementTime = 2;

    public float movementSpeed;
    public float rotationSpeed;

    bool cameraInMotion;
    float cameraMovementTimer;
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
    }

    void Update()
    {
        if (cameraMovementTimer > 0)
        {
            cameraMovementTimer = Mathf.MoveTowards(cameraMovementTimer, 0, Time.deltaTime);
            mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, goalPoistion, Time.deltaTime * movementSpeed);
            mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, goalRotation, Time.deltaTime * rotationSpeed);

        }
        else
        {
            float hInput = Input.GetAxisRaw("Horizontal");
            float vInput = Input.GetAxisRaw("Vertical");

            Vector3 fwd = mainCamera.transform.forward - Vector3.up * mainCamera.transform.forward.y;
            Vector3 right = mainCamera.transform.right - Vector3.up * mainCamera.transform.right.y;
            goalPoistion = mainCamera.transform.position + fwd * vInput * 100 + right * hInput * 100;

            mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, goalPoistion, Time.deltaTime * movementSpeed);
        }
        
    }

    public void shiftCamera(int playerTurn)
    {
        goalPoistion = cameraPositions[playerTurn];
        goalRotation = cameraRotations[playerTurn];
        cameraInMotion = true;
        cameraMovementTimer = cameraMovementTime;
    }

    public bool getCameraInMotion()
    {
        return cameraMovementTimer > 0;
    }
}
