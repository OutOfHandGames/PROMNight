using UnityEngine;
using System.Collections;

public class PlayerSelectManager : MonoBehaviour {
    public Entity currentCharacterSelected;
    public Tile currentTileSelected;
    public bool ignoreClick;
    public Color highlightColor = Color.green;
    

    Camera mainCamera;
    GameManager gameManager;
    public ActionPanel actionPanel;

    void Start()
    {
        mainCamera = GameObject.FindObjectOfType<Camera>();
        gameManager = GetComponent<GameManager>();
        //actionPanel = GameObject.FindObjectOfType<ActionPanel>();
    }

    void Update()
    {
        
        if (!ignoreClick && InputTouchScreen.GetNewTouchDown())
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
            Ray ray = mainCamera.ScreenPointToRay(InputTouchScreen.newTouchPosition);
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
