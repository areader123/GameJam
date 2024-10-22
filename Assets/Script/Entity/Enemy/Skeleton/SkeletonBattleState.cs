using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SK
{

    public class SkeletonBattleState : EnemyState
    {
        protected Enemy_Skeleton enemy;
        private Transform player;
        private int moveDir;
        public SkeletonBattleState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Skeleton enemy) : base(stateMachine, enemyBase, animBoolName)
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
            if (enemy.IsCharacterFightingWith())
            {
                if (enemy.CanskillUsedInBattleRange && enemy.ifHaveSkill)
                {
                    if (enemy.DoSkill_One())
                    {
                        stateMachine.ChangeState(enemy.skeleton_Skill_Attack_State);
                        return;
                    }
                }
                if (enemy.IsCharacterAttackable())
                {
                    if (DoAttack())
                    {
                        if (enemy.DoSkill_One() && !enemy.CanskillUsedInBattleRange  && enemy.ifHaveSkill)
                        {
                            stateMachine.ChangeState(enemy.skeleton_Skill_Attack_State);
                            return;
                        }
                        //如果可以攻击则攻击
                        stateMachine.ChangeState(enemy.Skeleton_AttackState);
                         return;
                    }
                    else
                    {
                        //在攻击范围内 但攻击冷却中
                        stateMachine.ChangeState(enemy.Skeleton_IdolState);
                         return;
                    }
                }
            }
            else
            {
                //Debug.Log("脱战");
                stateMachine.ChangeState(enemy.Skeleton_MoveState);
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
}
