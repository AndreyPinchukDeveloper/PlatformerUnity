using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : State
{
    private MainHero _mainHero;

    public JumpState(MainHero mainHero)
    {
        _mainHero = mainHero;
    }

    public override void Enter()
    {
        base.Enter();
        _mainHero._animator.CrossFade("Jump", 0.1f);
        _mainHero._body.AddForce(new Vector2(0f, _mainHero._jumpForce), ForceMode2D.Impulse);
    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void Update()
    {
        base.Update();
    }
}
