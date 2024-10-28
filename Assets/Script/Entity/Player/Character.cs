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
        public CapsuleCollider2D capsuleCollider2D;
        public Transform destroyTransform;
        public float destroyRadius;
        [SerializeField] private float unHittableDuration;

        #region Player_State
        public StateMachine stateMachine { get; private set; }
        public Player_Idel_State player_Idel_State { get; private set; }
        public Player_Move_State player_Move_State { get; private set; }

        public Player_Dash_State player_Dash_State { get; private set; }

        public Player_Grounded_State player_Grounded_State { get; private set; }

        public Player_Attack_State player_Attack_State { get; private set; }
        public Player_Die_State player_Die_State { get; private set; }
        public Player_Move_Attack_State player_Move_Attack_State { get; private set; }
        #endregion

        protected override void Awake()
        {
            base.Awake();
            stateMachine = new StateMachine();
            player_Idel_State = new Player_Idel_State("Idel", stateMachine, this);
            player_Move_State = new Player_Move_State("Move", stateMachine, this);
            player_Dash_State = new Player_Dash_State("Dash", stateMachine, this);
            player_Grounded_State = new Player_Grounded_State("Isground", stateMachine, this);
            player_Attack_State = new Player_Attack_State("Stand_Attack", stateMachine, this);
            player_Die_State = new Player_Die_State("Die", stateMachine, this);
            player_Move_Attack_State = new Player_Move_Attack_State("Move_Attack", stateMachine, this);

            capsuleCollider2D = GetComponent<CapsuleCollider2D>();
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
            if (uI != null)
            {

                if (!uI.ifTimeStop && !isKoncked)
                {
                    if (!GetComponent<Character_Stat>().isDead)
                    {
                        stateMachine.currentstate.Update();
                        FlipControll(inputHandler.horizonal);
                    }
                }
            }
        }
        public void AnimationTrigger() => stateMachine.currentstate.AnimationFinishTrigger();
        public override void Damage(Skill skill = null, Entity_Stat entity_Stat = null)
        {
            base.Damage(skill, entity_Stat);
            fx.Entity_FX_White();
            //fx.RedColorBlinkFor(.3f);
            int expression = 0;
            if (entity_Stat != null)
                expression = 0;
            if (skill != null)
                expression = 1;
            if (skill == null && entity_Stat == null)
            {
                return;
            }
            switch (expression)
            {
                case 0:
                    if (!isKoncked && entity_Stat.canHitBack == CanHitBack.can)
                    {
                        //  StopCoroutine("HitKnockbackCharacter");
                        StartCoroutine("HitKnockbackCharacter");
                        // StopCoroutine("HitKnockbackCharacterCollider2D");
                        // StartCoroutine("HitKnockbackCharacterCollider2D");
                    }
                    break;
                case 1:
                    if (!isKoncked && skill.skillHitBack == SkillHitBack.can)
                    {
                        // StopCoroutine("HitKnockbackCharacter");
                        // StartCoroutine("HitKnockbackCharacter");
                        //  StopCoroutine("HitKnockbackCharacterCollider2D");
                        // StartCoroutine("HitKnockbackCharacterCollider2D");
                    }
                    break;
                default:
                    break;
            }
        }

        public void Destroy()
        {
           // Debug.Log("siwang");
            //StopAllCoroutines();
            Destroy(gameObject);
            // StartCoroutine("DestroySelf");
        }

        private IEnumerator DestroySelf()
        {
            yield return new WaitForSeconds(0.1f);

        }

        // protected override void OnDrawGizmos()
        // {
        //     Gizmos.DrawWireSphere(attackableTransform.position, attackRadius);
        //     Gizmos.DrawWireSphere(destroyTransform.position, destroyRadius);
        // }


        protected IEnumerator HitKnockbackCharacter()
        {
            isKoncked = true;
            animator.speed = 0;
            rb.velocity = new Vector2(konckedSpeed * (-faceDir), 0);
            //  capsuleCollider2D.enabled = false;
            yield return new WaitForSeconds(konckbackDuration);
            //   capsuleCollider2D.enabled = true;
            rb.velocity = Vector2.zero;
            animator.speed = 1;
            isKoncked = false;
        }
        protected IEnumerator HitKnockbackCharacterCollider2D()
        {
            capsuleCollider2D.enabled = false;
            yield return new WaitForSeconds(unHittableDuration);
            capsuleCollider2D.enabled = true;
        }

        public void OpenDead()
        {
            uI.OpenDead();
        }

    }
}

