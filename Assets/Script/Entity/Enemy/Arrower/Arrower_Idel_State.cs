using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SK
{

    public class Arrower_Idel_State : Arrower_Grounded_State
    {
        // Start is called before the first frame update

        public Arrower_Idel_State(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName,Enemy_Arrower enemy) : base(stateMachine, enemyBase, animBoolName,enemy)
        {
        }


    }
}
