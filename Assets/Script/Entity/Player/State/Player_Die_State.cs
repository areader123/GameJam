using System.Collections;
using System.Collections.Generic;
using SK;
using UnityEngine;

public class Player_Die_State : PlayerState
{
    public Player_Die_State(string _animboolname, StateMachine _stateMachine, Character _character) : base(_animboolname, _stateMachine, _character)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        base.Update();
        character.rb.velocity = Vector2.zero;
        if(triggercalled)
        {
            character.Destroy();
        }
    }
}
