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
        enemy.SetVelocity(Vector3.zero.x,Vector3.zero.y,0);
       
    }

    public override void Exit()
    {
        base.Exit();
       
    }

    public override void Update()
    {
        base.Update();
        if(enemy.IsCharacterDectected())
        {
            stateMachine.ChangeState(enemy.Skeleton_MoveState);
        }
    }
}
}
