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
        RunAnimation();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        RunLogic();
    }

    private void RunAnimation()
    {
        _mainHero._animator.SetFloat("Speed", Mathf.Abs(_mainHero.MovementX));
    }

    private void RunLogic()
    {


        _mainHero.MovementX = Input.GetAxisRaw("Horizontal");
        _mainHero.transform.position += new Vector3(_mainHero.MovementX, 0f, 0f) * Time.deltaTime * _mainHero.MoveForce;

        //going to the right side
        if (_mainHero.MovementX > 0)
        {
            _mainHero.SpriteRenderer.flipX = false;
        }
        //going to the left side
        else if (_mainHero.MovementX < 0)
        {
            _mainHero.SpriteRenderer.flipX = true;
        }
    }
}
