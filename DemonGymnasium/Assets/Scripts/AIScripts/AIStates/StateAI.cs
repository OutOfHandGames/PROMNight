using UnityEngine;
using System.Collections.Generic;

public abstract class StateAI {
    public int depth;
    public float chanceMistake;
    public StateMachineBehaviour stateMachine;

    public abstract List<MoveInfo> getBestMoves(AIStateMachine aiStateMachine);
    public abstract float scoreMap(AIMapInfo mapInfo);

    public virtual void startState(StateMachineBehaviour stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public virtual void endState()
    {

    }
}
