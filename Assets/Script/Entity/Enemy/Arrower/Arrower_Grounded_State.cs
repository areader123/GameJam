using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SK
{

    public class Arrower_Grounded_State : EnemyState
    {
        protected Enemy_Arrower enemy;
        public Arrower_Grounded_State(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName,Enemy_Arrower enemy) : base(stateMachine, enemyBase, animBoolName)
        {
            this.enemy = enemy;
        }
    }
}
