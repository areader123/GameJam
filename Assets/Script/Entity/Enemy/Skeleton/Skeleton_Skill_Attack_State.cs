using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SK;
public class Skeleton_Skill_Attack_State : EnemyState
{
    Enemy_Skeleton enemy;
    public Skeleton_Skill_Attack_State(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName,Enemy_Skeleton enemy) : base(stateMachine, enemyBase, animBoolName)
    {
        this.enemy = enemy;
    }


     public override void Enter()
        {
            base.Enter();
            enemy.SetVelocity(Vector3.zero.x, Vector3.zero.y, 0);
        }

        public override void Exit()
        {
            base.Exit();
            enemy.SetVelocity(Vector3.zero.x, Vector3.zero.y, 0);
            enemy.lastTimeAttack = Time.time;
        }

        public override void Update()
        {
            base.Update();
            enemy.SetVelocity(Vector3.zero.x, Vector3.zero.y, 0);
            if (triggerCalled)
            {
                stateMachine.ChangeState(enemy.Skeleton_BattleState);
            }

        }
}
