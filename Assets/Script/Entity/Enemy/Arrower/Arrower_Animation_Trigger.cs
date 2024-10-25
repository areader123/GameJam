using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SK
{

    public class Arrower_Animation_Trigger : MonoBehaviour
    {
        private Enemy_Arrower enemy => GetComponent<Enemy_Arrower>();
        private Enemy_Stat enemy_Stat => GetComponent<Enemy_Stat>();
        private void Start()
        {

        }
        private void AnimationFinishTriger()
        {
            enemy.AnimationFinishTrigger();
        }

        private void AttackTrigger()
        {
          //  if (enemy.IsCharacterAttackable())
            {
              enemy.GetComponent<Bow_Skill>().CanUseSkill();
            }
        }
        private void Skill_MultiBullet_Trigger()
        {
          enemy.GetComponent<Enemy_MultiTransmit_Skill>().CanUseSkill();
        }
    }
}
