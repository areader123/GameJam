using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SK;
using UnityEditor;
public class Enemy_Create_Skill_Controller : MonoBehaviour
{
    private float existTime;
    private float existCounter;
    private float damageValue;
    private bool destroyAfterDamage;
    private Rigidbody2D rb;
    private Skill skill;
    private float damageTimeCounter;
    private float timePerDamage;
    Enemy_Robot enemy;
    private void Start()
    {
        existCounter = existTime;
    }
    private void Update()
    {
        existCounter -= Time.deltaTime;
        if(existCounter < 0)
        {
            Destroy(gameObject);
        }
    }
    public void SetUp(float _existTime,Skill _skill,float _timePerDamage,bool _destroyAfterDamage,float _damageValue,Enemy_Robot _enemy)
    {
        existTime = _existTime;
        skill =_skill;
        timePerDamage = _timePerDamage;
        destroyAfterDamage = _destroyAfterDamage;
        damageValue =_damageValue;
        enemy = _enemy;
    }
     private void OnTriggerEnter2D(Collider2D hit)
        {
            if (hit.GetComponent<Character_Stat>() != null && hit.GetComponent<Character>() != null && destroyAfterDamage)
            {
                hit.GetComponent<Character_Stat>().DoMagicDamage(enemy.GetComponent<Enemy_Stat>(),skill);
                //hit.GetComponent<Character_Stat>().TakeDamage(damageValue, skill);
                Destroy(gameObject);
            }
        }

        private void OnTriggerStay2D(Collider2D hit)
        {
            if (hit.GetComponent<Character_Stat>() != null && hit.GetComponent<Character>() != null && !destroyAfterDamage)
            {
                if (damageTimeCounter <= 0)
                {
                    hit.GetComponent<Character_Stat>().DoMagicDamage(enemy.GetComponent<Enemy_Stat>(),skill);
                    damageTimeCounter = timePerDamage;
                }
            }
        }
}
