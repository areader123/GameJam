using System;
using System.Collections;
using System.Collections.Generic;
using SK;
using UnityEngine;
using UnityEditor;

namespace SK
{

    public enum SkillName
    {
        Dash,
        Clone,
        SpeedUp,
        Bullet_Circle,
        Bullet_Fan,
        Light,
        blood,
        AttackSpeedUp
    }
    public enum SkillHitBack
    {
        can,canNot
    }
    public class Skill : MonoBehaviour
    {
        public SkillName skillName;
        public SkillHitBack skillHitBack;
        public KeyCode keyCode;
        public float cooldown;
        public float cooldowmTImer;

        public float cooldownDecrease;

        protected int cost;
        protected int Cost_HP;

        // Start is called before the first frame update
        protected virtual void Start()
        {
            CheckUnlock();
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            cooldowmTImer -= Time.deltaTime;
        }


        public virtual bool CanUseSkill()
        {
            if (cooldowmTImer < 0 && Character_Controller.instance.GetLightingNumber() >= cost && Character_Controller.instance.character.GetComponent<Character_Stat>()._currentHP > Cost_HP)
            {
                UseSkill();
                cooldowmTImer = cooldown;
                return true;
            }
            return false;

        }

        public virtual void UseSkill()
        {

        }

        protected virtual void CheckUnlock()
        {

        }
    }
}
