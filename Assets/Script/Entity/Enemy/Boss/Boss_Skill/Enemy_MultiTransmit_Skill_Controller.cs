using System;
using System.Collections;
using System.Collections.Generic;
using SK;
using UnityEngine;
using UnityEngine.AI;
public enum Target
{
    enemy, character
}

public class Enemy_MultiTransmit_Skill_Controller : MonoBehaviour
{
    private Target target;
    private float arrowDamage;

    private float arrowExistTime;

    private float timeCounter;

    private float arrowSpeed;


    private Rigidbody2D rb;
    private float damagepPerTime;
    private float damageTimeCounter;
    private bool destroySelfAfterDamage;
    private Skill skill;



    private float timePerSplit;
    private float timePerSplitCounter;
    private bool canStickIn;

    private Enemy_MultiTransmit_Skill enemy_MultiTransmit_Skill;
    private CapsuleCollider2D capsuleCollider2D;
    private bool ifHitted;
    private Enemy_Stat enemy_Stat;
    private int damageAdded;

    private Enemy enemy;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        if (GetComponent<Enemy_MultiTransmit_Skill>() != null)
        {
            enemy_MultiTransmit_Skill = GetComponent<Enemy_MultiTransmit_Skill>();
        }
    }
    void Start()
    {
        timePerSplitCounter = timePerSplit;
        timeCounter = arrowExistTime;
    }
    private void Update()
    {
        timeCounter -= Time.deltaTime;
        damageTimeCounter -= Time.deltaTime;
        timePerSplitCounter -= Time.deltaTime;
        if (timeCounter < 0)
        {
            Destroy(gameObject);
        }
        if (!ifHitted)
        {
            SetVelocity();
        }

        Split();
    }

    private void SetVelocity()
    {
        // Vector2 direction = Character_Controller.instance.character.transform.position - (orignTarget.transform.position + offset);
        // rb.velocity = direction.normalized * arrowSpeed;
        transform.position += transform.right * arrowSpeed * Time.deltaTime;
    }


    public void SetArrow(float _arrowDamage, float _arrowExistTime, float _arrowSpeed, float _damagepPerTime, bool _destroySelfAfterDamage, Skill _skill, Target _target, float _timePerSplit, bool _canStickIn, Enemy _enemy)
    {
        target = _target;
        arrowDamage = _arrowDamage;
        arrowExistTime = _arrowExistTime;
        arrowSpeed = _arrowSpeed;

        damagepPerTime = _damagepPerTime;
        destroySelfAfterDamage = _destroySelfAfterDamage;

        skill = _skill;
        timePerSplit = _timePerSplit;
        canStickIn = _canStickIn;

        enemy = _enemy;

        // SetRotation();

    }


    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (target == Target.character)
        {
            if (hit.GetComponent<Character_Stat>() != null && hit.GetComponent<Character>() != null)
            {

                if (enemy != null)
                {
                    hit.GetComponent<Character_Stat>().DoMagicDamage(enemy.GetComponent<Enemy_Stat>(), skill);
                }
                else
                {
                    hit.GetComponent<Character_Stat>().TakeDamage(arrowDamage, skill);
                }
                if (canStickIn && !destroySelfAfterDamage)
                {
                    ifHitted = true;
                    capsuleCollider2D.enabled = false;
                    rb.isKinematic = true;
                    rb.velocity = Vector2.zero;
                    rb.constraints = RigidbodyConstraints2D.FreezeAll;
                    gameObject.transform.SetParent(hit.transform);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            if (hit.GetComponent<Enemy_Stat>() != null && hit.GetComponent<Enemy>() != null)
            {
                hit.GetComponent<Enemy_Stat>().DoDamage(Character_Controller.instance.character.GetComponent<Character_Stat>());
                if (canStickIn && !destroySelfAfterDamage)
                {
                    ifHitted = true;
                    capsuleCollider2D.enabled = false;
                    rb.isKinematic = true;
                    rb.velocity = Vector2.zero;
                    rb.constraints = RigidbodyConstraints2D.FreezeAll;
                    gameObject.transform.SetParent(hit.transform);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D hit)
    {
        if (target == Target.character)
        {

            if (hit.GetComponent<Character_Stat>() != null && hit.GetComponent<Character>() != null && !destroySelfAfterDamage)
            {
                if (damageTimeCounter <= 0)
                {
                    if (enemy != null)
                    {
                        hit.GetComponent<Character_Stat>().DoMagicDamage(enemy.GetComponent<Enemy_Stat>(), skill);
                    }
                    else
                    {
                        hit.GetComponent<Character_Stat>().TakeDamage(arrowDamage, skill);
                    }

                    damageTimeCounter = damagepPerTime;
                }
            }
        }
        else
        {
            if (hit.GetComponent<Enemy_Stat>() != null && hit.GetComponent<Enemy>() != null && !destroySelfAfterDamage)
            {
                if (damageTimeCounter <= 0)
                {
                    hit.GetComponent<Enemy_Stat>().DoDamage(Character_Controller.instance.character.GetComponent<Character_Stat>());
                    damageTimeCounter = damagepPerTime;
                }
            }
        }
    }

    private void Split()
    {
        if (enemy_MultiTransmit_Skill != null)
        {

            if (timePerSplitCounter < 0)
            {
                enemy_MultiTransmit_Skill.CanUseSkill();
                timePerSplitCounter = timePerSplit;
            }
        }
    }
}
