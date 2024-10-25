using System.Collections;
using System.Collections.Generic;
using SK;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Bullet_Fan_Skill : Skill
{
    [SerializeField] private Transform bulletTransformIntialized;
    public bool bulletWithLightingUnLocked;
    [SerializeField] private int damageIncreasePerSkilled_1;
    [SerializeField] private GameObject basicPrefab;
    [SerializeField] private int fanAngle;
    [SerializeField] private int amount;
  
    [SerializeField] private float transmitDamage;
    [SerializeField] private float transmitExistTime;
    [SerializeField] private float transmitSpeed;
    [SerializeField] private int damagepPerTime;
    [SerializeField] private int lightingCost;

    [SerializeField] public UI_Skill_Slot bulletWithLighting;
    [Space(10)]
    public bool bulletCanHitBackLocked;
    [SerializeField] private int damageIncreasePerSkilled_2;
    [SerializeField] public UI_Skill_Slot bulletCanHitBack;
    [Space(10)]
    public bool bulletCanPenetratelocked;
    [SerializeField] private bool noDestroyAfterDamage;
    [SerializeField] private int damageIncreasePerSkilled_3;
    [SerializeField] public UI_Skill_Slot bulletCanPenetrate;
    [Space(10)]
    public bool bulletWithLowerCoolDownLocked;
    [SerializeField] private float newCoolDown;
    [SerializeField] private int damageIncreasePerSkilled_4;
    [SerializeField] public UI_Skill_Slot bulletWithLowerCoolDown;
    [Space(10)]
    public bool bulletWithMoreTimesLocked;
    [SerializeField] private int waves;
    [SerializeField] private float deltaTime;
    [SerializeField] private int damageIncreasePerSkilled_5;
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
        bulletCanHitBack.GetComponent<Button>().onClick.AddListener(UnlockbulletCanHitBack);
        bulletCanPenetrate.GetComponent<Button>().onClick.AddListener(UnlockbulletCanPenetrate);
        bulletWithLowerCoolDown.GetComponent<Button>().onClick.AddListener(UnlockbulletWithLowerCoolDown);
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
            if (bulletCanHitBackLocked)
            {
                this.skillHitBack = SkillHitBack.can;
                if (bulletCanPenetratelocked)
                {
                    noDestroyAfterDamage = true;
                    if (bulletWithLowerCoolDown)
                    {
                        cooldown = newCoolDown;
                        if (bulletWithMoreTimesLocked)
                        {
                            if (Character_Controller.instance.UseSkillCostLighting(lightingCost))
                            {
                                //不要让持续时间 大于冷却时间
                                StopCoroutine("MiltiTimes");
                                StartCoroutine("MiltiTimes");
                            }
                            return;
                        }
                        if (Character_Controller.instance.UseSkillCostLighting(lightingCost))
                        {
                            int mid = amount / 2;
                            for (int i = 0; i < amount; i++)
                            {
                                GameObject _gameObject = Instantiate(basicPrefab, bulletTransformIntialized.position, transform.rotation * Quaternion.Euler(new Vector3(0, 0, (i - mid) * fanAngle )));
                                Bullet_Skill_Controller bullet_Skill_Controller = _gameObject.GetComponent<Bullet_Skill_Controller>();
                                bullet_Skill_Controller.SetArrow(transmitDamage, transmitExistTime, transmitSpeed, this, 0, false, damageIncreasePerSkilled_4, !noDestroyAfterDamage, damagepPerTime);
                            }
                        }
                        return;
                    }
                    if (Character_Controller.instance.UseSkillCostLighting(lightingCost))
                    {
                        int mid = amount / 2;
                        for (int i = 0; i < amount; i++)
                        {
                            GameObject _gameObject = Instantiate(basicPrefab, bulletTransformIntialized.position, transform.rotation * Quaternion.Euler(new Vector3(0, 0, (i - mid) * fanAngle )));
                            Bullet_Skill_Controller bullet_Skill_Controller = _gameObject.GetComponent<Bullet_Skill_Controller>();
                            bullet_Skill_Controller.SetArrow(transmitDamage, transmitExistTime, transmitSpeed, this, 0, false, damageIncreasePerSkilled_3, !noDestroyAfterDamage, damagepPerTime);
                        }
                    }
                    return;
                }
                if (Character_Controller.instance.UseSkillCostLighting(lightingCost))
                {
                    int mid = amount / 2;
                    for (int i = 0; i < amount; i++)
                    {
                        GameObject _gameObject = Instantiate(basicPrefab, bulletTransformIntialized.position, transform.rotation * Quaternion.Euler(new Vector3(0, 0, (i - mid) * fanAngle )));
                        Bullet_Skill_Controller bullet_Skill_Controller = _gameObject.GetComponent<Bullet_Skill_Controller>();
                        bullet_Skill_Controller.SetArrow(transmitDamage, transmitExistTime, transmitSpeed, this, 0, false, damageIncreasePerSkilled_2, !noDestroyAfterDamage, damagepPerTime);
                    }
                    return;
                }
            }


            //花费数值
            if (Character_Controller.instance.UseSkillCostLighting(lightingCost))
            {
                int mid = amount / 2;
                for (int i = 0; i < amount; i++)
                {
                    GameObject _gameObject = Instantiate(basicPrefab, bulletTransformIntialized.position, transform.rotation * Quaternion.Euler(new Vector3(0, 0, (i - mid) * fanAngle )));
                    Bullet_Skill_Controller bullet_Skill_Controller = _gameObject.GetComponent<Bullet_Skill_Controller>();
                    bullet_Skill_Controller.SetArrow(transmitDamage, transmitExistTime, transmitSpeed, this, 0, false, damageIncreasePerSkilled_1, !noDestroyAfterDamage, damagepPerTime);
                }
            }
        }
    }

    private IEnumerator MiltiTimes()
    {
        int j = waves;
        int mid = amount / 2;
        while (j > 0)
        {
            for (int i = 0; i < amount; i++)
            {
                GameObject _gameObject = Instantiate(basicPrefab, bulletTransformIntialized.position, transform.rotation * Quaternion.Euler(new Vector3(0, 0, (i - mid) * fanAngle )));
                Bullet_Skill_Controller bullet_Skill_Controller = _gameObject.GetComponent<Bullet_Skill_Controller>();
                bullet_Skill_Controller.SetArrow(transmitDamage, transmitExistTime, transmitSpeed, this, 0, false, damageIncreasePerSkilled_5, noDestroyAfterDamage, damagepPerTime);
            }
            yield return new WaitForSeconds(deltaTime);
            j--;
        }
    }

    protected override void CheckUnlock()
    {
        UnlockbulletWithLighting();
        UnlockbulletWithMoreTimesLocked();

        UnlockbulletWithLowerCoolDown();
        UnlockbulletCanPenetrate();
        UnlockbulletCanHitBack();
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


    public void UnlockbulletWithLowerCoolDown()
    {
        Debug.Log("尝试");
        if (bulletWithLowerCoolDown.unLock)
        {
            Debug.Log("成功");
            bulletWithLowerCoolDownLocked = true;
        }
    }

    public void UnlockbulletCanPenetrate()
    {
        Debug.Log("尝试");
        if (bulletCanPenetrate.unLock)
        {
            Debug.Log("成功");
            bulletCanPenetratelocked = true;
        }
    }

    public void UnlockbulletCanHitBack()
    {
        Debug.Log("尝试");
        if (bulletCanHitBack.unLock)
        {

            Debug.Log("成功");
            bulletCanHitBackLocked = true;
        }
    }


    public void UnlockbulletWithLighting()
    {
        Debug.Log("尝试");
        if (bulletWithLighting.unLock)
        {
            Debug.Log("成功");
            bulletWithLightingUnLocked = true;
            cost = lightingCost;
        }
    }
}
