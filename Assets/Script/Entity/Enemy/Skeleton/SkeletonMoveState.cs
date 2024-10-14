using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
namespace SK
{

    public class SkeletonMoveState : SkeletonGroundState
    {
        public SkeletonMoveState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Skeleton enemy) : base(enemyBase, stateMachine, animBoolName, enemy)
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
                stateMachine.ChangeState(enemy.Skeleton_IdolState);
            }
        }
    }
}
