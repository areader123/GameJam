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
        SpeedUp
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
        protected float cooldowmTImer;

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
            if (cooldowmTImer < 0)
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
