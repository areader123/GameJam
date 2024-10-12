using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SK
{
    public class Clone_Skill : Skill
    {
        public bool cloneUnlocked;
        [SerializeField] private UI_Skill_Slot cloneUnlockButton;
        [Header("Clone Info")]
        [SerializeField] private GameObject clonePrefab;
        [SerializeField] private float cloneDuration;
        [SerializeField] private bool canAttack;
        private Clone_Skill_Controller clone_Skill_Controller;

        protected override void Start()
        {
            base.Start();
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
            if (cloneUnlocked)
            {
                CreatClone(character.transform,Vector3.zero);
            }
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

