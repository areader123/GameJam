using System;
using System.Collections;
using System.Collections.Generic;
using SK;
using UnityEngine;
using UnityEngine.UI;

public class Light_Skill : Skill
{
    private Light_Controller light_Controller;
    public bool lightCanBeIncreaseByTimeUnlock;
    [SerializeField] private float newRangePercent_1;
    private bool new_1;
    [SerializeField] private int increaseLightAmount;
    [SerializeField] private float timePerIncreaseLight;
    private float timeCounterLight;
    [SerializeField] public UI_Skill_Slot lightCanBeIncreaseByTime;
    [Space(10)]
    public bool lightWithHealthRecoverUnLocked;
    [SerializeField] private float newRangePercent_2;
    private bool new_2;

    [SerializeField] private int increaseHealthAmount;
    [SerializeField] private float timePerIncreaseHealth;
    private float timeCounterHealth;
    [SerializeField] public UI_Skill_Slot lightWithHealthRecover;
    [Space(10)]
    public bool lightWithMonsterSpeedDownUnlocked;
    [SerializeField] private float newRangePercent_3;
    private bool new_3;

    [SerializeField][Range(0,1)] private float decreaseSpeedPercent;
    [SerializeField] public UI_Skill_Slot lightWithMonsterSpeedDown;
    [Space(10)]
    public bool lightWithMonsterDamagedUnLocked;
    [SerializeField] private float newRangePercent_4;
    private bool new_4;

    [SerializeField] private int damageAmount;
    [SerializeField] private float timePerDamage;
    private float timeCounterDamage;
    [SerializeField] public UI_Skill_Slot lightWithMonsterDamaged;
    [Space(10)]
    public bool lightWithMonsterAniSlowUnLocked;
    [SerializeField] private float newRangePercent_5;
    private bool new_5;

    [SerializeField][Range(0, 1)] private float decreaseAniPercent;
    [SerializeField] public UI_Skill_Slot lightWithMonsterAniSlow;
    [Space(10)]

    public bool lightwhenLowHealthUnhittableUnlock;
    [SerializeField] private float newRangePercent_6;
    private bool new_6;

    [SerializeField][Range(0,1)] private float lowHealthPercentsBoardary;
    [SerializeField] private float timePerUnhittable;
    private float timeCounterUnittable;
    [SerializeField]private float timeDuration;
    [SerializeField]private float alphaPercent;
    [SerializeField] private UI_Skill_Slot lightwhenLowHealthUnhittable;

    private Character character;
    private Character_Controller character_Controller;


    protected override void Start()
    {
        base.Start();
        lightCanBeIncreaseByTime.GetComponent<Button>().onClick.AddListener(UnlocklightCanBeIncreaseByTime);
        lightWithHealthRecover.GetComponent<Button>().onClick.AddListener( UnlocklightWithHealthRecover);
        lightWithMonsterSpeedDown.GetComponent<Button>().onClick.AddListener(UnlocklightWithMonsterSpeedDown);
        lightWithMonsterDamaged.GetComponent<Button>().onClick.AddListener(UnlocklightWithMonsterDamaged);
        lightWithMonsterAniSlow.GetComponent<Button>().onClick.AddListener(UnlocklightWithMonsterAniSlowUnLocked);
        lightwhenLowHealthUnhittable.GetComponent<Button>().onClick.AddListener(UnlocklightwhenLowHealthUnhittable);
         light_Controller = Light_Controller.instance;
        character = Character_Controller.instance.character;
        character_Controller = Character_Controller.instance;
        Debug.Log("start");
    }

    protected override void Update()
    {
        base.Update();
        timeCounterDamage -= Time.deltaTime;
        timeCounterHealth -= Time.deltaTime;
        timeCounterLight -= Time.deltaTime;
        timeCounterUnittable -= Time.deltaTime;
       
    }

    public override void UseSkill()
    {
        base.UseSkill();
        if (lightCanBeIncreaseByTimeUnlock)
        {
            //玩家光亮值自行增加
          
            if (character_Controller.GetLightingNumber() + increaseLightAmount <= character_Controller.GetMaxLightingNumber() && timeCounterLight < 0)
            {
                for (int i = 0; i < increaseLightAmount; i++)
                {
                    character_Controller.AddLightingNumber();
                }
                Debug.Log("玩家光亮值自行增加");
                timeCounterLight = timePerIncreaseLight;
            }
            if (lightWithHealthRecoverUnLocked)
            {
                
                if (timeCounterHealth < 0)
                {
                    Debug.Log("玩家回血");
                    //玩家回血 //increaseHealth
                    character.GetComponent<Character_Stat>().IncreaseHealthOnly(increaseHealthAmount);
                    timeCounterHealth = timePerIncreaseHealth;
                }
                if (lightWithMonsterSpeedDownUnlocked)
                {
                    
                    //怪物速度降低
                    //在lighting_Controller中触发
                    if (lightWithMonsterDamagedUnLocked)
                    {
                      
                        //怪物受到伤害
                         //在lighting_Controller中触发
                        if (lightWithMonsterAniSlowUnLocked)
                        {
                           
                            //怪物动画速度降低
                             //在lighting_Controller中触发
                            if (lightwhenLowHealthUnhittableUnlock)
                            {
                               
                                //低血量时免疫
                                if(character.GetComponent<Character_Stat>()._currentHP <= lowHealthPercentsBoardary * character.GetComponent<Character_Stat>().GetMaxHealth() 
                                && timeCounterUnittable < 0)
                                {
                                    StopCoroutine("UnHittable");
                                    StartCoroutine("UnHittable");
                                    timeCounterUnittable = timePerUnhittable;
                                }
                            }
                        }
                    }

                }
            }
        }
    }

