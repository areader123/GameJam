using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SK;
using Unity.Mathematics;
public class Robot_Skill_One_Skill : Skill
{
    [SerializeField] private GameObject effect;
    [SerializeField] private Transform effectInstantiateTransform;

    [SerializeField]private float existtime;
    [SerializeField]private Vector3 offset;

    private Robot_Skill_One_Skill_Controller robot_Skill_One_Skill_Controller;
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

    public void CreatEffect()
    {
        GameObject newEffect = Instantiate(effect,transform.position + offset * enemy.faceDir,quaternion.identity);
        robot_Skill_One_Skill_Controller = newEffect.GetComponent<Robot_Skill_One_Skill_Controller>();
        robot_Skill_One_Skill_Controller.SetUpEffect(existtime,enemy.charactersDetected,enemy);
    }
    public override void UseSkill()
    {
        base.UseSkill();
        CreatEffect();
    }
}
