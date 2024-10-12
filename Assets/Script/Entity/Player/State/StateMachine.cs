using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SK
{
    public class StateMachine
    {
    public PlayerState currentstate;
    public void Intialize(PlayerState _startstate)
    {
        currentstate = _startstate;
        currentstate.Enter();
    }

    public void ChangeState(PlayerState _newstate)
    {
        currentstate.Exit();
        currentstate = _newstate;
        currentstate.Enter();
    }
    }

}
