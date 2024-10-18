using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SK;
using System.Diagnostics;
namespace SK
{

    public class Enemy_Create_Skill : Skill
    {
        Enemy_Robot enemy;
        [SerializeField] private GameObject prefab;
        [SerializeField] private float existTime;
        [SerializeField]private bool destroyAfterDamage;   
        [SerializeField]private float damagepPerTime;
        [SerializeField]private Vector3 offset;
       

        private void Awake()
        {
            enemy = GetComponent<Enemy_Robot>();
        }
        private void Create()
        {
            if (enemy.charactersDetected != null)
            {
                UnityEngine.Debug.Log("Instantiate");
                GameObject newCreate = Instantiate(prefab, enemy.charactersDetected.transform.position + offset, Quaternion.identity);
                Enemy_Create_Skill_Controller enemy_Create_Skill_Controller = newCreate.GetComponent<Enemy_Create_Skill_Controller>();  
                enemy_Create_Skill_Controller.SetUp(existTime,this,damagepPerTime,destroyAfterDamage);
            }
        }
        public override void UseSkill()
        {
            base.UseSkill();
            Create();
        }
    }
}
