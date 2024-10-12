using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SK
{
    public class Enemy_Stat : Entity_Stat
    {
        Enemy enemy;
        //ItemDrop itemDropSystem;
        public int _strength;
        public int _damage;


        protected override void Start()
        {
            enemy = GetComponent<Enemy>();
            //itemDropSystem = GetComponent<ItemDrop>();
            Modifier();
            base.Start();
        }

        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);

            enemy.Damage();
        }
        protected override void Die()
        {
            base.Die();
            //死亡方式
            // enemy_Skeleton.stateMachine
            // enemy_Skeleton.animator.speed = 0;
            // enemy_Skeleton.GetComponent<CapsuleCollider2D>().enabled = false;
            // //enemy_Skeleton.SetVelocity()
            // enemy_Skeleton.rb.gravityScale = 30;
            //死亡掉落
            //itemDropSystem.generateDrop();
        }
        //属性更改
        public void Modifier()
        {
            strength.AddModifiers(_strength);
            damage.AddModifiers(_damage);
        }
    }
}

