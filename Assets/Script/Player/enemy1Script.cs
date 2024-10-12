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

    public float coldtime = 3.0f; // ��ȴʱ��
    private float attackCooldown = 3.0f; // �̶���ȴʱ�䣬��������
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
            // ������ȴ�߼�
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
    //ʷ��ķʼ��������ƶ�
    void move()
    {
        Vector3 direction = (Charaposition - position).normalized; // ���㷽��
        transform.position += direction * moveSpeed * Time.deltaTime; // �ƶ�ʷ��ķ
    }
    //������Ҵ���
    void attack()
    {
        Debug.Log("ʷ��ķʹ���˳�ʪ����Ч��һ��");
    }

    //ʷ��ķ���빥����Χ

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // ����Ƿ������
        {
            if (firstAttack) // ����ǵ�һ�νӴ������̹���
            {
                attack();
                firstAttack = false; 
                coldtime = attackCooldown; 
            }
            isAttack = true; 
        }
    }

    // ʷ��ķ�뿪������Χ
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // ����Ƿ������
        {
            isAttack = false;
            firstAttack = true; // ���������һ�ι�����������
            coldtime = attackCooldown; // �뿪ʱ������ȴʱ��
        }
    }
}
