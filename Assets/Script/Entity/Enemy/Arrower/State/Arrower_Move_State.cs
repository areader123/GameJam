using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SK
{
    public class Arrower_Move_State : Arrower_Grounded_State
    {
        public Arrower_Move_State(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName, Enemy_Arrower enemy) : base(stateMachine, enemyBase, animBoolName, enemy)
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
                stateMachine.ChangeState(enemy.arrower_Idel_State);
            }
        }

    }
}
