using System.Collections;
using System.Collections.Generic;
using SK;
using UnityEngine;

public class Robot_Skill_One_Skill_Controller : MonoBehaviour
{

        private float arrowExistTime;

        private float timeCounter;

       

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
            SetRotation(target,orignTarget);
        }

        private void SetRotation(Character _target,Enemy _orignTarget)
        {
            transform.LookAt(_target.transform.position);
            transform.Rotate(0,90,0);
            // Quaternion newQuaternion = Quaternion.LookRotation(_target.transform.position - _orignTarget.transform.position);
            // transform.rotation = newQuaternion;
        }

        public void SetUpEffect(float _arrowExistTime, Character _target, Enemy _orignTarget)
        {
            arrowExistTime = _arrowExistTime;
            target = _target;
            orignTarget = _orignTarget;
        }
}
