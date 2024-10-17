using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SK
{

    public class Enemy_Robot : Enemy
    {
        public Robot_Attack_State robot_Attack_State;
        public Robot_Dead_State robot_Dead_State;
        //public Robot_Grounded_State robot_Grounded_State;
        public Robot_Idel_State robot_Idel_State;
        public Robot_Skill_Attack_State robot_Skill_Attack_State;
        public Robot_Walk_State robot_Walk_State;
        public Robot_Battle_State robot_Battle_State;

        // Start is called before the first frame update
        protected override void Awake()
        {
            base.Awake();
            robot_Attack_State = new Robot_Attack_State(stateMachine,this,"Attack",this);
            robot_Dead_State = new Robot_Dead_State(stateMachine,this,"Dead",this);
            robot_Idel_State = new Robot_Idel_State(stateMachine,this,"Idel", this);
            robot_Walk_State = new Robot_Walk_State(stateMachine,this,"Walk",this);
            robot_Skill_Attack_State = new Robot_Skill_Attack_State(stateMachine,this,"Skill_Attack",this);
            robot_Battle_State = new Robot_Battle_State(stateMachine,this,"Battle",this);
        }
        protected override void Start()
        {
            base.Start();
            stateMachine.Intialize(robot_Idel_State);
        }

        public override void Damage()
        {
            base.Damage();
            fx.RedColorBlinkFor(.3f);
        }

         public override void Die()
        {
            base.Die();
            stateMachine.ChangeState(robot_Dead_State);
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

    }
}
