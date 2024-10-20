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
        }
    }
}
