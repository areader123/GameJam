using System.Collections;
using System.Collections.Generic;
using SK;
using UnityEngine;

public class Arrower_Hit_State : EnemyState
{
    Enemy_Arrower enemy;
    public Arrower_Hit_State(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName, Enemy_Arrower enemy) : base(stateMachine, enemyBase, animBoolName)
    {
        this.enemy = enemy;
    }
    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (triggerCalled)
        {
           stateMachine.ChangeState(enemy.arrower_Idel_State);
        }

    }
}
