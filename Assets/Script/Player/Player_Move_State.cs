using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
namespace SK
{
    public class Player_Move_State : PlayerState
    {
        public Player_Move_State(string _animboolname, StateMachine _stateMachine, Character _character) : base(_animboolname, _stateMachine, _character)
        {
        }
        public override void Update()
        {
            base.Update();
            character.SetVelocity(character.inputHandler.horizonal,character.inputHandler.vertical,character.movementSpeed);
            MoveDirection();
            if (character.inputHandler.moveAmount <= 0.1f)
            {
                stateMachine.ChangeState(character.player_Idel_State);
            }
        }

        public void MoveDirection()
        {
            int v =0,
             h = 0;
            
            if (character.inputHandler.horizonal > 0.55f)
            {
                h = 1;
            }
            if (character.inputHandler.horizonal < -0.55f)
            {
                h = -1;
            }
            if (character.inputHandler.vertical > 0.55f)
            {
                v = 1;
            }
            if (character.inputHandler.vertical < -0.55f)
            {
                v = -1;
            }

            character.animator.SetFloat("Vertical",v,0.1f, Time.deltaTime);
            character.animator.SetFloat("Horizonal",h,0.1f, Time.deltaTime);
        }
    }

}
