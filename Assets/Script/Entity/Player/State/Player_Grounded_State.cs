using System.Collections;
using System.Collections.Generic;
using SK;
using UnityEngine;
using UnityEngine.InputSystem;
namespace SK
{
    public class Player_Grounded_State : PlayerState
    {
        public Player_Grounded_State(string _animboolname, StateMachine _stateMachine, Character _character) : base(_animboolname, _stateMachine, _character)
        {
        }
        public override void Update()
        {
            base.Update();
            if (Keyboard.current.shiftKey.wasPressedThisFrame && SkillManager.instance.dash_Skill.dashUnlocked)
            {
                SkillManager.instance.dash_Skill.CanUseSkill();
            }
            if(Keyboard.current.cKey.wasPressedThisFrame && SkillManager.instance.clone_Skill.cloneUnlocked)
            {
                SkillManager.instance.clone_Skill.CanUseSkill();
            }

        }
        public override void Exit()
        {
            base.Exit();
        }

        public override void Enter()
        {
            base.Enter();
        }
    }
}

