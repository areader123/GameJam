using System.Collections;
using System.Collections.Generic;
using SK;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.TextCore;
namespace SK
{

    public class Bow_Skill : Skill
    {
        [SerializeField] private GameObject arrow;
        [SerializeField] private Vector3 offset;
        [SerializeField] private float arrowDamage;
    
        [SerializeField] private float arrowExistTime;


        [SerializeField] private float arrowSpeed;
        [SerializeField]private Transform arrowInstantiateTransform;


        private Bow_Skill_Controller bow_Skill_Controller;

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

        public void CreatArrow(Transform transform)
        {
            GameObject newArrow = Instantiate(arrow, arrowInstantiateTransform.position, quaternion.identity);
            bow_Skill_Controller = newArrow.GetComponent<Bow_Skill_Controller>();
            bow_Skill_Controller.SetArrow(arrowDamage, arrowExistTime, arrowSpeed, enemy.charactersDetected,offset,enemy);
        }
        public override void UseSkill()
        {
            base.UseSkill();
            CreatArrow(enemy.transform);
        }
    }
}
