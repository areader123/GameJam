using System.Collections;
using System.Collections.Generic;
using SK;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
namespace SK
{

    public class Player_Idel_State : Player_Grounded_State
    {
        public Player_Idel_State(string _animboolname, StateMachine _stateMachine, Character _character) : base(_animboolname, _stateMachine, _character)
        {
        }
        public override void Update()
        {
            base.Update();
            if (character.inputHandler.moveAmount != 0f)
            {
                stateMachine.ChangeState(character.player_Move_State);
            }
            if(Mouse.current.leftButton.wasPressedThisFrame)
            {
                stateMachine.ChangeState(character.player_Attack_State);
            }
        }
        public override void Enter()
        {
            base.Enter();
            IdelDirection();
        }

        private void IdelDirection () 
        {
            // character.animator.SetFloat("X_Face",character.horizonal,0.1f,Time.deltaTime);
            // character.animator.SetFloat("Y_Face",character.vertical,0.1f,Time.deltaTime);
        }

        
    }
}
