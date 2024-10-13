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
        // public SkeletonBattleState Skeleton_BattleState {  get; private set; }
        // public SkeletonAttackState Skeleton_AttackState {  get; private set; }  
        // public SkeletonStunnedState Skeleton_StunnedState { get; private set; } 
        protected override void Awake()
        {
            base.Awake();
             Skeleton_IdolState = new SkeletonIdolState(this,stateMachine,"Idol",this);
             Skeleton_MoveState = new SkeletonMoveState(this,stateMachine,"Move",this);
            // Skeleton_BattleState = new SkeletonBattleState(this, stateMachine, "Move", this);
            // Skeleton_AttackState = new SkeletonAttackState(this, stateMachine, "Attack", this);
            // Skeleton_StunnedState = new SkeletonStunnedState(this, stateMachine, "Stunned", this);
        }

        protected override void Start()
        {
            base.Start();
            stateMachine.Intialize(Skeleton_IdolState);
        }

        protected override void Update()
        {
            //FlipController(rb.velocity.x);
            base.Update();
            // if (Input.GetKeyDown(KeyCode.R))
            // {
            //     stateMachine.ChangeState(Skeleton_StunnedState);
            // }
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

        
    }
}
