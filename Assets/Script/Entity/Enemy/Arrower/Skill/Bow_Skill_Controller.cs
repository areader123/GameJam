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
            if (timeCounter < 0)
            {
                Destroy(gameObject);
            }
        }

        private void SetVelocity()
        {
            Vector2 direction = Character_Controller.instance.character.transform.position - (orignTarget.transform.position + offset);
            rb.velocity = direction.normalized * arrowSpeed;
        }

        public void SetArrow(float _arrowDamage, float _arrowExistTime, float _arrowSpeed, Character _target,Vector3 _offset,Enemy _orignTarget)
        {

            arrowDamage = _arrowDamage;
            arrowExistTime = _arrowExistTime;
            arrowSpeed = _arrowSpeed;
            target = _target;
            offset = _offset;
            orignTarget =_orignTarget;
            SetVelocity();
        }


        private void OnTriggerEnter2D(Collider2D hit)
        {
            if (hit.GetComponent<Character>() != null)
            {
                hit.GetComponent<Character_Stat>().TakeDamage(arrowDamage);
                Destroy(gameObject);
            }
        }
    }
}
