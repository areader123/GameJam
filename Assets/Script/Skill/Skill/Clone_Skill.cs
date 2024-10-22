using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SK
{
    public class Clone_Skill : Skill
    {
        public bool cloneUnlocked;
        public bool cloneSkillUsedUnlock;
        [SerializeField] private UI_Skill_Slot cloneUnlockButton;
        [SerializeField] private UI_SkillUsed_Slot uI_SkillUsed_Slot;
        [Header("Clone Info")]
        [SerializeField] private GameObject clonePrefab;
        [SerializeField] private float cloneDuration;
        [SerializeField] private bool canAttack;
        private Clone_Skill_Controller clone_Skill_Controller;
        private Character character;

        protected override void Start()
        {
            base.Start();
             character = Character_Controller.instance.character;
            
            cloneUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockClone);
        }

        public void CreatClone(Transform _cloneTransform, Vector3 _offset)
        {
            GameObject newClone = Instantiate(clonePrefab);
            clone_Skill_Controller = newClone.GetComponent<Clone_Skill_Controller>();
            clone_Skill_Controller.SetUpClone(_cloneTransform, cloneDuration, _offset);
        }

        public override void UseSkill()
        {
            base.UseSkill();
            if (cloneUnlocked && uI_SkillUsed_Slot.Unlock)
            {
                cloneSkillUsedUnlock = uI_SkillUsed_Slot.Unlock;
                CreatClone(character.transform,Vector3.zero);
            }
        }

        public void UseByOutside()
        {
            CreatClone(character.transform,Vector3.zero);
        }

        protected override void CheckUnlock()
        {
            UnlockClone();
        }

        public void UnlockClone()
        {
            if (cloneUnlockButton.unLock)
            {
                cloneUnlocked =true;
            }
        }
    }
}

