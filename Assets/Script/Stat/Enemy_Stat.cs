using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DamageNumbersPro;
using System;
namespace SK
{
    public class Enemy_Stat : Entity_Stat
    {
        Enemy enemy;
        //ItemDrop itemDropSystem;
        public int perLevelIncreaseMonsterStrength;
        public int perLevelIncreaseMonstermaxHP;

        protected override void Update()
        {
            base.Update();
           
        }

      
        private void Awake() {
        }

        protected override void Start()
        {
            enemy = GetComponent<Enemy>();
            Modifier();
            base.Start();
        }

        public override void TakeDamage(float damage, Skill skill)
        {
            base.TakeDamage(damage, skill);
            if (!isDead)
            {
                enemy.Damage(skill);
            }
        }

        public override void DoDamage(Entity_Stat target)
        {
            base.DoDamage(target);
            int totalDamage = CaculateAttack(target);
            //被动吸血
            int blood = (int)(totalDamage * target.GetStat(StatType.Blood).GetValue() / 100);
            Debug.Log("blood" + blood);
            Character_Controller.instance.character.GetComponent<Character_Stat>().IncreaseHealthOnly(blood);
            SkillManager.instance.blood_Skill.BloodToLighting(totalDamage);
            SkillManager.instance.blood_Skill.MoreLightingMoreDuration(totalDamage);
            //玩家主动吸血
            if (!isDead)
            {
                enemy.Damage(null, target);
            }

        }
        public override void DoMagicDamage(Entity_Stat target)
        {
            base.DoMagicDamage(target);
            if (!isDead)
            {
                enemy.Damage(null, target);
            }
        }



        protected override void Die()
        {
            base.Die();
            if (!isDead)
            {
                enemy.Die();
                isDead = true;
                Debug.Log("怪物死亡");
                Character_Controller.instance.ifEnemyKilled = true;
            }
        }
        //属性更改
        public void Modifier()
        {
            int level = Character_Controller.instance.GetLevel();
            strength.AddModifiers(perLevelIncreaseMonsterStrength * level);
            maxHP.AddModifiers(perLevelIncreaseMonstermaxHP * level);
        }
    }
}

