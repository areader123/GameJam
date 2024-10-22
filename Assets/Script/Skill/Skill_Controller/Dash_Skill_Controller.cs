using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SK;
public class Dash_Skill_Controller : MonoBehaviour
{
    private float arrowDamage;

    private float arrowExistTime;

    private float timeCounter;

    private float arrowSpeed;
    private float damagepPerTime;
    private float damageTimeCounter;
    private bool destroySelfAfterDamage;
    private Skill skill;
        
    
    public void SetArrow(float _arrowDamage, float _arrowExistTime, float _arrowSpeed, float _damagepPerTime, bool _destroySelfAfterDamage,Skill _skill)
    {
        arrowDamage = _arrowDamage;
        arrowExistTime = _arrowExistTime;
        arrowSpeed = _arrowSpeed;
        timeCounter = arrowExistTime;
        damagepPerTime = _damagepPerTime;
        destroySelfAfterDamage = _destroySelfAfterDamage;
        skill = _skill;
       
    }
    private void Update()
    {
        timeCounter -= Time.deltaTime;
        damageTimeCounter -= Time.deltaTime;
        if (timeCounter < 0)
        {
            Destroy(gameObject);
        }
        SetVelocity();
    }

    private void SetVelocity()
    {
        transform.position += transform.right * arrowSpeed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.GetComponent<Enemy_Stat>() != null && hit.GetComponent<Enemy>() != null && destroySelfAfterDamage)
        {
            hit.GetComponent<Enemy_Stat>().TakeDamage(arrowDamage, skill);
            Destroy(gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D hit)
    {
        if (hit.GetComponent<Enemy_Stat>() != null && hit.GetComponent<Enemy>() != null && !destroySelfAfterDamage)
            {
                if (damageTimeCounter <= 0)
                {
                    hit.GetComponent<Enemy_Stat>().TakeDamage(arrowDamage, skill);
                    damageTimeCounter = damagepPerTime;
                }
            }
    }

}
