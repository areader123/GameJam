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
        [SerializeField]private float timePerDamage;
        [SerializeField]private Vector3 offset;
        [SerializeField]private float damageValue;
       

        private void Awake()
        {
            enemy = GetComponent<Enemy_Robot>();
        }
        private void Create()
        {
            if (enemy.charactersDetected != null)
            {
                UnityEngine.Debug.Log("Instantiate");
                GameObject newCreate = Instantiate(prefab, RandomPosition () , Quaternion.identity);
                Enemy_Create_Skill_Controller enemy_Create_Skill_Controller = newCreate.GetComponent<Enemy_Create_Skill_Controller>();  
                enemy_Create_Skill_Controller.SetUp(existTime,this,timePerDamage,destroyAfterDamage,damageValue);
            }
        }

        private Vector3 RandomPosition () 
        {
            float randomX = Random.Range(enemy.charactersDetected.transform.position.x - offset.x,enemy.charactersDetected.transform.position.x + offset.x);
          //  float randomY = Random.Range(enemy.charactersDetected.transform.position.y - offset.y,enemy.charactersDetected.transform.position.y + offset.y);
          return new Vector3(randomX,enemy.charactersDetected.transform.position.y + offset.y,0);
        }
        public override void UseSkill()
        {
            base.UseSkill();
            Create();
        }
    }
}
