using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public State CurrentState { get; set; }

    //method to initialize first state
    public void Initialize(State startState)
    {
        CurrentState = startState;
        CurrentState.Enter();
    }

    //state switcher
    public void ChangeState(State newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
