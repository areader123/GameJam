using System.Collections;
using System.Collections.Generic;
using SK;
using UnityEngine;

public class Player_Dash_State : PlayerState
{
    public Player_Dash_State(string _animboolname, StateMachine _stateMachine, Character _character) : base(_animboolname, _stateMachine, _character)
    {

    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = SkillManager.instance.dash_Skill.dashDuration;

    }
    public override void Exit()
    {
        base.Exit();
        character.SetVelocity(0,0,0);
    }

    public override void Update()
    {
        base.Update();
        if (character.inputHandler.moveAmount != 0)
        {
            character.SetVelocity(character.horizonal, character.vertical, SkillManager.instance.dash_Skill.dashSpeed);
        }
        else
        {
            if (character.faceRight)
            {
                character.SetVelocity(1,0 , SkillManager.instance.dash_Skill.dashSpeed);
            }
            if (!character.faceRight)
            {
                character.SetVelocity(-1, 0, SkillManager.instance.dash_Skill.dashSpeed);
            }
         
        }
        if(stateTimer < 0)
        {
            stateMachine.ChangeState(character.player_Idel_State);
        }
    }
}
