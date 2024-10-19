using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SK;
public class Boss_Idel_State : Boss_grounded_State
{
    public Boss_Idel_State(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName, Enemy_Boss enemy) : base(stateMachine, enemyBase, animBoolName, enemy)
    {

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
        enemy.SetVelocity(Vector3.zero.x, Vector3.zero.y, 0);
        //怪物移动有两种情况 一种是检测到敌人并且攻击冷却已好 另一种 检测到在战斗范围内但不在攻击范围内
        if (enemy.IsCharacterDectected() && enemy.CanAttack())
        {
            if (enemy.IsCharacterFightingWith() && !enemy.IsCharacterAttackable())
            {
                stateMachine.ChangeState(enemy.boss_Battle_State);
            }

            stateMachine.ChangeState(enemy.boss_Move_State);
        }
    }
}
