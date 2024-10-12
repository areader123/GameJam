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
        [SerializeField] private UI_Skill_Slot dashUnlockButton;
        public float dashDuration;
        public float dashSpeed;
        protected override void Start()
        {
            base.Start();
            dashUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockDash);
        }

        public override void UseSkill()
        {
            base.UseSkill();
            if(dashUnlocked)
            {
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
            if (dashUnlockButton.unLock)
            {
                Debug.Log("成功");
                dashUnlocked = true;
            }
        }
    }
}

