using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SK
{

    public class SkeletonAnimationTriggers : MonoBehaviour
    {
        private Enemy_Skeleton enemy => GetComponent<Enemy_Skeleton>();
       // private Enemy_Stat enemy_Stat => GetComponentInParent<Enemy_Stat>();
        private void Start()
        {

        }
        private void AnimationTriger()
        {
            enemy.AnimationFinishTrigger();
        }

        // private void AttackTrigger()
        // {
        //     Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius);

        //     foreach (var hit in colliders)
        //     {
        //         if (hit.GetComponent<Player_Stat>() != null)
        //         {
        //             //enemy_Stat.DoDamage(hit.GetComponent<Player_Stat>());
        //             hit.GetComponent<Player_Stat>().DoDamage(enemy_Stat);
        //         }
        //     }
        // }

        // private void OpenCounterAttackWindow() => enemy.OpenCounterAttackWindow();
        // private void CloseCounterAttackWindow() => enemy.CloseCounterAttackWindow();
    }
}
