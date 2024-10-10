using System.Collections;
using System.Collections.Generic;
using SK;
using UnityEngine;
namespace SK
{

    public class Player_Idel_State : PlayerState
    {
        public Player_Idel_State(string _animboolname, StateMachine _stateMachine, Character _character) : base(_animboolname, _stateMachine, _character)
        {
        }
        public override void Update()
        {
            base.Update();
            if (character.inputHandler.moveAmount >= 0.1f)
            {
                stateMachine.ChangeState(character.player_Move_State);
            }
        }

        
    }
}
