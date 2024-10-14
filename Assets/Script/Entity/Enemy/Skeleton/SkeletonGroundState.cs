using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SK
{

    public class SkeletonGroundState : EnemyState
    {
        protected Enemy_Skeleton enemy;
        public SkeletonGroundState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Skeleton enemy) : base(stateMachine, enemyBase, animBoolName)
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
                stateMachine.ChangeState(enemy.Skeleton_BattleState);
            }
            //敌人检测玩家 如果检测到 则进入战斗状态
            // if (enemy.IsPlayerDetected() || Vector2.Distance(enemy.transform.position, player.position) < 2)
            // {
            //     stateMachine.ChangeState(enemy.Skeleton_BattleState);
            // }

        }
    }
}
