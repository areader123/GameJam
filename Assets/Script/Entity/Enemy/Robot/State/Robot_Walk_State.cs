using System.Collections;
using System.Collections.Generic;
using SK;
using UnityEngine;
namespace SK
{

    public class Robot_Walk_State : Robot_Grounded_State
    {
        public Robot_Walk_State(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName, Enemy_Robot enemy) : base(stateMachine, enemyBase, animBoolName, enemy)
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
            enemy.SetVelocity(enemy.characterDirection.x,enemy.characterDirection.y,enemy.movementSpeed);
            if(!enemy.IsCharacterDectected() && !enemy.IsCharacterAttackable())
            {
                stateMachine.ChangeState(enemy.robot_Idel_State);
            }
        }
    }
}
