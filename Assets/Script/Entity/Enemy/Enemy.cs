using System;
using System.Collections;
using System.Collections.Generic;
using SK;
using UnityEngine;

namespace SK
{
    public class Enemy : Entity
    {
        //[SerializeField] public LayerMask WhatIsPlayer;

        [Header("TimeFreeze Info")]
        public bool timeFrozen;

        [Header("Stunned Info")]
        public float stunDuration;
        public Vector2 stunDirection;
        protected bool canBestunned;
        [SerializeField] protected GameObject counterImage;
        public EnemyStateMachine stateMachine { get; private set; }

        [Header("Move Info")]
        public float battleSpeed;
        public float MoveSpeed;
        //  public float battleTime;
        public float defaultMoveSpeed;

        [Header("Attack info")]
        public bool attackting;
        public bool attcakUnFollow;
        public float attackCooldown;
        [SerializeField] public float lastTimeAttack;
        [SerializeField] private Transform characterAttackedTransform;
        [SerializeField] private float characterAttackRadius;

        [Header("Detected info")]
        [SerializeField] private Transform characterDetectedTransform;
        [SerializeField] private float characterDetectedRadius;
        public Character charactersDetected;
        public Vector3 characterDirection;

        [Header("Battle Info")]
        [SerializeField]private Transform characterFightingWithTransform;
        [SerializeField]private float characterFightingWithRadius;
        [Header("Skill Info")]
        [SerializeField]public float skill_One_Cooldown;
        [SerializeField]public float lastTimeSkill_One;

        [SerializeField]public float skill_Two_Cooldown;
        [SerializeField]public float lastTimeSkill_Two;


        protected Enemy_Drop enemy_Drop;
        protected Enemy_Stat enemy_Stat;


        protected override void Awake()
        {
            base.Awake();
            stateMachine = new EnemyStateMachine();
            enemy_Drop = GetComponent<Enemy_Drop>();
            enemy_Stat = GetComponent<Enemy_Stat>();

        }
        protected override void Update()
        {
            base.Update();

            stateMachine.currentState.Update();
            if (!attcakUnFollow)
            {
                attackting = false;
            }
            IsCharacterDectected();
            CalculateDirection();
            if (IsCharacterDectected() && !enemy_Stat.isDead && !attackting)
                FlipControll(charactersDetected.transform.position.x - transform.position.x);
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

        public bool IsCharacterAttackable()
        {
            if (charactersDetected == null)
                return false;
            if (Vector2.Distance(charactersDetected.transform.position, characterAttackedTransform.position) < characterAttackRadius)
            {
                return true;
            }
            return false;
        }
        public bool IsCharacterFightingWith()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(characterFightingWithTransform.position, characterFightingWithRadius);
            if (colliders == null)
                return false;
            foreach (var hit in colliders)
            {
                if (hit.GetComponent<Character>() != null)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsCharacterDectected()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(characterDetectedTransform.position, characterDetectedRadius);
            if (colliders == null)
            {
                return false;
            }
            foreach (var hit in colliders)
            {
                if (charactersDetected != null)
                    break;
                if (hit.GetComponent<Character>() != null)
                {
                    charactersDetected = hit.GetComponent<Character>();
                }
            }
            if (charactersDetected == null)
            {
                return false;
            }
            if ((charactersDetected.transform.position - transform.position).magnitude >= characterDetectedRadius)
            {
                charactersDetected = null;
                return false;
            }
            return true;
        }

        private void CalculateDirection()
        {
            if (charactersDetected != null)
                characterDirection = (charactersDetected.transform.position - transform.position).normalized;
            else
                characterDirection = Vector3.zero;
        }

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            Gizmos.DrawWireSphere(characterDetectedTransform.position, characterDetectedRadius);
            Gizmos.DrawWireSphere(characterFightingWithTransform.position, characterFightingWithRadius);
            Gizmos.DrawWireSphere(characterAttackedTransform.position, characterAttackRadius);
            // Gizmos.DrawWireSphere(IsCharacterLosedTransform.position, characterLosedRadius);
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
        public bool CanAttack()
        {
            if (Time.time >= lastTimeAttack + attackCooldown)
            {
                return true;
            }
            return false;
        }

        public override void Damage(Skill skill = null,Entity_Stat entity_Stat = null)
        {
            int expression = 0;
            if (entity_Stat != null)
                expression = 0;
            if (skill != null)
                expression = 1;
            switch (expression)
            {
                case 0:
                    if (!isKoncked && entity_Stat.canHitBack == CanHitBack.can)
                    {

                        Debug.Log("entity_Stat.canHitBack");
                        StartCoroutine("HitKnockback");
                    }
                    break;
                case 1:
                    if (!isKoncked && skill.skillHitBack == SkillHitBack.can)
                    {
                        Debug.Log("skill.skillHitBack");
                        StartCoroutine("HitKnockback");
                    }
                    break;
                default:
                    break;
            }
            //敌人实体受击打效果
        }

        public override void Die()
        {
            base.Die();
            enemy_Drop.InstantiatePrefab();
        }
        protected override IEnumerator HitKnockback()
        {
            return base.HitKnockback();
        }



        public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();
    }
}
