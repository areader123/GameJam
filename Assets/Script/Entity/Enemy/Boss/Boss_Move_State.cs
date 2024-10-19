using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SK;
public class Boss_Move_State : Boss_grounded_State
{
    public Boss_Move_State(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName, Enemy_Boss enemy) : base(stateMachine, enemyBase, animBoolName, enemy)
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
                stateMachine.ChangeState(enemy.boss_Idel_State);
            }
        }
}
