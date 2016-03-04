using UnityEngine;
using System.Collections;

public class PlayerSelectManager : MonoBehaviour {
    public Entity currentCharacterSelected;
    public Tile currentTileSelected;
    public bool ignoreClick;
    public Color highlightColor = Color.green;
    

    Camera mainCamera;
    GameManager gameManager;
    bool isMobile = false;
    ActionPanel actionPanel;

    void Start()
    {
        mainCamera = GameObject.FindObjectOfType<Camera>();
        gameManager = GetComponent<GameManager>();
        isMobile = Application.isMobilePlatform;
        actionPanel = GameObject.FindObjectOfType<ActionPanel>();
    }

    void Update()
    {
        if (!isMobile && Input.GetButtonDown("Fire1")) 
        {
            //print("I was clicked!");
            mouseClicked();
        }
        else if (InputTouchScreen.GetNewTouchDown())
        {
            mouseClicked();
        }

    }

    public void mouseClicked()
    {
        if (!ignoreClick)
        {
            resetSelection();
            currentTileSelected = null;
            Ray ray;
            if (isMobile)
            {
                ray = mainCamera.ScreenPointToRay(InputTouchScreen.newTouchPosition);
            }
            else
            {
                ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            }
            
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Tile tile = hit.collider.GetComponent<Tile>();
                currentTileSelected = tile;
                if (tile != null)
                {
                    Entity tileEntity = tile.getCurrentEntity();
                    if (tileEntity != null && tileEntity.entityType == gameManager.currentTurn)
                    {
                        currentCharacterSelected = tile.getCurrentEntity();
                        setHighlightColor(tileEntity);
                        tileEntity.GetComponentInChildren<SpriteRenderer>().color = Color.green;

                        actionPanel.gameObject.SetActive(true);
                        actionPanel.Initialize(tileEntity.transform);
                        
                    }

                }

            }
            else
            {
                Debug.Log("hit nothing" + gameObject.name);
            }
        }
    }

    void setHighlightColor(Entity entity)
    {
        if (currentCharacterSelected != null)
        {
            currentCharacterSelected.GetComponentInChildren<SpriteRenderer>().color = Color.white;
        }
        entity.GetComponentInChildren<SpriteRenderer>().color = highlightColor;
    }

    public void resetColor()
    {
        if (currentCharacterSelected != null)
        {
            currentCharacterSelected.GetComponentInChildren<SpriteRenderer>().color = Color.white;
        }
    }

    public void resetSelection()
    {
       resetColor();
        currentCharacterSelected = null;
    }

    public static void TouchDetected(int touchCount)
    {

    }
}
