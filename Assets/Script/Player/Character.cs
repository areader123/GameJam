using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SK
{
    public class Character : Entity
    {
        public InputHandler inputHandler;
        Vector2 moveDirection;

        [HideInInspector]
        public Transform myTansform;


        #region Player_State
        public StateMachine stateMachine {get;private set;}
        public Player_Idel_State player_Idel_State{get;private set;}
        public Player_Move_State player_Move_State{get;private set;}
        #endregion

        protected override void Awake()
        {
            base.Awake();
            stateMachine = new StateMachine();
            player_Idel_State = new Player_Idel_State("Idel",stateMachine,this);
            player_Move_State = new Player_Move_State("Move",stateMachine,this);
            inputHandler = GetComponent<InputHandler>();
        }


        protected override void  Start() {
            stateMachine.Intialize(player_Idel_State);
            myTansform = transform;
        }

        protected override void Update() {
            float delta = Time.deltaTime;
            inputHandler.TickInput(delta);
            //Debug.Log("Vertical" +vertical);
            //Debug.Log("horizonal" + horizonal);
            //Debug.Log("inputHandler.vertical" + inputHandler.vertical);
            stateMachine.currentstate.Update();
        }
    }
}

