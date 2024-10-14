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


        private Bow_Skill_Controller bow_Skill_Controller;

        private Enemy_Arrower enemy_Arrower;

        protected override void Update()
        {
            base.Update();
        }
        protected override void Start()
        {
            enemy_Arrower = GetComponent<Enemy_Arrower>();
            base.Start();

        }

        public void CreatArrow(Transform transform)
        {
            GameObject newArrow = Instantiate(arrow, transform.position + offset * enemy_Arrower.faceDir, quaternion.identity);
            Debug.Log("创造了剑");
            bow_Skill_Controller = newArrow.GetComponent<Bow_Skill_Controller>();
            bow_Skill_Controller.SetArrow(arrowDamage, arrowExistTime, arrowSpeed, enemy_Arrower.charactersDetected,offset,enemy_Arrower);
        }
        public override void UseSkill()
        {
            base.UseSkill();
            CreatArrow(enemy_Arrower.transform);
        }
    }
}
