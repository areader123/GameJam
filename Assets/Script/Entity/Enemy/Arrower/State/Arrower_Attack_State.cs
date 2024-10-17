using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SK
{

    public class Arrower_Attack_State : EnemyState
    {
        private Enemy_Arrower enemy;
        public Arrower_Attack_State(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName ,Enemy_Arrower enemy) : base(stateMachine, enemyBase, animBoolName)
        {
            this.enemy = enemy;
        }

         public override void Enter()
        {
            base.Enter();
            enemy.attackting= true;
            enemy.SetVelocity(Vector3.zero.x,Vector3.zero.y,0);
        }

        public override void Exit()
        {
            base.Exit();
         
            enemy.lastTimeAttack = Time.time;
        }

        public override void Update()
        {
            base.Update();
            enemy.SetVelocity(Vector3.zero.x,Vector3.zero.y,0);
            if (triggerCalled)
            {
                enemy.attackting = false;
                stateMachine.ChangeState(enemy.arrower_Battle_State);
            }

        }
    }
}
