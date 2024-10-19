using System.Collections;
using System.Collections.Generic;
using SK;
using UnityEngine;

public class Boss_Attack_Two_State : EnemyState
{
    Enemy_Boss enemy;
    public float animationCounter;
    public Boss_Attack_Two_State(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName,Enemy_Boss enemy) : base(stateMachine, enemyBase, animBoolName)
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
            enemy.lastTimeSkill_One = Time.time;
        }
    public override void Update()
    {
        base.Update();
         animationCounter -= Time.deltaTime;
       if(animationCounter < 0)
        {
            enemy.animator.SetBool("Attack_Two_Second",false);
            enemy.SetVelocity(Vector3.zero.x, Vector3.zero.y, 0);
        }
        if(triggerCalled)
        {
            stateMachine.ChangeState(enemy.boss_Battle_State);
        }
    }
}
