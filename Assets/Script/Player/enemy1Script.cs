using SK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class enemy1Script : MonoBehaviour
{
    public float health;
    public float moveSpeed;
    private Vector3 Charaposition;
    public SK.Character character;
    private Vector3 position;

    private bool isAttack;
    private bool firstAttack = true; 

    public float coldtime = 3.0f; // 冷却时间
    private float attackCooldown = 3.0f; // 固定冷却时间，用于重置
    public float time;

    void Start()
    {
        isAttack = false;
        time = Time.deltaTime;
        moveSpeed = 0.5f;
    }


    void Update()
    {
        Charaposition = character.transform.position;
        position = transform.position;
        //Debug.Log(Cposition);
        if (!isAttack)
        {
            move();
        }
        else
        {
            // 攻击冷却逻辑
            if (coldtime > 0)
            {
                coldtime -= Time.deltaTime;
            }
            else
            {
                attack();
                coldtime = attackCooldown;
            }
        }

    }
    //史莱姆始终向玩家移动
    void move()
    {
        Vector3 direction = (Charaposition - position).normalized; // 计算方向
        transform.position += direction * moveSpeed * Time.deltaTime; // 移动史莱姆
    }
    //碰到玩家触发
    void attack()
    {
        Debug.Log("史莱姆使用了潮湿黏糊，效果一般");
    }

    //史莱姆进入攻击范围

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // 检查是否是玩家
        {
            if (firstAttack) // 如果是第一次接触，立刻攻击
            {
                attack();
                firstAttack = false; 
                coldtime = attackCooldown; 
            }
            isAttack = true; 
        }
    }

    // 史莱姆离开攻击范围
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // 检查是否是玩家
        {
            isAttack = false;
            firstAttack = true; // 重新允许第一次攻击立即发生
            coldtime = attackCooldown; // 离开时重置冷却时间
        }
    }
}
