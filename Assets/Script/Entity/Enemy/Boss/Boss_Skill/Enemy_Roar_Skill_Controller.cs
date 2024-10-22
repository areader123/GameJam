using System;
using System.Collections;
using System.Collections.Generic;
using SK;
using UnityEngine;

public class Enemy_Roar_Skill_Controller : MonoBehaviour
{
    private enum TypeSkill
    {
        call, buff
    }
    [SerializeField] private TypeSkill typeSkill;
    [Header("Buff")]
    [SerializeField] private StatType statType;
    [SerializeField] private int increaseAmount;
    private float increaseSpeed;
    private float buffExsitTime;
    private float bufferPrefabExsitCounter;
    private float buffPrefabExistTime;
    private void Awake()
    {
        bufferPrefabExsitCounter = buffPrefabExistTime;
        
    }

    private void Update()
    {
        bufferPrefabExsitCounter -= Time.deltaTime;
        if (typeSkill == TypeSkill.buff)
        {
            if (bufferPrefabExsitCounter < 0)
                Destroy(gameObject);
        }
    }

    public void SetUpCall(Enemy enemy,float increaseSpeed)
    {
        this.increaseSpeed = increaseSpeed;
        //被召唤的怪物可以升级技能
        if (enemy != null)
        {
            enemy.battleSpeed += increaseSpeed;
        }
    }

    public void SetUpBuffs(Enemy enemy,float increaseSpeed,float buffExsitTime,float buffPrefabExistTime)
    {
        this.increaseSpeed = increaseSpeed;
        this.buffExsitTime = buffExsitTime;
        this.buffPrefabExistTime = buffPrefabExistTime;
        Enemy_Stat enemy_Stat = enemy.GetComponent<Enemy_Stat>();
        enemy_Stat.IncreaseStatBy(increaseAmount, buffExsitTime, enemy_Stat.GetStat(statType));
        if (enemy != null)
        {
            enemy.battleSpeed += increaseSpeed;
        }
    }
}
