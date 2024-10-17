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
        private Skill  skill;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
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
                SetRotation(target, orignTarget);
        }

        private void SetVelocity()
        {
            Vector2 direction = Character_Controller.instance.character.transform.position - (orignTarget.transform.position + offset);
            rb.velocity = direction.normalized * arrowSpeed;
        }

        private void SetRotation(Character _target, Enemy _orignTarget)
        {
            transform.LookAt(_target.transform.position);
            transform.Rotate(0, 90, 0);
            // Quaternion newQuaternion = Quaternion.LookRotation(_target.transform.position - _orignTarget.transform.position);
            // transform.rotation = newQuaternion;
        }

        public void SetArrow(float _arrowDamage, float _arrowExistTime, float _arrowSpeed, Character _target, Vector3 _offset, Enemy _orignTarget, float _damagepPerTime, bool _destroySelfAfterDamage, bool _RotationWhileDamage,Skill _skill)
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
            SetVelocity();

            SetRotation(_target, _orignTarget);


        }


        private void OnTriggerEnter2D(Collider2D hit)
        {
            if (hit.GetComponent<Character_Stat>() != null && hit.GetComponent<Character>() != null && destroySelfAfterDamage)
            {
                hit.GetComponent<Character_Stat>().TakeDamage(arrowDamage,skill);
                Destroy(gameObject);
            }
        }

        private void OnTriggerStay2D(Collider2D hit)
        {
            if (hit.GetComponent<Character_Stat>() != null && hit.GetComponent<Character>() != null && !destroySelfAfterDamage)
            {
                if (damageTimeCounter <= 0)
                {
                    hit.GetComponent<Character_Stat>().TakeDamage(arrowDamage,skill);
                    damageTimeCounter = damagepPerTime;
                }
            }
        }
    }
}
