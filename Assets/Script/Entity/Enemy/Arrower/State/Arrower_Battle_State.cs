using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SK
{


    public class Arrower_Battle_State : EnemyState
    {
        Enemy_Arrower enemy;
        public Arrower_Battle_State(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName,Enemy_Arrower enemy) : base(stateMachine, enemyBase, animBoolName)
        {
            this.enemy =enemy;
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
                if (enemy.IsCharacterAttackable())
                {
                    if (DoAttack())
                    {
                        //如果可以攻击则攻击
                        stateMachine.ChangeState(enemy.arrower_Attack_State);
                        return;
                    }
                    else
                    {
                        //在攻击范围内 但攻击冷却中
                        stateMachine.ChangeState(enemy.arrower_Idel_State);
                        return;
                    }
                }
            }
            else
            {
                Debug.Log("脱战");
                stateMachine.ChangeState(enemy.arrower_Move_State);
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
