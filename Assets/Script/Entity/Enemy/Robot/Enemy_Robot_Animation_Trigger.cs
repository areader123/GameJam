using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SK
{

    public class Enemy_Robot_Animation_Trigger : MonoBehaviour
    {
        private Enemy_Robot enemy_Robot => GetComponent<Enemy_Robot>();
        private Enemy_Stat enemy_Stat => GetComponent<Enemy_Stat>();

        private void AnimationTrigger()
        {
            enemy_Robot.AnimationFinishTrigger();
        }

        private void AttackAnimationTrigger()
        {
            enemy_Robot.GetComponent<Bow_Skill>().CanUseSkill();
        }

        private void Skill_OneAnimationTrigger()
        {
            //enemy_Robot.GetComponent<Robot_Skill_One_Skill>().CanUseSkill();
        }
    }
}
