using System.Collections;
using System.Collections.Generic;
using SK;
using UnityEngine;

namespace SK
{
    public class Enemy : Entity
    {
        [SerializeField] public LayerMask WhatIsPlayer;

        [Header("TimeFreeze Info")]
        public bool timeFrozen;

        [Header("Stunned Info")]
        public float stunDuration;
        public Vector2 stunDirection;
        protected bool canBestunned;
        [SerializeField] protected GameObject counterImage;
        public EnemyStateMachine stateMachine { get; private set; }

        [Header("Move Info")]
        public float MoveSpeed;
        public float battleTime;
        public float defaultMoveSpeed;

        [Header("Attack info")]
        public float attackDistance;
        public float attackCooldown;
        [SerializeField] public float lastTimeAttack;

        [Header("Detected info")]
        [SerializeField]private Transform IsCharacterDetectedTransform;
        public float IsCharacterRadius;
        public List<Character> charactersDetected;


        protected override void Awake()
        {
            base.Awake();
            stateMachine = new EnemyStateMachine();
            charactersDetected = new List<Character>();
        }
        protected override void Update()
        {
            base.Update();

            stateMachine.currentState.Update();

            IsCharacterDectected();
            // if (IsPlayerDetected())
            // {
            // }
            // if (!isKoncked)
            //{
            // FlipController(rb.velocity.x);
            //  }
        }

        //指攻击时可被打算
        public virtual void OpenCounterAttackWindow()
        {

            canBestunned = true;
            counterImage.SetActive(true);
        }
        public virtual void CloseCounterAttackWindow()
        {
            canBestunned = false;
            counterImage.SetActive(false);
        }

        public virtual bool CanBeStunned()
        {
            if (canBestunned)
            {
                CloseCounterAttackWindow();
                return true;
            }
            return false;
        }

        public virtual void FreezeTime(bool _timeFrozen)
        {
            if (_timeFrozen)
            {
                MoveSpeed = 0;
                animator.speed = 0;
                timeFrozen = _timeFrozen;
            }
            else
            {
                MoveSpeed = defaultMoveSpeed;
                animator.speed = 1;
            }
        }
        public virtual void FreezeTimeFor(float _duration) => StartCoroutine(FreezeTimerCoroutine(_duration));
        public virtual IEnumerator FreezeTimerCoroutine(float _second)
        {
            FreezeTime(true);
            yield return new WaitForSeconds(_second);
            FreezeTime(false);
        }

        public bool IsCharacterDectected()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(IsCharacterDetectedTransform.position, IsCharacterRadius);
            if(colliders == null)
            return false;
            foreach (var hit in colliders)
            {
                if(hit.GetComponent<Character>() != null)
                {
                    charactersDetected.Add(hit.GetComponent<Character>());
                }
            }
            if(charactersDetected == null)
            return false;
            else
            return true;
        }

        public override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            Gizmos.DrawWireSphere(IsCharacterDetectedTransform.position, IsCharacterRadius);
        }

        //射线检测
        // public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(wallcheck.position, Vector2.right * faceDir, 50, WhatIsPlayer);

        // public override void OnDrawGizmos()
        // {
        //     base.OnDrawGizmos();
        //     Gizmos.color = Color.yellow;
        //     Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackDistance * faceDir, transform.position.y));
        // }
        //������enemystate�еķ�����¶��enemy�� ���� ������skeletonAnimationtriggers ������triggers������ʹ�� ���Ҹ������˵�triggers��Ҫ���ڶ����ϵ�
        public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();
    }
}
