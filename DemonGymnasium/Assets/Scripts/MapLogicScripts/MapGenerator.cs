using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour, MapProperties {
    public int height = 10;
    public int width = 10;
    public float obstructionPercantage = .05f;
	public GameObject tileObject;
	public Entity obstructionObject;
	public Entity monsterObject;
	public Entity playerObject;
    public Entity kingPlayer;
    public Entity kingMonster;
	public int numMonsters;
	public int numPlayers;
	public static Tile[,] mapTiles;

	private Entity[] monsters;
	private Entity[] players;
    public static int[] currentTileTypes = new int[3];
    public static int BoardWidth;
    public static int BoardHeight;


	void Start() {
		mapTiles = new Tile[width, height];
		monsters = new Entity[numMonsters];
		players = new Entity[numPlayers];
		generateMap();
        
        BoardWidth = mapTiles.GetLength(0);
        BoardHeight = mapTiles.GetLength(1);
        GetComponent<TileColorManager>().enabled = true;

        updateTileScore();
        //print(currentTileTypes[2]);
    }

	void generateMap() {
		//int monsterCount = 0, playerCount = 0;
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				GameObject obj = (GameObject)Instantiate(tileObject, Vector3.zero, new Quaternion());
				Tile tile = obj.GetComponent<Tile> ();

				tile.setLocation (i, j);
                tile.transform.parent = this.transform;
				mapTiles [i, j] = tile;

                if (Random.Range(0f, 1f)  < obstructionPercantage)
                {
					/*Entity entity = ((GameObject)Instantiate(obstructionObject.gameObject, Vector3.zero, new Quaternion())).GetComponent<Entity>();
                    tile.setInitialEntity(entity);*/
                }

				//place monsters in first column and players in last
//				if (monsterCount < numMonsters && i == 0 && !tile.getIsObstructed()) {
//					Entity entity = ((GameObject)Instantiate(monsterObject.gameObject, Vector3.zero, new Quaternion())).GetComponent<Entity>();
//                    tile.setInitialEntity(entity);
//					monsters[monsterCount] = entity;
//					monsterCount++;
//				} else if (playerCount < numPlayers && i == width - 1 && !tile.getIsObstructed()) {
//					Entity entity = ((GameObject)Instantiate(playerObject.gameObject, Vector3.zero, new Quaternion())).GetComponent<Entity>();
//					tile.setInitialEntity(entity);
//					players[playerCount] = entity;
//					playerCount++;
//				}

			}
		}

        mapTiles[0, 0].setInitialEntity(((GameObject)Instantiate(kingMonster.gameObject, Vector3.zero, new Quaternion())).GetComponent<Entity>());
		mapTiles[1, 0].setInitialEntity(((GameObject)Instantiate(monsterObject.gameObject, Vector3.zero, new Quaternion())).GetComponent<Entity>());
		mapTiles[0, 1].setInitialEntity(((GameObject)Instantiate(monsterObject.gameObject, Vector3.zero, new Quaternion())).GetComponent<Entity>());
		mapTiles[2, 0].setInitialEntity(((GameObject)Instantiate(monsterObject.gameObject, Vector3.zero, new Quaternion())).GetComponent<Entity>());
		mapTiles[0, 2].setInitialEntity(((GameObject)Instantiate(monsterObject.gameObject, Vector3.zero, new Quaternion())).GetComponent<Entity>());
		mapTiles[1, 1].setInitialEntity(((GameObject)Instantiate(monsterObject.gameObject, Vector3.zero, new Quaternion())).GetComponent<Entity>());

		mapTiles[8, 8].setInitialEntity(((GameObject)Instantiate(kingPlayer.gameObject, Vector3.zero, new Quaternion())).GetComponent<Entity>());
		mapTiles[7, 8].setInitialEntity(((GameObject)Instantiate(playerObject.gameObject, Vector3.zero, new Quaternion())).GetComponent<Entity>());
		mapTiles[8, 7].setInitialEntity(((GameObject)Instantiate(playerObject.gameObject, Vector3.zero, new Quaternion())).GetComponent<Entity>());
		mapTiles[8, 6].setInitialEntity(((GameObject)Instantiate(playerObject.gameObject, Vector3.zero, new Quaternion())).GetComponent<Entity>());
		mapTiles[6, 8].setInitialEntity(((GameObject)Instantiate(playerObject.gameObject, Vector3.zero, new Quaternion())).GetComponent<Entity>());
		mapTiles[7, 7].setInitialEntity(((GameObject)Instantiate(playerObject.gameObject, Vector3.zero, new Quaternion())).GetComponent<Entity>());


        mapTiles[3, 3].setInitialEntity(((GameObject)Instantiate(obstructionObject.gameObject, Vector3.zero, new Quaternion())).GetComponent<Entity>());
        mapTiles[3, 5].setInitialEntity(((GameObject)Instantiate(obstructionObject.gameObject, Vector3.zero, new Quaternion())).GetComponent<Entity>());
        mapTiles[5, 3].setInitialEntity(((GameObject)Instantiate(obstructionObject.gameObject, Vector3.zero, new Quaternion())).GetComponent<Entity>());
        mapTiles[5, 5].setInitialEntity(((GameObject)Instantiate(obstructionObject.gameObject, Vector3.zero, new Quaternion())).GetComponent<Entity>());
    }

    public static Tile getTileAtPoint(Point2 p)
    {
        //print(p);
        return getTileAtPoint(p.x, p.y);
    }

    public static Tile getTileAtPoint(int x, int y)
    {
        return mapTiles[x, y];
    }

    public Tile getTile(Point2 point)
    {
        return mapTiles[point.x, point.y];
    }

    public Tile getTile(int x, int y)
    {
        return getTile(new Point2(x, y));
    }

    public static void updateTileScore()
    {
        resetCurrentTileCount(currentTileTypes);
        for (int x = 0; x < BoardWidth; x++)
        {
            for (int y = 0; y < BoardHeight; y++)
            {
                currentTileTypes[MapGenerator.getTileAtPoint(new Point2(x, y)).getCurrentTileType()]++;
            }
        }
    }

    static void resetCurrentTileCount(int[] count)
    {
        for (int i = 0; i < count.Length; i++)
        {
            count[i] = 0;
        }
    }

    public int getNumberTiles(int tileType)
    {
        return currentTileTypes[tileType];
    }

	public Tile getTileAtPosition(int x, int y) {
		return mapTiles [x, y];
	}

	public Entity[] getMonsters() {
		return monsters;
	}

	public Entity[] getPlayers() {
		return players;
	}

    
}
