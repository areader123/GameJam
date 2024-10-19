using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SK;
public class Boss_Dead_State : EnemyState
{
    Enemy_Boss enemy;
    public Boss_Dead_State(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName, Enemy_Boss enemy) : base(stateMachine, enemyBase, animBoolName)
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
