using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace SK
{

    public class Light_Controller : MonoBehaviour
    {
        public static Light_Controller instance;
        private CircleCollider2D cd;
        [Header("Light Info")]
        public Light2D light2d;
        [SerializeField] private float minScale;
        [SerializeField] private float radius;
        public float maxScale;

        [Header("LightNumber Info")]

        [SerializeField] private int DecreaseLightingNumber;
        [SerializeField] private float timeDuration;
        [Header("lightGrow Info")]
        [SerializeField] private float scaleSpeed;
        private Character character;

        private int _maxLightingNumber;
        private int _lightingNumber;
        private float finalScale;
        private float timeCounter;
        private float scaleTimeCounter;



        private List<Enemy> enemies;
       

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(instance);
            }
            else
            {
                instance = this;
            }

        }

        private void Start()
        {
            cd = GetComponent<CircleCollider2D>();
            enemies = new List<Enemy>();
        }
        private void Update()
        {
            if (Character_Controller.instance.canChangeLightScale())
            {
                CaculateScale();
            }
            UpdateScale();
            DecreaseWhenDetectEnemy();

            SkillManager.instance.light_Skill.CanUseSkill();

            DoSkillDamage();

        }

        private void UpdateScale()
        {
            if (light2d.pointLightOuterRadius > finalScale && cd.radius > finalScale)
            {
                float delta = finalScale * scaleSpeed * Time.deltaTime;
                light2d.pointLightOuterRadius -= delta;
                cd.radius -= delta;
            }
            if (light2d.pointLightOuterRadius < finalScale&& cd.radius < finalScale)
            {
                float delta = finalScale * scaleSpeed * Time.deltaTime;
                light2d.pointLightOuterRadius += delta;
                cd.radius += delta;
            }
        }

        private void DecreaseWhenDetectEnemy()
        {
            if (DetectEnemy())
            {
                timeCounter -= Time.deltaTime;
                if (timeCounter <= 0)
                {
                    Character_Controller.instance.DecreaseLightingNumber(DecreaseLightingNumber);
                    timeCounter = timeDuration;
                }
            }
        }

        private void CaculateScale()
        {
            _lightingNumber = Character_Controller.instance.GetLightingNumber();
            _maxLightingNumber = Character_Controller.instance.GetMaxLightingNumber();
            finalScale = Mathf.Lerp(minScale, maxScale, (float)_lightingNumber / _maxLightingNumber);
            //  transform.localScale = Vector2.Lerp(new Vector2(minScale, minScale), new Vector2(finalScale, finalScale), scaleSpeed * Time.deltaTime);
        }

        void OnTriggerEnter2D(Collider2D hit)
        {


            if (hit.GetComponent<Enemy>() != null)
            {
                enemies.Add(hit.GetComponent<Enemy>());
                if (SkillManager.instance.light_Skill.lightWithMonsterSpeedDownUnlocked)
                {
                    SkillManager.instance.light_Skill.EnemySpeedDown(hit.GetComponent<Enemy>());
                    if (SkillManager.instance.light_Skill.lightWithMonsterAniSlowUnLocked)
                    {
                        SkillManager.instance.light_Skill.EnemyAniDown(hit.GetComponent<Enemy>());
                    }

                }
            }

        }

        private void DoSkillDamage()
        {
            foreach (Enemy obj in enemies)
            {
                if (SkillManager.instance.light_Skill.lightWithMonsterDamagedUnLocked)
                {
                    SkillManager.instance.light_Skill.EnemyDamaged(obj);
                }
            }
        }

      

        void OnTriggerExit2D(Collider2D hit)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                if (enemies.Remove(hit.GetComponent<Enemy>()))
                {
                    if (SkillManager.instance.light_Skill.lightWithMonsterSpeedDownUnlocked)
                    {
                        SkillManager.instance.light_Skill.EnemySpeedUp(hit.GetComponent<Enemy>());
                        if (SkillManager.instance.light_Skill.lightWithMonsterAniSlowUnLocked)
                        {
                            SkillManager.instance.light_Skill.EnemyAniUp(hit.GetComponent<Enemy>());
                        }

                    }
                }
            }
        }





        private bool DetectEnemy()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, finalScale);
            if (colliders == null)
            {
                return false;
            }
            foreach (var collider in colliders)
            {
                if (collider.GetComponent<Enemy>() != null)
                {
                    return true;
                }
            }
            return false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, finalScale * radius);
        }

      



    }

}
