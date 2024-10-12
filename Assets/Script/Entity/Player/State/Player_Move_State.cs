using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
namespace SK
{
    public class Player_Move_State : Player_Grounded_State
    {
        public Player_Move_State(string _animboolname, StateMachine _stateMachine, Character _character) : base(_animboolname, _stateMachine, _character)
        {
        }
        public override void Update()
        {
            base.Update();
            character.SetVelocity(character.inputHandler.horizonal,character.inputHandler.vertical,character.movementSpeed);
            MoveDirection();
            if (character.inputHandler.moveAmount == 0f)
            {
                stateMachine.ChangeState(character.player_Idel_State);
            }
        }

        public void MoveDirection()
        {
            character.horizonal = 0;
            character.vertical =0;
            
            if (character.inputHandler.horizonal > 0.55f)
            {
                character.horizonal = 1;
                character.last_face_reigon = 3;
            }
            if (character.inputHandler.horizonal < -0.55f)
            {
                character.horizonal = -1;
                character.last_face_reigon = 2;
            }
            if(character.inputHandler.horizonal <=0.55f && character.inputHandler.horizonal>=-0.55f)
            {
                character.horizonal = 0;
            }
            if (character.inputHandler.vertical > 0.55f)
            {
                character.vertical = 1;
                character.last_face_reigon = 0;
            }
            if (character.inputHandler.vertical < -0.55f)
            {
                character.vertical = -1;
                character.last_face_reigon = 1;
            }
            if(character.inputHandler.vertical <=0.55f && character.inputHandler.vertical>=-0.55f)
            {
                character.vertical = 0;
            }

            character.animator.SetFloat("Vertical",character.vertical,0.1f, Time.deltaTime);
            character.animator.SetFloat("Horizonal",character.horizonal,0.1f, Time.deltaTime);
        }
    }

}