    private IEnumerator UnHittable()
    {
        character.capsuleCollider2D.enabled = false;
        character.GetComponent<SpriteRenderer>().color = new Vector4(1,1,1,character.GetComponent<SpriteRenderer>().color.a * alphaPercent);
        yield return new WaitForSeconds(timeDuration);
        character.capsuleCollider2D.enabled = true;
        character.GetComponent<SpriteRenderer>().color = new Vector4(1,1,1,character.GetComponent<SpriteRenderer>().color.a / alphaPercent);
    }
   

    public void EnemySpeedDown(Enemy enemy)
    {
        enemy.movementSpeed *= decreaseSpeedPercent;
        enemy.battleSpeed *= decreaseSpeedPercent;
    }

    public void EnemySpeedUp(Enemy enemy)
    {
        enemy.movementSpeed /= decreaseSpeedPercent;
        enemy.battleSpeed /= decreaseSpeedPercent;
    }

    public void EnemyDamaged(Enemy enemy)
    {
        if (timeCounterDamage < 0)
        {
            enemy.GetComponent<Enemy_Stat>().TakeDamage(damageAmount, this);
            timeCounterDamage = timePerDamage;
        }
    }

    public void EnemyAniDown(Enemy enemy)
    {
        enemy.animator.speed *=  decreaseAniPercent;
    }

    public void EnemyAniUp(Enemy enemy)
    {
        enemy.animator.speed /=  decreaseAniPercent;
    }



    protected override void CheckUnlock()
    {
        UnlocklightCanBeIncreaseByTime();
        UnlocklightWithMonsterAniSlowUnLocked();
        UnlocklightWithMonsterDamaged();
        UnlocklightWithMonsterSpeedDown();
        UnlocklightWithHealthRecover();
        UnlocklightwhenLowHealthUnhittable();
    }

    private void UnlocklightwhenLowHealthUnhittable()
    {
        Debug.Log("尝试");
        if (lightwhenLowHealthUnhittable.unLock)
        {
            Debug.Log("成功");
            lightwhenLowHealthUnhittableUnlock = true;
            if(!new_6)
            {
                new_1 = true;
                 light_Controller.maxScale *=  newRangePercent_6;
            }
        }
    }

    public void UnlocklightWithMonsterAniSlowUnLocked()
    {
        Debug.Log("尝试");
        if (lightWithMonsterAniSlow.unLock)
        {
            Debug.Log("成功");
            lightWithMonsterAniSlowUnLocked = true;
            if(!new_5)
            {
                new_5 = true;
                light_Controller.maxScale *=  newRangePercent_5;
            }
        }
    }


    public void UnlocklightWithMonsterDamaged()
    {
        Debug.Log("尝试");
        if (lightWithMonsterDamaged.unLock)
        {
            Debug.Log("成功");
            lightWithMonsterDamagedUnLocked = true;
            if(!new_4)
            {
                new_4 = true;
                light_Controller.maxScale *=  newRangePercent_4;
            }
        }
    }

    public void UnlocklightWithMonsterSpeedDown()
    {
        Debug.Log("尝试");
        if (lightWithMonsterSpeedDown.unLock)
        {
            Debug.Log("成功");
            lightWithMonsterSpeedDownUnlocked = true;
            if(!new_3)
            {
                new_3 = true;
                light_Controller.maxScale *=  newRangePercent_3;
            }
        }
    }

    public void UnlocklightWithHealthRecover()
    {
        Debug.Log("尝试");
        if (lightWithHealthRecover.unLock)
        {

            Debug.Log("成功");
            lightWithHealthRecoverUnLocked = true;
            if(!new_2)
            {
                new_2 = true;
                light_Controller.maxScale *=  newRangePercent_2;
            }
        }
    }


    public void UnlocklightCanBeIncreaseByTime()
    {
        Debug.Log("尝试");
        if (lightCanBeIncreaseByTime.unLock)
        {
            Debug.Log("成功");
            lightCanBeIncreaseByTimeUnlock = true;
            if(!new_1)
            {
                new_1 = true;
                light_Controller.maxScale *=  newRangePercent_1;
            }
        }
    }

}
