using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SK
{

public class SkeletonIdolState : SkeletonGroundState
{
    public SkeletonIdolState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Skeleton enemy) : base(enemyBase, stateMachine, animBoolName, enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();
        enemy.rb.velocity = new Vector2(0, 0);
        stateTimer = 2.5f;
    }

    public override void Exit()
    {
        base.Exit();
        enemy.rb.velocity = new Vector2(0, 0);
        stateTimer = 2.5f;
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer <0) 
        {
            stateMachine.ChangeState(enemy.Skeleton_MoveState);
        }
    }
}
}
