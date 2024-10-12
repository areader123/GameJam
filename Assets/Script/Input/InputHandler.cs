using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SK
{
    public class InputHandler : MonoBehaviour
    {
        public float horizonal =0f;
        public float vertical = 0f;

        public float moveAmount =0f;

        

        Vector2 movementInput;


        PlayerControl inputActions;

        private void OnEnable()
        {
            if (inputActions == null)
            {
                inputActions = new PlayerControl();
                inputActions.PlayerMovemnet.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
            
            }
            inputActions.Enable();
        }

        private void OnDisable()
        {
            inputActions.Disable();
        }

        public void  TickInput (float delta) 
        {
            MoveInput(delta);
        }

        private void MoveInput (float delta) 
        {
            horizonal = movementInput.x;
            vertical = movementInput.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizonal) + Mathf.Abs(vertical));
        }





    }
}

