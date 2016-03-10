using UnityEngine;
using System.Collections.Generic;

public class BasicStateAI : StateAI {
    int movesLeft = 3;
    int testCount = 50;

    public void setMovesLeft(int moves)
    {
        if (moves < 0)
        {
            throw new System.Exception("Move count cannot be less than 0");
        }
        movesLeft = moves;
    }

    public override List<MoveInfo> getBestMoves(AIStateMachine aiStateMachine)
    {
        List<MoveInfo> bestMoves = new List<MoveInfo>();
        List<MoveInfo> lastMoves = new List<MoveInfo>();
        List<TurnInfo> lastTurns = new List<TurnInfo>();
        float bestScore = -10000;

        for (int i = 0; i < testCount; i++)
        {
            lastMoves = new List<MoveInfo>();
            lastTurns.Clear();
            while (lastMoves.Count < 3)
            {
                MoveInfo move = getRandomMove(aiStateMachine.mapInfo);
                Actions action = move.entity.getEntityActionManager().actions[move.actionSelected];

                List<Point2> validTiles = action.getValidMoves(move.entity.getCurrentLocation(), aiStateMachine.mapInfo);
                if (validTiles.Count > 0)
                {
                    Point2 tileSelected = validTiles[Random.Range(0, validTiles.Count)];
                    lastTurns.Add(saveTurn(move.entity, move.actionSelected, tileSelected, aiStateMachine.mapInfo));

                    if (!action.performAction(validTiles[Random.Range(0, validTiles.Count)], aiStateMachine.mapInfo))
                    {
                        lastMoves.RemoveAt(lastMoves.Count);
                    }
                }
                

            }
            float mapScore = scoreMap(aiStateMachine.mapInfo);

            if (mapScore > bestScore)
            {
                bestMoves = lastMoves;
                bestScore = mapScore;
            }
            aiStateMachine.mapInfo.resetMap();
        }
        
        return bestMoves;
    }

    TurnInfo saveTurn(Entity selectedEntity, int action, Point2 tileSelected, AIMapInfo mapInfo)
    {
        TurnInfo turnInfo = new TurnInfo();
        List<Point2> affectedTiles = selectedEntity.getEntityActionManager().actions[action].getAffectedTiles(selectedEntity.getCurrentLocation(), tileSelected, mapInfo);
        foreach (Point2 p in affectedTiles)
        {
            turnInfo.addAffectedTile(mapInfo.getTile(p));
        }
        return turnInfo;
    }


    public override float scoreMap(AIMapInfo aiMapInfo)
    {
        int currentEnemyCount = 0;
        int currentEnemyKingCount = 0;
        int enemyTiles = 0;
        int friendlyTiles = 0;
        float mapScore = 0;
        for (int x = 0; x < MapGenerator.BoardWidth; x++)
        {
            for (int y = 0; y < MapGenerator.BoardHeight; y++)
            {
                Tile tileAtPoint = MapGenerator.getTileAtPoint(x, y);
                if (tileAtPoint.getCurrentEntity() != null && aiMapInfo.checkEntityIsEnemy(tileAtPoint.getCurrentEntity()))
                {
                    if (tileAtPoint.getCurrentEntity() is King)
                    {
                        currentEnemyKingCount++;
                    }
                    currentEnemyCount++;
                }
                if (tileAtPoint.currentTileType == Tile.JANITOR)
                {
                    enemyTiles++;
                }
                if (tileAtPoint.currentTileType == Tile.DEMON)
                {
                    friendlyTiles++;
                }
            }
        }
        if (currentEnemyKingCount <= 0)
        {
            mapScore += 100000;
        }

        mapScore += (5 * (friendlyTiles - aiMapInfo.tilesControlled));
        mapScore += (5 * (aiMapInfo.enemyTilesControlled - enemyTiles));
        mapScore += 30 * (aiMapInfo.getAllEnemyEntities().Count - currentEnemyCount);
        mapScore += scorePosition(aiMapInfo);
        return mapScore;
    }

    float scorePosition(AIMapInfo aiMapInfo)
    {
        float totalScore = 0;


        return totalScore;
    }

    MoveInfo getRandomMove(AIMapInfo aiMapInfo)
    {
        MoveInfo moveInfo = new MoveInfo();

        moveInfo.entity = aiMapInfo.getAllFriendlies()[Random.Range(0, aiMapInfo.getAllFriendlies().Count)];
        EntityActionManager eActionManager = moveInfo.entity.getEntityActionManager();
        moveInfo.actionSelected = Random.Range(0, eActionManager.actions.Length);

        return moveInfo;
    }

}
