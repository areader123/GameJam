using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DamageNumbersPro;
namespace SK
{
    public class Enemy_Stat : Entity_Stat
    {
        Enemy enemy;
        //ItemDrop itemDropSystem;
        public int perLevelIncreaseMonsterStrength;
        public int perLevelIncreaseMonsterDamage;
        


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
            }
        }
        //属性更改
        public void Modifier()
        {
            int level = Character_Controller.instance.GetLevel();
            strength.AddModifiers(perLevelIncreaseMonsterStrength * level);
            damage.AddModifiers(perLevelIncreaseMonsterDamage * level);
        }
    }
}

