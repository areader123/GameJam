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
        enemy.SetVelocity(Vector3.zero.x,Vector3.zero.y,0);
    }

    public override void Update()
    {
        base.Update();
        enemy.SetVelocity(Vector3.zero.x,Vector3.zero.y,0);
        //怪物移动有两种情况 一种是检测到敌人并且攻击冷却已好 另一种 检测到在战斗范围内但不在攻击范围内
        if(enemy.IsCharacterDectected() && enemy.CanAttack() )
        {
            if(enemy.IsCharacterFightingWith() && !enemy.IsCharacterAttackable())
            {
                stateMachine.ChangeState(enemy.Skeleton_BattleState);
            }
            stateMachine.ChangeState(enemy.Skeleton_MoveState);
        }
    }
}
}
