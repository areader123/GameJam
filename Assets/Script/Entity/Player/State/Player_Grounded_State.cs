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
            if (Input.GetKeyDown(SkillManager.instance.dash_Skill.keyCode) && SkillManager.instance.dash_Skill.dashUnlocked)
            {
                SkillManager.instance.dash_Skill.CanUseSkill();
            }
            if(Input.GetKeyDown(SkillManager.instance.clone_Skill.keyCode) && SkillManager.instance.clone_Skill.cloneUnlocked)
            {
                SkillManager.instance.clone_Skill.CanUseSkill();
            }
            if(Input.GetKeyDown(SkillManager.instance.changeWithEnemy_Skill.keyCode) && SkillManager.instance.changeWithEnemy_Skill.speedUpLocked)
            {
                SkillManager.instance.changeWithEnemy_Skill.CanUseSkill();
            }
            if(Input.GetKeyDown(SkillManager.instance.bullet_Skill.keyCode) &&SkillManager.instance.bullet_Skill.bulletWithLightingUnLocked )
            {
                SkillManager.instance.bullet_Skill.CanUseSkill();
            }
            if(Input.GetKeyDown(SkillManager.instance.bullet_Fan_Skill.keyCode) &&SkillManager.instance.bullet_Fan_Skill.bulletWithLightingUnLocked )
            {
                SkillManager.instance.bullet_Fan_Skill.CanUseSkill();
            }
            if(Input.GetKeyDown(SkillManager.instance.blood_Skill.keyCode) &&SkillManager.instance.blood_Skill.bloodLocked)
            {
                SkillManager.instance.blood_Skill.CanUseSkill();
            }
            if(Input.GetKeyDown(SkillManager.instance.attack_SpeedUp_Skill.keyCode) &&SkillManager.instance.attack_SpeedUp_Skill.attackSpeedUpWithHPAndLightingUnlocked)
            {
                SkillManager.instance.attack_SpeedUp_Skill.CanUseSkill();
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

