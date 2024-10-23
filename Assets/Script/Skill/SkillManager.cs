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
        public ChangeWithEnemy_Skill changeWithEnemy_Skill { get; private set; }
        public Bullet_Skill bullet_Skill;
        public Bullet_Fan_Skill bullet_Fan_Skill;
        public Light_Skill light_Skill;
        public Blood_Skill blood_Skill;
        public Attack_SpeedUp_Skill attack_SpeedUp_Skill;

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
            dash_Skill = GetComponent<Dash_Skill>();
            clone_Skill = GetComponent<Clone_Skill>();
            changeWithEnemy_Skill = GetComponent<ChangeWithEnemy_Skill>();
            bullet_Skill = GetComponent<Bullet_Skill>();
            bullet_Fan_Skill = GetComponent<Bullet_Fan_Skill>();
            light_Skill = GetComponent<Light_Skill>();
            blood_Skill = GetComponent<Blood_Skill>();
            attack_SpeedUp_Skill = GetComponent<Attack_SpeedUp_Skill>();
        }

        private void Start()
        {

        }

        public Skill GetSkillByName(SkillName skillName)
        {
            if (skillName == SkillName.Dash)
            {
                return dash_Skill;
            }
            if (skillName == SkillName.Clone)
            {
                return clone_Skill;
            }
            if (skillName == SkillName.SpeedUp)
            {
                return changeWithEnemy_Skill;
            }
            if (skillName == SkillName.Bullet_Circle)
            {
                return bullet_Skill;
            }
            if (skillName == SkillName.Bullet_Fan)
            {
                return bullet_Fan_Skill;
            }
            if (skillName == SkillName.Light)
            {
                return light_Skill;
            }
            if (skillName == SkillName.blood)
            {
                return blood_Skill;
            }
            if (skillName == SkillName.AttackSpeedUp)
            {
                return attack_SpeedUp_Skill;
            }
            return null;
        }


        public Skill GetSkillByStringName(string skillName)
        {
            if (skillName == SkillName.Dash.ToString())
            {
                return dash_Skill;
            }
            if (skillName == SkillName.Clone.ToString())
            {
                return clone_Skill;
            }
            if (skillName == SkillName.SpeedUp.ToString())
            {
                return changeWithEnemy_Skill;
            }
            if (skillName == SkillName.Bullet_Circle.ToString())
            {
                return bullet_Skill;
            }
            if (skillName == SkillName.Bullet_Fan.ToString())
            {
                return bullet_Fan_Skill;
            }
            if (skillName == SkillName.Light.ToString())
            {
                return light_Skill;
            }
            if (skillName == SkillName.blood.ToString())
            {
                return blood_Skill;
            }
            if (skillName == SkillName.AttackSpeedUp.ToString())
            {
                return attack_SpeedUp_Skill;
            }
            return null;
        }


    }
}

