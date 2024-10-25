using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SK
{

    public class Arrower_Idel_State : Arrower_Grounded_State
    {
        // Start is called before the first frame update

        public Arrower_Idel_State(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName, Enemy_Arrower enemy) : base(stateMachine, enemyBase, animBoolName, enemy)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("Arrower正在Idel");
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
            enemy.SetVelocity(Vector3.zero.x, Vector3.zero.y, 0);
            //怪物移动有两种情况 一种是检测到敌人并且攻击冷却已好 另一种 检测到在战斗范围内但不在攻击范围内
            if (enemy.IsCharacterDectected())
            {
                if (enemy.IsCharacterFightingWith() && !enemy.IsCharacterAttackable())
                {
                    stateMachine.ChangeState(enemy.arrower_Battle_State);
                }

                 stateMachine.ChangeState(enemy.arrower_Move_State);
            }
        }



    }
}
