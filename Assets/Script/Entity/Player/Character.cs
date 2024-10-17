using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SK
{
    public class Character : Entity
    {
        public InputHandler inputHandler;
        Vector2 moveDirection;

        public Transform attackableTransform;
        public float attackRadius;
        [SerializeField] private UI uI;


        #region Player_State
        public StateMachine stateMachine { get; private set; }
        public Player_Idel_State player_Idel_State { get; private set; }
        public Player_Move_State player_Move_State { get; private set; }

        public Player_Dash_State player_Dash_State { get; private set; }

        public Player_Grounded_State player_Grounded_State { get; private set; }

        public Player_Attack_State player_Attack_State { get; private set; }
        public Player_Die_State player_Die_State { get; private set; }
        #endregion

        protected override void Awake()
        {
            base.Awake();
            stateMachine = new StateMachine();
            player_Idel_State = new Player_Idel_State("Idel", stateMachine, this);
            player_Move_State = new Player_Move_State("Move", stateMachine, this);
            player_Dash_State = new Player_Dash_State("Dash", stateMachine, this);
            player_Grounded_State = new Player_Grounded_State("Isground", stateMachine, this);
            player_Attack_State = new Player_Attack_State("Attack", stateMachine, this);
            player_Die_State = new Player_Die_State("Die", stateMachine, this);


            inputHandler = GetComponent<InputHandler>();
        }


        protected override void Start()
        {
            base.Start();
            stateMachine.Intialize(player_Idel_State);
        }

        protected override void Update()
        {
            float delta = Time.deltaTime;
            inputHandler.TickInput(delta);
            // Debug.Log("Vertical" +vertical);
            // Debug.Log("horizonal" + horizonal);
            //Debug.Log("inputHandler.vertical" + inputHandler.vertical);
            if (!uI.ifTimeStop || !isKoncked)
            {
                FlipControll(inputHandler.horizonal);
                stateMachine.currentstate.Update();
            }
        }
        public void AnimationTrigger() => stateMachine.currentstate.AnimationFinishTrigger();
        public override void Damage(Entity_Stat entity_Stat = null)
        {
            base.Damage(entity_Stat);
            fx.RedColorBlinkFor(.3f);
            if (entity_Stat != null)
            {
                if (!isKoncked && entity_Stat.canHitBack == CanHitBack.can)
                {
                    StartCoroutine("HitKnockback");
                }
            }
            Debug.Log("玩家受到伤害");
        }

        public void Destroy()
        {
            // Destroy(gameObject);
            //使用协程报错
            StopAllCoroutines();
            StartCoroutine("DestroySelf");
        }

        private IEnumerator DestroySelf()
        {
            yield return new WaitForSeconds(2f);
            Destroy(gameObject);
        }

        protected override void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(attackableTransform.position, attackRadius);
        }





    }
}

