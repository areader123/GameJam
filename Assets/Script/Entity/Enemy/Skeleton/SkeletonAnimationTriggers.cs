using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SK
{

    public class SkeletonAnimationTriggers : MonoBehaviour
    {
        private Enemy_Skeleton enemy => GetComponent<Enemy_Skeleton>();
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

        // private void OpenCounterAttackWindow() => enemy.OpenCounterAttackWindow();
        // private void CloseCounterAttackWindow() => enemy.CloseCounterAttackWindow();
    }
}
