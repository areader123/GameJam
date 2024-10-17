using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
            if (!isDead)
            {
                enemy.Damage();
            }
        }

        public override void DoDamage(Entity_Stat target)
        {
            base.DoDamage(target);
            if(!isDead)
            {
                enemy.Damage(target);
            }
        }



        protected override void Die()
        {
            base.Die();
            enemy.Die();
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

