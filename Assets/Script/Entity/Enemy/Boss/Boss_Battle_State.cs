using System.Collections;
using System.Collections.Generic;
using SK;
using UnityEngine;

public class Boss_Battle_State : EnemyState
{
    Enemy_Boss enemy;
    public Boss_Battle_State(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName, Enemy_Boss enemy) : base(stateMachine, enemyBase, animBoolName)
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
        if (enemy.IsCharacterFightingWith())
        {

            if (enemy.DoSkill_Two())
            {
                stateMachine.ChangeState(enemy.boss_Attack_Three_State);
                return;
            }
            if (enemy.DoSkill_One())
            {
                stateMachine.ChangeState(enemy.boss_Attack_Two_State);
                return;
            }
            if (enemy.IsCharacterAttackable())
            {
                if (DoAttack())
                {
                    stateMachine.ChangeState(enemy.boss_Attack_One_State);
                    return;
                }
                else
                {
                    //在攻击范围内 但攻击冷却中
                    stateMachine.ChangeState(enemy.boss_Idel_State);
                    return;
                }
            }
        }
        else
        {
            //Debug.Log("脱战");
            stateMachine.ChangeState(enemy.boss_Move_State);
            return;
        }
        //设置速度
        enemy.SetVelocity(enemy.characterDirection.x, enemy.characterDirection.y, enemy.battleSpeed);


    }

    private bool DoAttack()
    {
        if (Time.time >= enemy.lastTimeAttack + enemy.attackCooldown)
        {
            enemy.lastTimeAttack = Time.time;
            return true;
        }
        return false;
    }


}

