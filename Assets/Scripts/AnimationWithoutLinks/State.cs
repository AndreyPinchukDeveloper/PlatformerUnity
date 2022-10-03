using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected PlayerScript player;
    protected Animator animator;
    protected string _currentState;
    protected State(PlayerScript _player, Animator _animator, string currentState)
    {
        player = _player;
        animator = _animator;
        _currentState = currentState;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }

    protected void ChangeAnimationState(string newState)
    {
        //stop the same animation from interrupting itself
        if (_currentState == newState) return;

        _currentState = newState;
        animator.Play(newState);

        //reassign the current state
        _currentState = newState;
    }

}
