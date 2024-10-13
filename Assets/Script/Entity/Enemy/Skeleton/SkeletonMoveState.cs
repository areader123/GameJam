using System.Collections;
using System.Collections.Generic;
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
            //enemy.SetVelocity(enemy.defaultMoveSpeed * enemy.faceDir, rb.velocity.y);
            enemy.rb.velocity = new Vector2(0, 0);
            //Debug.Log("�����ƶ�");
        }

        public override void Exit()
        {
            base.Exit();
            // enemy.SetVelocity(enemy.defaultMoveSpeed * enemy.faceDir, rb.velocity.y);
            enemy.rb.velocity = new Vector2(0, 0);
            // Debug.Log("�ƶ��˳�");
        }

        public override void Update()
        {
            base.Update();
            //时间静止
            // if (!enemy.timeFrozen)
            //     enemy.SetVelocity(enemy.defaultMoveSpeed * enemy.faceDir, rb.velocity.y);


      

        }
    }
}
