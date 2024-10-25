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
        [SerializeField] private Transform arrowInstantiateTransform;

        [SerializeField] private bool destroySelfAfterDamage;
        [SerializeField] private float damagepPerTime;
        [SerializeField] private bool RotationWhileDamage;
        [SerializeField] private bool slowRotationWhileDamage;
        [SerializeField] private float slowRotationSpeed;

        [SerializeField]private bool stickIn;


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

        public void CreatArrow()
        {
            GameObject newArrow = Instantiate(arrow, arrowInstantiateTransform.position, quaternion.identity,this.transform);
            bow_Skill_Controller = newArrow.GetComponent<Bow_Skill_Controller>();
            if (enemy.charactersDetected != null)
            {
                bow_Skill_Controller.SetArrow(arrowDamage, arrowExistTime, arrowSpeed, enemy.charactersDetected, offset, enemy, damagepPerTime, destroySelfAfterDamage, RotationWhileDamage, this, slowRotationWhileDamage, slowRotationSpeed, arrowInstantiateTransform,stickIn);
            }
        }
        public override void UseSkill()
        {
            base.UseSkill();
            CreatArrow();
        }
    }
}
