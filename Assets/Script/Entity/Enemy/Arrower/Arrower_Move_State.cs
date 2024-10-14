using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SK
{
    public class Arrower_Move_State : Arrower_Grounded_State
    {
        public Arrower_Move_State(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName,Enemy_Arrower enemy) : base(stateMachine, enemyBase, animBoolName, enemy)
        {
        }

      
    }
}
