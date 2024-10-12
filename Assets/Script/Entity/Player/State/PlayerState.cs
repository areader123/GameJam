using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SK
{
    public class PlayerState
    {
        private string animboolname;

        protected float xinput;
        protected float yinput;
        protected StateMachine stateMachine;
        protected Character character;
        public float stateTimer;
        // ��stateTimer ����������ͨ�õ� һ��ĳ����ʹ���� ���Թ���һ�������� ��Ϊ�������¸����ำֵ������� false // dash ������ȴ ����
        //ͬһstatetimer �Ḳ��ԭ�е�statetimer 
        protected bool triggercalled;

        public PlayerState(string _animboolname, StateMachine _stateMachine, Character _character)
        {
            this.animboolname = _animboolname;
            this.stateMachine = _stateMachine;
            this.character = _character;
        }

        public virtual void Enter()
        {
            character.animator.SetBool(animboolname, true);
            Debug.Log("I am entering" + animboolname);
            triggercalled = false;
        }
        public virtual void Update()
        {
            xinput = Input.GetAxisRaw("Horizontal");
            yinput = Input.GetAxisRaw("Vertical");
             Debug.Log("I am in"+animboolname);
            stateTimer -= Time.deltaTime;
        }
        public virtual void Exit()
        {
            character.animator.SetBool(animboolname, false);
             Debug.Log("I am exit" + animboolname);
        }
        public virtual void AnimationFinishTrigger()
        {
            triggercalled = true;
        }
    }
}

