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


    public float coldtime = 3.0f; // ��ȴʱ��
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
    //ʷ��ķÿ��3������ҳ��
    void dash()
    {
        dashSpeed -= Time.deltaTime * 10;
        Vector3 direction = (Charaposition - position).normalized; 
        transform.position += direction * dashSpeed * Time.deltaTime; 
    }
    //ʷ��ķʼ��������ƶ�
    void move()
    {
        Vector3 direction = (Charaposition - position).normalized; // ���㷽��
        transform.position += direction * moveSpeed * Time.deltaTime; // �ƶ�ʷ��ķ
    }
    //������Ҵ���
    void attack()
    {
        Debug.Log("ʷ��ķʹ���˳�̣�Ч����Ⱥ");
    }

    //ʷ��ķ���빥����Χ

     private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // ����Ƿ������
        {
                attack();
        }
    }
}
