using System.Collections;
using System.Collections.Generic;
using SK;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class Bullet_Skill : Skill
{
   
    [SerializeField] private Transform bulletTransformIntialized;
    public bool bulletWithLightingUnLocked;
    [SerializeField] private int damageIncreasePerSkilled_1;
    [SerializeField] private GameObject basicPrefab;
    [SerializeField] private int amount;
    [SerializeField] private int angleRange;
    [SerializeField] private float transmitDamage;
    [SerializeField] private float transmitExistTime;
    [SerializeField] private float transmitSpeed;
    [SerializeField] private int lightingCost;
    
    [SerializeField] public UI_Skill_Slot bulletWithLighting;
    [Space(10)]
    public bool bulletWithMoreAmountLocked;
    [SerializeField] private int moreAmount;
    [SerializeField] private int damageIncreasePerSkilled_2;
    [SerializeField] public UI_Skill_Slot bulletWithMoreAmount;
    [Space(10)]
    public bool bulletWithLargerLocked;
    [SerializeField] private GameObject largePrefab;
    [SerializeField] private int damageIncreasePerSkilled_3;
    [SerializeField] public UI_Skill_Slot bulletWithLarger;
    [Space(10)]
    public bool bulletWithLessLightingLocked;

    [SerializeField] private int lessLightingCost;
    [SerializeField] private int damageIncreasePerSkilled_4;
    [SerializeField] public UI_Skill_Slot bulletWithLessLighting;
    [Space(10)]
    public bool bulletWithSplitableLocked;
    [SerializeField] private int damageIncreasePerSkilled_5;
    [SerializeField]private float newCoolDown_1;
    [SerializeField] public UI_Skill_Slot bulletWithSplitable;
    [SerializeField] private float splitTime;
    [Space(10)]
    public bool bulletWithMoreTimesLocked;
    [SerializeField] private int waves;
    [SerializeField]private float deltaTime;
    [SerializeField]private float newCoolDown_2;
    [SerializeField] private int damageIncreasePerSkilled_6;
    [SerializeField] public UI_Skill_Slot bulletWithMoreTimes;
    [Space(10)]
    public bool bulletSkillUsedUnlocked;
    [SerializeField] private UI_SkillUsed_Slot uI_SkillUsed_Slot;

    private Character character;
    private void Awake()
    {

    }
    protected override void Start()
    {
        base.Start();
        character = Character_Controller.instance.character;
        bulletWithLighting.GetComponent<Button>().onClick.AddListener(UnlockbulletWithLighting);
        bulletWithMoreAmount.GetComponent<Button>().onClick.AddListener(UnlockbulletWithMoreAmount);
        bulletWithLarger.GetComponent<Button>().onClick.AddListener(UnlockbulletWithLarger);
        bulletWithLessLighting.GetComponent<Button>().onClick.AddListener(UnlockbulletWithLessLighting);
        bulletWithSplitable.GetComponent<Button>().onClick.AddListener(UnlockbulletWithSplitable);
        bulletWithMoreTimes.GetComponent<Button>().onClick.AddListener(UnlockbulletWithMoreTimesLocked);
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void UseSkill()
    {
        base.UseSkill();
        if (bulletWithLightingUnLocked && uI_SkillUsed_Slot.Unlock) { }
        {

            bulletSkillUsedUnlocked = uI_SkillUsed_Slot.Unlock;
            if (bulletWithMoreAmountLocked)
            {
                if (bulletWithLargerLocked)
                {
                    if (bulletWithLessLighting)
                    {
                        if (bulletWithSplitableLocked)
                        {
                            this.cooldown = newCoolDown_1;
                            if (bulletWithMoreTimesLocked)
                            {
                                this.cooldown = newCoolDown_2;
                                if (Character_Controller.instance.UseSkillCostLighting(lessLightingCost))
                                {
                                    //不要让持续时间 大于冷却时间
                                    StopCoroutine("MiltiTimes");
                                    StartCoroutine("MiltiTimes");
                                }
                                return;
                            }
                            if (Character_Controller.instance.UseSkillCostLighting(lessLightingCost))
                            {
                                for (int i = 0; i < moreAmount; i++)
                                {
                                    GameObject _gameObject = Instantiate(largePrefab, bulletTransformIntialized.position, transform.rotation * Quaternion.AngleAxis(angleRange / moreAmount * i, Vector3.forward));
                                    Bullet_Skill_Controller bullet_Skill_Controller = _gameObject.GetComponent<Bullet_Skill_Controller>();
                                    bullet_Skill_Controller.SetArrow(transmitDamage, transmitExistTime, transmitSpeed, this, splitTime, bulletWithSplitableLocked,damageIncreasePerSkilled_5,true,0);
                                }
                            }
                            return;
                        }
                        if (Character_Controller.instance.UseSkillCostLighting(lessLightingCost))
                        {
                            for (int i = 0; i < moreAmount; i++)
                            {
                                GameObject _gameObject = Instantiate(largePrefab, bulletTransformIntialized.position, transform.rotation * Quaternion.AngleAxis(angleRange / moreAmount * i, Vector3.forward));
                                Bullet_Skill_Controller bullet_Skill_Controller = _gameObject.GetComponent<Bullet_Skill_Controller>();
                                bullet_Skill_Controller.SetArrow(transmitDamage, transmitExistTime, transmitSpeed, this, splitTime, bulletWithSplitableLocked,damageIncreasePerSkilled_4,true,0);
                            }
                        }
                        return;
                    }
                    if (Character_Controller.instance.UseSkillCostLighting(lightingCost))
                    {
                        for (int i = 0; i < moreAmount; i++)
                        {
                            GameObject _gameObject = Instantiate(largePrefab, bulletTransformIntialized.position, transform.rotation * Quaternion.AngleAxis(angleRange / moreAmount * i, Vector3.forward));
                            Bullet_Skill_Controller bullet_Skill_Controller = _gameObject.GetComponent<Bullet_Skill_Controller>();
                            bullet_Skill_Controller.SetArrow(transmitDamage, transmitExistTime, transmitSpeed, this, splitTime, bulletWithSplitableLocked,damageIncreasePerSkilled_3,true,0);
                        }
                    }
                    return;
                }
                if (Character_Controller.instance.UseSkillCostLighting(lightingCost))
                {
                    for (int i = 0; i < moreAmount; i++)
                    {
                        GameObject _gameObject = Instantiate(basicPrefab, bulletTransformIntialized.position, transform.rotation * Quaternion.AngleAxis(angleRange / moreAmount * i, Vector3.forward));
                        Bullet_Skill_Controller bullet_Skill_Controller = _gameObject.GetComponent<Bullet_Skill_Controller>();
                        bullet_Skill_Controller.SetArrow(transmitDamage, transmitExistTime, transmitSpeed, this, splitTime, bulletWithSplitableLocked,damageIncreasePerSkilled_2,true,0);
                    }
                    return;
                }
            }


            //花费数值
            if (Character_Controller.instance.UseSkillCostLighting(lightingCost))
            {
                for (int i = 0; i < amount; i++)
                {
                    GameObject _gameObject = Instantiate(basicPrefab, bulletTransformIntialized.position, transform.rotation * Quaternion.AngleAxis(angleRange / amount * i, Vector3.forward));
                    Bullet_Skill_Controller bullet_Skill_Controller = _gameObject.GetComponent<Bullet_Skill_Controller>();
                    bullet_Skill_Controller.SetArrow(transmitDamage, transmitExistTime, transmitSpeed, this, splitTime, bulletWithSplitableLocked,damageIncreasePerSkilled_1,true,0);
                }
            }
        }
    }

    private IEnumerator MiltiTimes()
    {
        int j = waves;
        while (j > 0)
        {
            for (int i = 0; i < moreAmount; i++)
            {
                GameObject _gameObject = Instantiate(largePrefab, bulletTransformIntialized.position, transform.rotation * Quaternion.AngleAxis(angleRange / moreAmount * i, Vector3.forward));
                Bullet_Skill_Controller bullet_Skill_Controller = _gameObject.GetComponent<Bullet_Skill_Controller>();
                bullet_Skill_Controller.SetArrow(transmitDamage, transmitExistTime, transmitSpeed, this, splitTime, bulletWithSplitableLocked,damageIncreasePerSkilled_6,true,0);
            }
            yield return new WaitForSeconds(deltaTime);
            j--;
        }
    }

    protected override void CheckUnlock()
    {
        UnlockbulletWithLighting();
        UnlockbulletWithMoreTimesLocked();
        UnlockbulletWithSplitable();
        UnlockbulletWithLessLighting();
        UnlockbulletWithLarger();
        UnlockbulletWithMoreAmount();
    }
    public void UnlockbulletWithMoreTimesLocked()
    {
        Debug.Log("尝试");
        if (bulletWithMoreTimes.unLock)
        {
            Debug.Log("成功");
            bulletWithMoreTimesLocked = true;
        }
    }
    public void UnlockbulletWithSplitable()
    {
        Debug.Log("尝试");
        if (bulletWithSplitable.unLock)
        {
            Debug.Log("成功");
            bulletWithSplitableLocked = true;
        }
    }

    public void UnlockbulletWithLessLighting()
    {
        Debug.Log("尝试");
        if (bulletWithLessLighting.unLock)
        {
            Debug.Log("成功");
            bulletWithLessLightingLocked = true;
        }
    }

    public void UnlockbulletWithLarger()
    {
        Debug.Log("尝试");
        if (bulletWithLarger.unLock)
        {
            Debug.Log("成功");
            bulletWithLargerLocked = true;
        }
    }

    public void UnlockbulletWithMoreAmount()
    {
        Debug.Log("尝试");
        if (bulletWithMoreAmount.unLock)
        {

            Debug.Log("成功");
            bulletWithMoreAmountLocked = true;
        }
    }


    public void UnlockbulletWithLighting()
    {
        Debug.Log("尝试");
        if (bulletWithLighting.unLock)
        {
            Debug.Log("成功");
            bulletWithLightingUnLocked = true;
        }
    }
}
