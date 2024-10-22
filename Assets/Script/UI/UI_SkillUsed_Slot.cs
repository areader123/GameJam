using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace SK
{

    public class UI_SkillUsed_Slot : MonoBehaviour
    {
        [SerializeField] public UI_Skill_Slot uI_Skill_Slot;
        [SerializeField] private UI_SkillTree_Slot uI_SkillTree_Slot;
        private Image image;
        public bool Unlock;
        public Skill skill;
        public SkillName skillName;
        private void Awake()
        {   
            uI_Skill_Slot = GetComponentInChildren<UI_Skill_Slot>();
            uI_SkillTree_Slot = GetComponentInParent<UI_SkillTree_Slot>();
            image = GetComponent<Image>();
            image.color = new Vector4(1, 1, 1, 0);
        }
        private void Update() 
        {
            if(skill ==null)
            {
              skill = SkillManager.instance.GetSkillByName(skillName);
            }
        }
        void Start()
        {
            skill = SkillManager.instance.GetSkillByName(skillName);
        }

        public void ShowSkillUsedSlot()
        {
            image.color = new Vector4(1, 1, 1, 1);
            Unlock = true;
            uI_SkillTree_Slot.AddQueue(this);

        }

        public void HideSkillUsedSlot()
        {
            image.color = new Vector4(1, 1, 1, 0);
            Unlock = false;
        }



    }

}