using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SK
{
    public class SkillManager : MonoBehaviour
    {
        public static SkillManager instance;

        public Dash_Skill dash_Skill { get; private set; }
        public Clone_Skill clone_Skill { get; private set; }
        public ChangeWithEnemy_Skill changeWithEnemy_Skill{ get; private set; }
        public Bullet_Skill bullet_Skill ;
        public Bullet_Fan_Skill bullet_Fan_Skill;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(instance);
            }
            else
            {
                instance = this;
            }
        }

        private void Start()
        {
            dash_Skill = GetComponent<Dash_Skill>();
            clone_Skill = GetComponent<Clone_Skill>();
            changeWithEnemy_Skill = GetComponent<ChangeWithEnemy_Skill>();
            bullet_Skill = GetComponent<Bullet_Skill>();
            bullet_Fan_Skill = GetComponent<Bullet_Fan_Skill>();
        }
      
        public Skill GetSkillByName(SkillName skillName)
        {
            if(skillName == SkillName.Dash)
            {
                return dash_Skill;
            }
            if(skillName == SkillName.Clone)
            {
                return clone_Skill;
            }
            if(skillName == SkillName.SpeedUp)
            {
                return changeWithEnemy_Skill;
            }
            if(skillName == SkillName.Bullet_Circle)
            {
                return bullet_Skill;
            }
            if(skillName == SkillName.Bullet_Fan)
            {
                return bullet_Fan_Skill;
            }
            return null;
        }
    }
}

