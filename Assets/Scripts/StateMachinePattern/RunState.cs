using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : State
{
    private MainHero _mainHero;

    public RunState(MainHero mainHero)
    {
        _mainHero = mainHero;
    }

    public override void Enter()
    {
        base.Enter();
        _mainHero._animator.CrossFade("Run", 0.1f);
    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void Update()
    {
        base.Update();

        _mainHero.MovementX = Input.GetAxisRaw("Horizontal");
        _mainHero.transform.position += new Vector3(_mainHero.MovementX, 0f, 0f) * Time.deltaTime * _mainHero.MoveForce;
    }
}
