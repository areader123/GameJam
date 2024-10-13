using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SK
{
    public class Character : Entity
    {
        public InputHandler inputHandler;
        Vector2 moveDirection;

        public bool isbusy { get; private set; }


        #region Player_State
        public StateMachine stateMachine { get; private set; }
        public Player_Idel_State player_Idel_State { get; private set; }
        public Player_Move_State player_Move_State { get; private set; }

        public Player_Dash_State player_Dash_State { get; private set; }

        public Player_Grounded_State player_Grounded_State { get; private set; }
        #endregion

        protected override void Awake()
        {
            base.Awake();
            stateMachine = new StateMachine();
            player_Idel_State = new Player_Idel_State("Idel", stateMachine, this);
            player_Move_State = new Player_Move_State("Move", stateMachine, this);
            player_Dash_State = new Player_Dash_State("Dash", stateMachine, this);
            player_Grounded_State = new Player_Grounded_State("Isground", stateMachine, this);
            inputHandler = GetComponent<InputHandler>();
        }


        protected override void Start()
        {
            stateMachine.Intialize(player_Idel_State);
        }

        protected override void Update()
        {
            float delta = Time.deltaTime;
            inputHandler.TickInput(delta);

            // Debug.Log("Vertical" +vertical);
            // Debug.Log("horizonal" + horizonal);
            //Debug.Log("inputHandler.vertical" + inputHandler.vertical);
            stateMachine.currentstate.Update();
        }

        public override void Damage()
        {
            base.Damage();
            Debug.Log("玩家受到伤害");
        }

        public IEnumerator BusyFor(float _seconds)
        {
            isbusy = true;
            yield return new WaitForSeconds(_seconds);
            isbusy = false;
        }
    }
}

