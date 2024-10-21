using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SK;
using UnityEngine.AI;
using UnityEngine.UI;
public class ChangeWithEnemy_Skill : Skill
{

    public bool speedUpLocked;
    [SerializeField] private float speedUpAmount;
    [SerializeField] private float speedUpTimeDuration;
    [SerializeField] private UI_Skill_Slot speedUp;
    [Space(10)]
    public bool moreSpeedMoreDamageLocked;
    [SerializeField] private float strengthAddedPerSpeed;
    [SerializeField] private float strengthTimeDuration;
    [SerializeField] private UI_Skill_Slot moreSpeedMoreDamage;
    [Space(10)]
    public bool changeWithEnemyLocked;
    [SerializeField] private float changeWithEnemyNewCoolDown;
    Character character;
    [SerializeField] private float findFarthestEnemyRadius;
    [SerializeField] private UI_Skill_Slot changeWithEnemy;
    [Space(10)]
    public bool changeWithEnemyLeaveCloneLocked;
    [SerializeField] private float changeWithEnemyLeaveCloneNewCoolDown;
    [SerializeField] private UI_Skill_Slot changeWithEnemyLeaveClone;
    [Space(10)]

    [SerializeField] private UI_SkillUsed_Slot uI_SkillUsed_Slot;
    public bool speedUpSkillUsedLocked;

    protected override void Start()
    {
        base.Start();
        character = Character_Controller.instance.character;
        speedUp.GetComponent<Button>().onClick.AddListener(UnlockSpeedUp);
        moreSpeedMoreDamage.GetComponent<Button>().onClick.AddListener(UnlockmoreSpeedMoreDamage);
        changeWithEnemy.GetComponent<Button>().onClick.AddListener(UnlockchangeWithEnemy);
        changeWithEnemyLeaveClone.GetComponent<Button>().onClick.AddListener(UnlockchangeWithEnemyLeaveClone);
    }
    public override void UseSkill()
    {
        base.UseSkill();
        if (speedUpLocked && uI_SkillUsed_Slot.Unlock)
        {
            //玩家短暂加速
            speedUpSkillUsedLocked = uI_SkillUsed_Slot.Unlock;
            StopCoroutine("SpeedUp");
            StartCoroutine("SpeedUp");
            Debug.Log("玩家短暂加速");
            if (moreSpeedMoreDamageLocked)
            {
                Character_Stat character_Stat = Character_Controller.instance.character.GetComponent<Character_Stat>();
                character_Stat.IncreaseStatBy((int)(strengthAddedPerSpeed * Character_Controller.instance.character.movementSpeed), strengthTimeDuration, character_Stat.strength);
                Debug.Log("根据加速时间 获得一定时间的伤害加成");
                //根据加速时间 获得一定时间的伤害加成
                if (changeWithEnemyLocked)
                {
                    if (changeWithEnemyLeaveCloneLocked)
                    {
                        cooldown = changeWithEnemyLeaveCloneNewCoolDown;
                        Debug.Log("创造克隆体");
                        ChangeWithEnemyLeaveClone();
                        //创造克隆体
                        return;
                    }
                    Debug.Log("交换位置");
                    cooldown = changeWithEnemyNewCoolDown;
                    ChangeWithEnemy();

                }
            }
        }
    }

    private IEnumerator SpeedUp()
    {
        Character_Controller.instance.character.movementSpeed += speedUpAmount;
        yield return new WaitForSeconds(speedUpTimeDuration);
        Character_Controller.instance.character.movementSpeed -= speedUpAmount;
    }

    protected override void CheckUnlock()
    {
        base.CheckUnlock();
        UnlockSpeedUp();
        UnlockmoreSpeedMoreDamage();
        UnlockchangeWithEnemy();
        UnlockchangeWithEnemyLeaveClone();
    }

    private void UnlockSpeedUp()
    {
        Debug.Log("尝试");
        if (speedUp.unLock)
        {
            Debug.Log("成功");
            speedUpLocked = true;
        }
    }

    private void UnlockmoreSpeedMoreDamage()
    {
        Debug.Log("尝试");
        if (moreSpeedMoreDamage.unLock)
        {
            Debug.Log("成功");
            moreSpeedMoreDamageLocked = true;
        }
    }
    private void UnlockchangeWithEnemy()
    {
        Debug.Log("尝试");
        if (changeWithEnemy.unLock)
        {
            Debug.Log("成功");
            changeWithEnemyLocked = true;
        }
    }
    private void UnlockchangeWithEnemyLeaveClone()
    {
        Debug.Log("尝试");
        if (changeWithEnemyLeaveClone.unLock)
        {
            Debug.Log("成功");
            changeWithEnemyLeaveCloneLocked = true;
        }
    }

    private void ChangeWithEnemyLeaveClone()
    {
        ExchangePositionLeaveClone();
    }

    private void ChangeWithEnemy()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(character.transform.position, findFarthestEnemyRadius);
        float Max = 0;

        GameObject enemy = null;
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {

                float distance = Vector2.Distance(hit.transform.position, character.transform.position);
                if (distance > Max)
                {
                    Max = distance;
                    enemy = hit.gameObject;
                    //hit.gameObject.transform.position = character.transform.position;
                }
            }
        }
        if (enemy != null)
        {
            character.transform.position = enemy.transform.position;
            Transform temp = character.transform;
            enemy.transform.position = temp.position;
        }
    }


    private void ExchangePositionLeaveClone()
    {
        SkillManager.instance.clone_Skill.UseByOutside();
        ChangeWithEnemy();
    }
    private void ExchangePosition(Transform _transform)
    {
        Character_Controller.instance.character.transform.position = _transform.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(character.transform.position, findFarthestEnemyRadius);
    }
}
