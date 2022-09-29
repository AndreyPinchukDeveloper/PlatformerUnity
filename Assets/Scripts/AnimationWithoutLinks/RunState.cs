using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class RunState : State
{
    private CompositeDisposable _runDisposable = new CompositeDisposable();
    [SerializeField] private float _runSpeed = 5f;
    private PlayerScript _player;
    public RunState(PlayerScript player)
    {
        _player = player;
    }

    public override void Enter()
    {
        base.Enter();

        RunLogic();
        RunAnimation();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        
    }

    private void RunAnimation()
    {

    }

    private void RunLogic()
    {
        Vector2 vel = new Vector2(0, _body.velocity.y);

        if (_player._xAxis < 0)
        {
            vel.x = -_runSpeed;
            transform.localScale = new Vector2(-1, 1);
            ChangeAnimationState(PLAYER_RUN);
        }
        else if (_xAxis > 0)
        {
            vel.x = _runSpeed;
            transform.localScale = new Vector2(1, 1);
            ChangeAnimationState(PLAYER_RUN);
        }
        else vel.x = 0;

        if (_isGrounded && !_isAttacking)//this condition need for animations don't override themself
        {
            if (_xAxis != 0)
            {
                ChangeAnimationState(PLAYER_RUN);
            }
            else ChangeAnimationState(PLAYER_IDLE);
        }
    }
}
