using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SK
{
    public class Dash_Skill : Skill
    {

        public bool dashUnlocked;
        public bool dashSkillUsedUnlocked;

        [SerializeField] public UI_Skill_Slot dash_UI_Skill_Slot;
        [SerializeField] private UI_SkillUsed_Slot uI_SkillUsed_Slot;
        public float dashDuration;
        public float dashSpeed;
        private Character character;
        protected override void Start()
        {
            base.Start();
            character = Character_Controller.instance.character;
            dash_UI_Skill_Slot.GetComponent<Button>().onClick.AddListener(UnlockDash);
        }

        public override void UseSkill()
        {
            base.UseSkill();
            if (dashUnlocked && uI_SkillUsed_Slot.Unlock)
            {
                dashSkillUsedUnlocked = uI_SkillUsed_Slot.Unlock;
                character.stateMachine.ChangeState(character.player_Dash_State);
            }
            // character.stateMachine.ChangeState();
        }


        protected override void CheckUnlock()
        {
            UnlockDash();
        }

        public void UnlockDash()
        {
            Debug.Log("尝试");
            if (dash_UI_Skill_Slot.unLock)
            {
                Debug.Log("成功");
                dashUnlocked = true;
            }
        }
    }
}

