using System.Collections;
using System.Collections.Generic;
using SK;
using UnityEngine;

public class Boss_Attack_one_State : EnemyState
{
    Enemy_Boss enemy;
    public float animationCounter;
    public Boss_Attack_one_State(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName, Enemy_Boss enemy) : base(stateMachine, enemyBase, animBoolName)
    {
        this.enemy = enemy;
    }
    public override void Enter()
    {
        base.Enter();
        enemy.attackting = true;
        enemy.SetVelocity(Vector3.zero.x, Vector3.zero.y, 0);
    }
    public override void Update()
    {
        base.Update();
        animationCounter -= Time.deltaTime;
        if (enemy.animator.GetBool("Attack_One_Second"))
        {
            enemy.SetVelocity(enemy.faceDir, enemy.characterDirection.y, enemy.rollSpeed);
           enemy.RollDamage();
        }
        if (animationCounter < 0)
        {
            enemy.animator.SetBool("Attack_One_Second", false);
            enemy.SetVelocity(Vector3.zero.x, Vector3.zero.y, 0);
        }
        if (triggerCalled)
        {
            stateMachine.ChangeState(enemy.boss_Battle_State);
        }
    }

    public override void Exit()
    {
        base.Exit();
        enemy.SetVelocity(Vector3.zero.x, Vector3.zero.y, 0);
        enemy.attackting = false;
        enemy.lastTimeAttack = Time.time;
    }
}
