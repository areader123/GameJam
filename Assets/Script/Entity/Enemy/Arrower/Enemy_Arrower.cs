using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SK
{
    public class Enemy_Arrower : Enemy
    {
        public Arrower_Attack_State arrower_Attack_State { get; private set; }
        public Arrower_Battle_State arrower_Battle_State { get; private set; }
        public Arrower_Idel_State arrower_Idel_State { get; private set; }
        public Arrower_Move_State arrower_Move_State { get; private set; }
        public Arrower_Dead_State arrower_Dead_State { get; private set; }
        public Arrower_Hit_State arrower_Hit_State { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            arrower_Attack_State = new Arrower_Attack_State(stateMachine, this, "Attack", this);
            arrower_Battle_State = new Arrower_Battle_State(stateMachine, this, "Battle", this);
            arrower_Idel_State = new Arrower_Idel_State(stateMachine, this, "Idel", this);
            arrower_Move_State = new Arrower_Move_State(stateMachine, this, "Move", this);
            arrower_Dead_State = new Arrower_Dead_State(stateMachine, this, "Dead", this);
            arrower_Hit_State = new Arrower_Hit_State(stateMachine, this, "Hit", this);
        }

        protected override void Start()
        {
            base.Start();
            stateMachine.Intialize(arrower_Idel_State);
        }

        protected override void Update()
        {
            base.Update();
        }
        public override void Damage(Skill skill, Entity_Stat entity_Stat)
        {
            if (!enemy_Stat.isDead)
            {
                base.Damage(skill, entity_Stat);
                fx.RedColorBlinkFor(.3f);
            }
        }
        public override void Die()
        {
            //无死亡状态的话 无法启用物品掉落
            base.Die();
            stateMachine.ChangeState(arrower_Dead_State);
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }


    }
}
