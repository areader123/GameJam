using System.Collections;
using System.Collections.Generic;
using SK;
using UnityEngine;

public class Player_Move_Attack_State : Player_Move_State
{
    public Player_Move_Attack_State(string _animboolname, StateMachine _stateMachine, Character _character) : base(_animboolname, _stateMachine, _character)
    {
    }

    public override void Update()
    {
        base.Update();
        if (character.inputHandler.moveAmount == 0f)
            {
                stateMachine.ChangeState(character.player_Idel_State);
            }
        if(triggercalled)
        {
            stateMachine.ChangeState(character.player_Move_State);
        }
    }




}
