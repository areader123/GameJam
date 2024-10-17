using System.Collections;
using System.Collections.Generic;
using SK;
using UnityEngine;
namespace SK
{

    public class Robot_Grounded_State : EnemyState
    {
        protected Enemy_Robot enemy;
        public Robot_Grounded_State(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName, Enemy_Robot enemy) : base(stateMachine, enemyBase, animBoolName)
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
            if(enemy.IsCharacterFightingWith() && enemy.CanAttack())
            {
                stateMachine.ChangeState(enemy.robot_Battle_State);
            }
        }
    }
}
