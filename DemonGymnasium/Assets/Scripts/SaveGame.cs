using UnityEngine;
using System.Collections;

/// <summary>
/// d = demon
/// D = Demon Queen
/// j = Janitor
/// J = Janitor King
/// o = obstacle
/// , = end tile
/// 0 = janitor type tile
/// 1 = demon type tile
/// </summary>
public class SaveGame : MonoBehaviour {
    public void saveGame()
    {
        string saveGameString = "";
        saveGameString += MapGenerator.BoardWidth;
        saveGameString += MapGenerator.BoardHeight;

        for (int x = 0; x < MapGenerator.BoardWidth; x++)
        {
            for (int y = 0; y < MapGenerator.BoardHeight; y++)
            {
                Tile tileAtPoint = MapGenerator.getTileAtPoint(new Point2(x, y));
                if (tileAtPoint.getCurrentEntity() != null)
                {
                    int entityType = tileAtPoint.getCurrentEntity().entityType;
                    if (entityType == 2)
                    {
                        saveGameString += "o";
                    }
                    else if (entityType == Tile.DEMON && tileAtPoint.getCurrentEntity() is King)
                    {
                        saveGameString += "D";
                    } 
                    else if (entityType == Tile.DEMON)
                    {
                        saveGameString += "d";
                    }
                    else if (entityType == Tile.JANITOR && tileAtPoint.getCurrentEntity() is King)
                    {
                        saveGameString += "J";
                    }
                    else if (entityType == Tile.JANITOR)
                    {
                        saveGameString += "j";
                    }
                }
                saveGameString += tileAtPoint.getCurrentTileType();
                saveGameString += ",";
            }
        }

        PlayerPrefs.SetString("saveGame", saveGameString);
    }

    public void loadGame()
    {
        string loadedGameString = PlayerPrefs.GetString("saveGame");
    }
}
