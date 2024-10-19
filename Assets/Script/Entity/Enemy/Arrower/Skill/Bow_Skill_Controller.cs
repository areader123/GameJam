using System.Collections;
using System.Collections.Generic;
using SK;
using UnityEngine;

namespace SK
{

    public class Bow_Skill_Controller : MonoBehaviour
    {
        private float arrowDamage;

        private float arrowExistTime;

        private float timeCounter;

        private float arrowSpeed;
        private Vector3 offset;

        private Character target;
        private Enemy orignTarget;

        private Rigidbody2D rb;
        private float damagepPerTime;
        private float damageTimeCounter;
        private bool destroySelfAfterDamage;
        private bool RotationWhileDamage;
        private Skill skill;
        private bool slowRotationWhileDamage;
        private float slowRotationSpeed;
        private Transform InstantiateTransform;
        private CapsuleCollider2D capsuleCollider2D;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        }
        void Start()
        {
            timeCounter = arrowExistTime;
        }
        private void Update()
        {
            timeCounter -= Time.deltaTime;
            damageTimeCounter -= Time.deltaTime;
            if (timeCounter < 0)
            {
                Destroy(gameObject);
            }
            if (RotationWhileDamage)
            {

                if (slowRotationWhileDamage)
                {
                    SlowRotation();
                }
                else
                {
                    SetRotation();
                }
            }
        }

        private void SetVelocity()
        {
            Vector2 direction = Character_Controller.instance.character.transform.position - (orignTarget.transform.position + offset);
            rb.velocity = direction.normalized * arrowSpeed;
        }

        private void SetRotation()
        {
            transform.LookAt(target.transform.position);
            // Quaternion newQuaternion = Quaternion.LookRotation(_target.transform.position - _orignTarget.transform.position);
            // transform.rotation = newQuaternion;
            transform.Rotate(0, -90, 0);
        }
        private void SlowRotation()
        {
            Vector3 direction = target.transform.position - transform.position;
            Vector3 directionNormalized = direction.normalized;
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.LookRotation(target.transform.position - transform.position, target.transform.position - transform.position), slowRotationSpeed);
            //transform.rotation.SetFromToRotation(temp1.eulerAngles,temp2.eulerAngles);
        }

        public void SetArrow(float _arrowDamage, float _arrowExistTime, float _arrowSpeed, Character _target, Vector3 _offset, Enemy _orignTarget, float _damagepPerTime, bool _destroySelfAfterDamage, bool _RotationWhileDamage, Skill _skill, bool _slowRotationWhileDamage, float _slowRotationSpeed, Transform _InstantiateTransform)
        {

            arrowDamage = _arrowDamage;
            arrowExistTime = _arrowExistTime;
            arrowSpeed = _arrowSpeed;
            target = _target;
            offset = _offset;
            orignTarget = _orignTarget;
            damagepPerTime = _damagepPerTime;
            destroySelfAfterDamage = _destroySelfAfterDamage;
            RotationWhileDamage = _RotationWhileDamage;
            skill = _skill;
            slowRotationWhileDamage = _slowRotationWhileDamage;
            slowRotationSpeed = _slowRotationSpeed;
            InstantiateTransform = _InstantiateTransform;
            SetVelocity();
            SetRotation();

        }


        private void OnTriggerEnter2D(Collider2D hit)
        {
            if (hit.GetComponent<Character_Stat>() != null && hit.GetComponent<Character>() != null && destroySelfAfterDamage)
            {
                hit.GetComponent<Character_Stat>().TakeDamage(arrowDamage, skill);        
                // capsuleCollider2D.enabled = false;      
                // rb.isKinematic = true;     
                // rb.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }

        private void OnTriggerStay2D(Collider2D hit)
        {
            if (hit.GetComponent<Character_Stat>() != null && hit.GetComponent<Character>() != null && !destroySelfAfterDamage)
            {
                if (damageTimeCounter <= 0)
                {
                    hit.GetComponent<Character_Stat>().TakeDamage(arrowDamage, skill);
                    damageTimeCounter = damagepPerTime;
                }
            }
        }
    }
}
