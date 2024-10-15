using System.Collections;
using System.Collections.Generic;
using SK;
using UnityEngine;
using UnityEngine.Playables;

public class Player_Attack_State : PlayerState
{
    public Player_Attack_State(string _animboolname, StateMachine _stateMachine, Character _character) : base(_animboolname, _stateMachine, _character)
    {
    }

    public override void Enter()
    {
        base.Enter();

    }
    public override void Update()
    {
        base.Update();
        character.SetVelocity(Vector3.zero.x,Vector3.zero.y,0);
        if(triggercalled)
        {
            stateMachine.ChangeState(character.player_Idel_State);
        }
    }
}
