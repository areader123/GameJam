using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SK;
namespace SK
{

    public class Enemy_Skeleton : Enemy
    {
        public SkeletonIdolState Skeleton_IdolState { get; private set; }
        public SkeletonMoveState Skeleton_MoveState { get; private set; }
        public SkeletonBattleState Skeleton_BattleState { get; private set; }
        public SkeletonAttackState Skeleton_AttackState { get; private set; }
        public SkeletonDieState Skeleton_DieState { get; private set; }
        // public SkeletonStunnedState Skeleton_StunnedState { get; private set; } 
        public Skeleton_Skill_Attack_State skeleton_Skill_Attack_State;
        public bool CanskillUsedInBattleRange;
        public bool ifHaveSkill;
        protected override void Awake()
        {
            base.Awake();
            Skeleton_IdolState = new SkeletonIdolState(this, stateMachine, "Idel", this);
            Skeleton_MoveState = new SkeletonMoveState(this, stateMachine, "Move", this);
            Skeleton_BattleState = new SkeletonBattleState(this, stateMachine, "Battle", this);
            Skeleton_AttackState = new SkeletonAttackState(this, stateMachine, "Attack", this);
            Skeleton_DieState = new SkeletonDieState(stateMachine, this, "Die", this);
            skeleton_Skill_Attack_State = new Skeleton_Skill_Attack_State(stateMachine, this, "Skill_Attack", this);

            // Skeleton_StunnedState = new SkeletonStunnedState(this, stateMachine, "Stunned", this);
        }

        protected override void Start()
        {
            base.Start();
            stateMachine.Intialize(Skeleton_IdolState);
        }

        protected override void Update()
        {
            base.Update();
        }
        public override bool CanBeStunned()
        {
            if (base.CanBeStunned())
            {
                //stateMachine.ChangeState(Skeleton_StunnedState);
                return true;
            }
            return false;
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
            base.Die();
            stateMachine.ChangeState(Skeleton_DieState);
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        public bool DoSkill_One()
        {

            if (Time.time >= lastTimeSkill_One + skill_One_Cooldown)
            {
                lastTimeSkill_One = Time.time;
                return true;
            }
            return false;
        }


    }
}
