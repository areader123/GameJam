using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SK
{

    public class Arrower_Grounded_State : EnemyState
    {
        protected Enemy_Arrower enemy;
        public Arrower_Grounded_State(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName,Enemy_Arrower enemy) : base(stateMachine, enemyBase, animBoolName)
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
            if(enemy.IsCharacterFightingWith() && enemy.CanAttack())
            {
                stateMachine.ChangeState(enemy.arrower_Battle_State);
            }
        }
    }
}
