using System.Collections;
using System.Collections.Generic;
using SK;
using UnityEngine;
namespace SK
{
    public class Character_Stat : Entity_Stat
    {
        Character character;
        [Header("OutOfLighting Info")]
        [SerializeField][Range(0,1)]private float HealthPercentageDecreased;
        [SerializeField]private float timeDuration;
        private float timeCounter;
        protected override void Start()
        {
            base.Start();
            character = GetComponent<Character>();
        }

        public override void TakeDamage(float damage,Skill skill)
        {
            base.TakeDamage(damage,skill);
            if (!isDead)
            {
                character.Damage(skill);
            }
        }

        public override void DoDamage(Entity_Stat target)
        {
            base.DoDamage(target);
            
            if (!isDead)
            {
                character.Damage(null,target);
            }
        }

        public override void DoMagicDamage(Entity_Stat target,Skill skill)
        {
            base.DoMagicDamage(target,skill);
            if (!isDead)
            {
                character.Damage(null, target);
            }
        }

        protected override void Die()
        {
            base.Die();
            //死亡状态
            
            character.stateMachine.ChangeState(character.player_Die_State);
            character.OpenDead();   
            isDead = true;
        }

        protected override void DecreaseHealthOnly(float damage)
        {
            base.DecreaseHealthOnly(damage);
            //当受伤害时 调用防具的效果
            // Item_Equipment armor = Inventor.instance.GetSingleEquipment(Equipment.Armor);
            // if(armor != null)
            // {
            //     armor.Effect(PlayerManger.instance.player.transform);
            // }
        }

       

        protected override void Update()
        {
            base.Update();
            if(Character_Controller.instance.GetLightingNumber() <= 0)
            {
                timeCounter -= Time.deltaTime; 
                if(timeCounter <= 0 )
                {
                    DecreaseHealthOnly(HealthPercentageDecreased);
                    timeCounter = timeDuration;
                }
            }
        }
    }
}

