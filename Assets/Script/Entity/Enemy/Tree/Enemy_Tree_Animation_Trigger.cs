using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SK;
public class Enemy_Tree_Animation_Trigger : MonoBehaviour
{
    private Enemy_Robot enemy => GetComponent<Enemy_Robot>();
        private Enemy_Stat enemy_Stat => GetComponent<Enemy_Stat>();
        private void Start()
        {

        }
        private void AnimationTriger()
        {
            enemy.AnimationFinishTrigger();
        }

        private void AttackTrigger()
        {
            if (enemy.IsCharacterAttackable())
            {
                enemy.charactersDetected.GetComponent<Character_Stat>().DoDamage(enemy_Stat);
            }
        }

        private void Attack2Trigger()
        {
            enemy.GetComponent<Enemy_Create_Skill>().CanUseSkill();
        }
}
