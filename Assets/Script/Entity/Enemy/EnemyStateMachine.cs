using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SK
{

    public class EnemyStateMachine
    {
        public EnemyState currentState { get; private set; }

        public EnemyState lastState { get; private set; }

        public void Intialize(EnemyState _starstate)
        {
            currentState = _starstate;
            currentState.Enter();
        }
        public void ChangeState(EnemyState _newstate)
        {
            currentState.Exit();
            currentState = _newstate;
            currentState.Enter();
        }
    }
}
