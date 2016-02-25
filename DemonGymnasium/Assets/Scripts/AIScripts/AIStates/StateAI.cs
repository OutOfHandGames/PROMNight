using UnityEngine;
using System.Collections;

public abstract class StateAI {
    public int depth;
    public float chanceMistake;
    public StateMachineBehaviour stateMachine;

    public abstract MoveInfo getBestMove();

    public virtual void startState(StateMachineBehaviour stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public virtual void endState()
    {

    }
}
