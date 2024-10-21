using System.Collections;
using System.Collections.Generic;
using SK;
using UnityEngine;

public class Bullet_Skill_Controller : MonoBehaviour
{
    private float arrowDamage;

    private float arrowExistTime;

    private float timeCounter;

    private float arrowSpeed;

    private bool destroyAfterDamage;
    private float damagepPerTime;
    private Skill skill;
    private float timePerSplit;
    private float timePerSplitCounter;
    private float damageTimeCounter;

    private bool canSplit;

    private Enemy_MultiTransmit_Skill enemy_MultiTransmit_Skill;



    private void Awake()
    {
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
        timePerSplitCounter -= Time.deltaTime;
        damageTimeCounter -= Time.deltaTime;
        if (timeCounter < 0)
        {
            Destroy(gameObject);
        }
        SetVelocity();
        Split();
    }

    private void SetVelocity()
    {

        transform.position += transform.right * arrowSpeed * Time.deltaTime;
    }


    public void SetArrow(float _arrowDamage, float _arrowExistTime, float _arrowSpeed, Skill _skill, float _timePerSplit, bool _canSplit, int damageUpgraded,bool _destroyAfterDamage,int _damagepPerTime)
    {

        arrowDamage = _arrowDamage + damageUpgraded;
        arrowExistTime = _arrowExistTime;
        arrowSpeed = _arrowSpeed;
        skill = _skill;
        timePerSplit = _timePerSplit;
        canSplit = _canSplit;
        damagepPerTime = _damagepPerTime;
        destroyAfterDamage = _destroyAfterDamage;
        // SetRotation();

    }


    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.GetComponent<Enemy_Stat>() != null && hit.GetComponent<Enemy>() != null && destroyAfterDamage)
        {
            hit.GetComponent<Enemy_Stat>().TakeDamage(arrowDamage, skill);
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D hit)
    {




        if (hit.GetComponent<Enemy_Stat>() != null && hit.GetComponent<Enemy>() != null && !destroyAfterDamage)
        {
            if (damageTimeCounter <= 0)
            {
                hit.GetComponent<Enemy_Stat>().TakeDamage(arrowDamage, skill);
                damageTimeCounter = damagepPerTime;
            }
        }

    }


    private void Split()
    {
        if (enemy_MultiTransmit_Skill != null && canSplit)
        {
            if (timePerSplitCounter < 0)
            {
                enemy_MultiTransmit_Skill.CanUseSkill();
                timePerSplitCounter = timePerSplit;
            }
        }
    }
}
