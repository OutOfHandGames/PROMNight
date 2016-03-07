using System.Collections.Generic;
using System;

public class BasicStateAI : StateAI {
    

    public override List<MoveInfo> getBestMoves(AIStateMachine aiStateMachine)
    {
        List<MoveInfo> bestMoves = new List<MoveInfo>();
        return bestMoves;
    }

    public override float scoreMap()
    {
        
        return 0;
    }

    List<MoveInfo> getRandomMoves(AIMapInfo aiMapInfo)
    {
        List<MoveInfo> moveList = new List<MoveInfo>();
        
        
        return moveList;
    }

}
