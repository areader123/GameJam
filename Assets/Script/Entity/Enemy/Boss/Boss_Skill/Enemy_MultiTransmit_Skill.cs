using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SK;
public class Enemy_MultiTransmit_Skill : Skill
{
    [SerializeField]private Target target;
    [SerializeField] private GameObject transmit;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float amount;
    [SerializeField]private int angleRange;
    [SerializeField] private float transmitDamage;

    [SerializeField] private float transmitExistTime;


    [SerializeField] private float transmitSpeed;
    [SerializeField] private Transform transmitInstantiateTransform;

    [SerializeField] private bool destroySelfAfterDamage;
    [SerializeField] private float damagepPerTime;

    [SerializeField]private float  timePerSplit;


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

        for (int i = 0; i < amount; i++)
        {
            GameObject newArrow = Instantiate(transmit, transmitInstantiateTransform.position + offset, Quaternion.AngleAxis(angleRange / amount * i, Vector3.forward));
            enemy_MultiTransmit_Skill_Controller = newArrow.GetComponent<Enemy_MultiTransmit_Skill_Controller>();
            enemy_MultiTransmit_Skill_Controller.SetArrow(transmitDamage, transmitExistTime, transmitSpeed , damagepPerTime, destroySelfAfterDamage,  this,target,timePerSplit);
        }
    }
    public override void UseSkill()
    {
        base.UseSkill();
        CreatArrow();
    }
}
