using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SK;
public class Enemy_Boss_Animation_Trigger : MonoBehaviour
{
    private Enemy_Boss enemy_Boss => GetComponent<Enemy_Boss>();
    private Enemy_Stat enemy_Stat => GetComponent<Enemy_Stat>();

    private void AnimationTrigger()
    {
        enemy_Boss.AnimationFinishTrigger();
    }
    private void AnimationAttack_Two_Trigger()
    {
        enemy_Boss.GetComponent<Enemy_MultiTransmit_Skill>().CanUseSkill();
    }

    private void AnimationAttack_Three_Trigger()
    {
        enemy_Boss.GetComponent<Enemy_Roar_Skill>().CanUseSkill();
    }

    private void AnimationAttack_Three_TimeCounter_Trigger()
    {
        enemy_Boss.boss_Attack_Three_State.animationCounter = enemy_Boss.Attack_Three_Animation_Duration;
        enemy_Boss.animator.SetBool("Attack_Three_Second", true);
    }
    private void AnimationAttack_Two_TimeCounter_Trigger()
    {
        enemy_Boss.boss_Attack_Two_State.animationCounter = enemy_Boss.Attack_Two_Animation_Duration;
        enemy_Boss.animator.SetBool("Attack_Two_Second", true);
    }
    private void AnimationAttack_One_TimeCounter_Trigger()
    {
        enemy_Boss.boss_Attack_One_State.animationCounter = enemy_Boss.Attack_One_Animation_Duration;
        enemy_Boss.animator.SetBool("Attack_One_Second", true);
    }
}
