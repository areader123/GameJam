using System.Collections;
using System.Collections.Generic;
using SK;
using UnityEngine;

public class SkeletonDieState : EnemyState
{
    Enemy_Skeleton enemy;
    public SkeletonDieState(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName,Enemy_Skeleton enemy) : base(stateMachine, enemyBase, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.rb.velocity = Vector2.zero;
    }
    public override void Update()
    {
        base.Update();
        if(triggerCalled)
        {
            enemy.Destroy();
        }
    }
}


