using System.Collections;
using System.Collections.Generic;
using SK;
using UnityEngine;

namespace SK
{

    public class Robot_Dead_State : EnemyState
    {
        Enemy_Robot enemy;
        public Robot_Dead_State(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName, Enemy_Robot enemy) : base(stateMachine, enemyBase, animBoolName)
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
                //此处调用robot爆炸死亡技能
                enemy.Destroy();
            }
        }

    }
}
