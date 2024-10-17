using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  SK;
public class Arrower_Dead_State : EnemyState
{
     Enemy_Arrower enemy;
        public Arrower_Dead_State(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName, Enemy_Arrower enemy) : base(stateMachine, enemyBase, animBoolName)
        {
            this.enemy = enemy;
        }

        public override void Enter()
        {
            base.Enter();
            enemy.rb.velocity = Vector2.zero;
        }
        public override void Update()
        {
            base.Update();
            if (triggerCalled)
            {
                enemy.Destroy();
            }
        }

}
