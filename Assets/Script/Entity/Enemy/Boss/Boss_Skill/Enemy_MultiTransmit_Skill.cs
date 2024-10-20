using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SK;
public class Enemy_MultiTransmit_Skill : Skill
{
    public enum BulletType
    {
        circle, fan
    }
    [SerializeField] private Target target;
    [SerializeField] private GameObject transmit;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float amount;
    [SerializeField] private int angleRange;
    [SerializeField]private int deltaAngle;
    [SerializeField] private float transmitDamage;

    [SerializeField] private float transmitExistTime;


    [SerializeField] private float transmitSpeed;
    [SerializeField] private Transform transmitInstantiateTransform;

    [SerializeField] private bool destroySelfAfterDamage;
    [SerializeField] private float damagepPerTime;

    [SerializeField] private float timePerSplit;
    [SerializeField] private BulletType bulletType;
    [SerializeField]private int fanAngle;
    [SerializeField]private bool canStickIn;


    private Enemy_MultiTransmit_Skill_Controller enemy_MultiTransmit_Skill_Controller;

    private Enemy enemy;

    protected override void Update()
    {
        base.Update();
    }
    protected override void Start()
    {
        enemy = GetComponent<Enemy>();
        base.Start();
    }

    public void CreatArrow()
    {

        if (bulletType == BulletType.circle)
        {
            for (int i = 0; i < amount; i++)
            {
                GameObject newArrow = Instantiate(transmit, transmitInstantiateTransform.position + offset, transform.rotation * Quaternion.AngleAxis((angleRange / amount * i)+Random.Range(0,deltaAngle), Vector3.forward));
                enemy_MultiTransmit_Skill_Controller = newArrow.GetComponent<Enemy_MultiTransmit_Skill_Controller>();
                enemy_MultiTransmit_Skill_Controller.SetArrow(transmitDamage, transmitExistTime, transmitSpeed, damagepPerTime, destroySelfAfterDamage, this, target, timePerSplit,canStickIn);
            }
        }
        if(bulletType == BulletType.fan)
        {
            int mid = (int)(amount /2);
             for (int i = 0; i < amount; i++)
            {
                GameObject newArrow = Instantiate(transmit, transmitInstantiateTransform.position + offset,Quaternion.Euler(new Vector3(0,0,(i - mid)*fanAngle + 180 * (enemy.faceDir-1)/2)));
                enemy_MultiTransmit_Skill_Controller = newArrow.GetComponent<Enemy_MultiTransmit_Skill_Controller>();
                enemy_MultiTransmit_Skill_Controller.SetArrow(transmitDamage, transmitExistTime, transmitSpeed, damagepPerTime, destroySelfAfterDamage, this, target, timePerSplit,canStickIn);
            }
        }
    }
    public override void UseSkill()
    {
        base.UseSkill();
        CreatArrow();
    }
}
