using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    [SerializeField]
    private State currentState;

    private void Update()
    {
        RunStateMachine();
    }

    private void RunStateMachine()
    {
        State nextState = currentState?.RunCurrentState();
        
        if (nextState != null)
        {
            SwitchToTheNextState(nextState);
        }
    }

    private void SwitchToTheNextState(State nextState)
    {
        currentState = nextState;
    }

    public void SetState(State state)
    {
        currentState = state;
    }

    public State GetState()
    {
        return currentState;
    }
}
