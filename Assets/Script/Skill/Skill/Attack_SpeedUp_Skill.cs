using System.Collections;
using System.Collections.Generic;
using SK;
using UnityEngine;
using UnityEngine.UI;

public class Attack_SpeedUp_Skill : Skill
{
    private bool skilling;
    private bool skilling_Three;
    public bool attackSpeedUpWithHPAndLightingUnlocked;
    [SerializeField] private float perLevelAmount_1;
    private bool basic_1;
    [SerializeField] private float speedUpAmount;
    [SerializeField] private float skillDuration;
    private float skillTimeCounter;
    [SerializeField] private int healthCost;
    [SerializeField] private int lightingCost;

    [SerializeField] private UI_Skill_Slot attackSpeedUpWithHPAndLighting;
    [Space(10)]
    public bool lowerLightingWithMoreAttackSpeedUnlocked;
    [SerializeField] private float perLevelAmount_2;
    private bool basic_2;
    [SerializeField] private int newLightCost;
    [SerializeField] private float newSpeedUpAmountByLight;

    [SerializeField] private UI_Skill_Slot lowerLightingWithMoreAttackSpeed;
   
     
  
   

  
    [Space(10)]
    public bool attckCanDestroyBulletUnlock;
    public bool skilling_Four;

    [SerializeField] private float perLevelAmount_4;
    private bool basic_4;
    [SerializeField] private UI_Skill_Slot attckCanDestroyBullet;
    [Space(10)]
    [SerializeField] private UI_SkillUsed_Slot uI_SkillUsed_Slot;
    public bool attackSpeedUpSkillUsedLocked;


    private Character_Stat character_Stat;
    private Animator animator;
    private float totalDecrease = 0;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        
    }
    protected override void Update()
    {
        base.Update();
        if (!attackSpeedUpSkillUsedLocked)
        {
            attackSpeedUpSkillUsedLocked = uI_SkillUsed_Slot.Unlock;
        }
        
    }

    protected override void Start()
    {
        base.Start();
        attackSpeedUpWithHPAndLighting.GetComponent<Button>().onClick.AddListener(UnlockattackSpeedUpWithHPAndLighting);
        lowerLightingWithMoreAttackSpeed.GetComponent<Button>().onClick.AddListener(UnlocklowerLightingWithMoreAttackSpeed);
        attckCanDestroyBullet.GetComponent<Button>().onClick.AddListener(UnlockattckCanDestroyBullet);
        character_Stat = Character_Controller.instance.character.GetComponent<Character_Stat>();
        animator = Character_Controller.instance.character.animator;
    }
    public override void UseSkill()
    {
        base.UseSkill();
        if (attackSpeedUpWithHPAndLightingUnlocked && uI_SkillUsed_Slot.Unlock)
        {
            //加速一
            if (lowerLightingWithMoreAttackSpeedUnlocked)
            {
                //加速二
              
             

                    if (attckCanDestroyBulletUnlock)
                    {
                        //击碎弹幕
                        StartCoroutine("Skilling_Four");
                    }
               
                if (Character_Controller.instance.UseSkillCostLighting(newLightCost) && character_Stat._currentHP >= healthCost)
                {
                    character_Stat.TakeDamage(healthCost, this);

                    StartCoroutine("Skill_Two");
                    return;
                }
            }
            if (Character_Controller.instance.UseSkillCostLighting(lightingCost) && character_Stat._currentHP >= healthCost)
            {
                character_Stat.TakeDamage(healthCost, this);
                StartCoroutine("Skill_One");
            }
        }


    }





    protected override void CheckUnlock()
    {
        base.CheckUnlock();
        UnlockattackSpeedUpWithHPAndLighting();
        UnlocklowerLightingWithMoreAttackSpeed();
        UnlockattckCanDestroyBullet();
    }

    private void UnlockattackSpeedUpWithHPAndLighting()
    {
       // Debug.Log("尝试");
        if (attackSpeedUpWithHPAndLighting.unLock)
        {
           // Debug.Log("成功");
            attackSpeedUpWithHPAndLightingUnlocked = true;
            if (!basic_1)
            {
                float basic = Character_Controller.instance.character.animator.GetFloat("Speed");
                basic += perLevelAmount_1;
                Character_Controller.instance.character.animator.SetFloat("Speed", basic);
                cost = lightingCost;
                basic_1 = true;
            }
        }
    }

    private IEnumerator Skill_One()
    {
        float basic = animator.GetFloat("Speed");
        float origon = basic;
        basic += speedUpAmount;
        animator.SetFloat("Speed", basic);
        skilling = true;
        yield return new WaitForSeconds(skillDuration);
        skilling = false;
        animator.SetFloat("Speed", origon);
    }
    private IEnumerator Skill_Two()
    {
        float basic = animator.GetFloat("Speed");
        float origon = basic;
        basic += newSpeedUpAmountByLight * (1 - Character_Controller.instance.GetLightingNumber() / Character_Controller.instance.GetMaxLightingNumber());
        basic += speedUpAmount;
        animator.SetFloat("Speed", basic);
        skilling = true;
        yield return new WaitForSeconds(skillDuration);
        skilling = false;
        animator.SetFloat("Speed", origon);
    }

    private IEnumerator Skill_Three()
    {
        float basic = animator.GetFloat("Speed");
        float origon = basic;
        basic += newSpeedUpAmountByLight;
        animator.SetFloat("Speed", basic);
        skilling = true;
        skilling_Three = true;
        //Debug.Log("Skill_Three");
        yield return new WaitForSeconds(skillDuration);
        skilling = false;
        skilling_Three = false;
        animator.SetFloat("Speed", origon);
    }

    private IEnumerator Skilling_Four()
    {
        skilling_Four = true;
        yield return new WaitForSeconds(skillDuration);
        skilling_Four = false;
    }




    private void UnlocklowerLightingWithMoreAttackSpeed()
    {
        //.Log("尝试");
        if (lowerLightingWithMoreAttackSpeed.unLock)
        {
          //  Debug.Log("成功");
            lowerLightingWithMoreAttackSpeedUnlocked = true;
            if (!basic_2)
            {
                basic_2 = true;
                float basic = Character_Controller.instance.character.animator.GetFloat("Speed");
                basic += perLevelAmount_2;
                cost = newLightCost;
                Character_Controller.instance.character.animator.SetFloat("Speed", basic);
            }
        }
    }
    private void UnlockattckCanDestroyBullet()
    {
        //Debug.Log("尝试");
        if (attckCanDestroyBullet.unLock)
        {
            //Debug.Log("成功");
            attckCanDestroyBulletUnlock = true;
            if (!basic_4)
            {
                basic_4 = true;
                float basic = Character_Controller.instance.character.animator.GetFloat("Speed");
                basic += perLevelAmount_4;
                Character_Controller.instance.character.animator.SetFloat("Speed", basic);
            }
        }
    }


    // private void OnDrawGizmos()
    // {
    //     Gizmos.DrawWireSphere(character.transform.position, findFarthestEnemyRadius);
    // }
}
