using UnityEngine;
using System.Collections;

public class PlayerSelectManager : MonoBehaviour {
    public Entity currentCharacterSelected;
    public Tile currentTileSelected;
    public bool ignoreClick;
    public Color highlightColor = Color.green;
    

    Camera mainCamera;
    GameManager gameManager;
    PlayerModal playerModal;

    void Start()
    {
        mainCamera = GameObject.FindObjectOfType<Camera>();
        gameManager = GetComponent<GameManager>();
        playerModal = GameObject.FindObjectOfType<PlayerModal>();
    }

    void Update()
    {
        if (!ignoreClick && Input.GetButtonDown("Fire1"))
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
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
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
                        playerModal.SetUIPos(tileEntity.transform);
                        playerModal.Enable();
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
}
