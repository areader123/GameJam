using System.Collections;
using System.Collections.Generic;
using SK;
using UnityEngine;

public class Robot_Battle_State : EnemyState
{
    Enemy_Robot enemy;
    public Robot_Battle_State(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName, Enemy_Robot enemy) : base(stateMachine, enemyBase, animBoolName)
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
            if (enemy.CanskillUsedInBattleRange)
            {
                if (DoSkill_One())
                {
                    stateMachine.ChangeState(enemy.robot_Skill_Attack_State);
                    return;
                }
            }

            if (enemy.IsCharacterAttackable())
            {
                if (DoAttack())
                {
                    //如果可以攻击则攻击
                    //if() 这里进入 Skill_Attack 逻辑是普攻有cd 当能普攻的时候判断技能cd cd好了放技能 cd没好则普攻
                    //{
                    //return; 记得return 释放完技能后 就不会再次普攻了
                    //}
                    if (DoSkill_One() && !enemy.CanskillUsedInBattleRange)
                    {
                        stateMachine.ChangeState(enemy.robot_Skill_Attack_State);
                        return;
                    }
                    stateMachine.ChangeState(enemy.robot_Attack_State);
                    return;
                }
                else
                {
                    //在攻击范围内 但攻击冷却中
                    stateMachine.ChangeState(enemy.robot_Idel_State);
                    return;
                }
            }
        }
        else
        {
            //Debug.Log("脱战");
            stateMachine.ChangeState(enemy.robot_Walk_State);
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

    private bool DoSkill_One()
    {

        if (Time.time >= enemy.lastTimeSkill_One + enemy.skill_One_Cooldown)
        {
            enemy.lastTimeSkill_One = Time.time;
            return true;
        }
        return false;
    }

}
