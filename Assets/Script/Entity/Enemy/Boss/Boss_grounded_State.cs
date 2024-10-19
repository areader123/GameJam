using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SK;
public class Boss_grounded_State : EnemyState
{
    protected Enemy_Boss enemy;
    public Boss_grounded_State(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName, Enemy_Boss enemy) : base(stateMachine, enemyBase, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.SetVelocity(Vector3.zero.x, Vector3.zero.y, 0);
    }

    public override void Exit()
    {
        base.Exit();
        enemy.SetVelocity(Vector3.zero.x, Vector3.zero.y, 0);
    }

    public override void Update()
    {
        base.Update();

         if (enemy.DoSkill_Two())
            {
                stateMachine.ChangeState(enemy.boss_Attack_Three_State);
                return;
            }
        if (enemy.IsCharacterFightingWith() && enemy.CanAttack())
        {
            stateMachine.ChangeState(enemy.boss_Battle_State);
        }
        
    }


}
