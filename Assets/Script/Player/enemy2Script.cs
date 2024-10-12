using SK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class enemy2Script : MonoBehaviour
{
    public float health;
    public float dashSpeed;
    public float moveSpeed;
    private Vector3 Charaposition;
    public SK.Character character;
    private Vector3 position;


    public float coldtime = 3.0f; // 冷却时间
    public float time;

    void Start()
    {
        dashSpeed = 5;
        moveSpeed = 0.2f;
    }


    void Update()
    {
        Charaposition = character.transform.position;
        position = transform.position;
        time += Time.deltaTime;
        if (time >= 3 && Vector3.Distance(Charaposition, position) <= 2)
        {
            dash();
            if (time >= 3.5)
            {
                time = 0;
                dashSpeed = 5;
            }
        }
        else
        {
            move();
        }
        
    }
    //史莱姆每过3秒向玩家冲刺
    void dash()
    {
        dashSpeed -= Time.deltaTime * 10;
        Vector3 direction = (Charaposition - position).normalized; 
        transform.position += direction * dashSpeed * Time.deltaTime; 
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
        Debug.Log("史莱姆使用了冲刺，效果拔群");
    }

    //史莱姆进入攻击范围

     private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // 检查是否是玩家
        {
                attack();
        }
    }
}
