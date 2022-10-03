using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    PlayerBaseState currentState;
    PlayerIdleState IdleState = new PlayerIdleState();
    PlayerRunState RunState = new PlayerRunState();
    PlayerAttackState AttackState = new PlayerAttackState();
    PlayerGetDamageState GetDamageState = new PlayerGetDamageState();
    PlayerJumpState JumpState = new PlayerJumpState();
    PlayerDeathState DeathState = new PlayerDeathState();

    // Start is called before the first frame update
    void Start()
    {
        currentState = IdleState;
        currentState.EnterState();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
