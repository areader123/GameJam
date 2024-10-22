using System.Collections;
using System.Collections.Generic;
using SK;
using UnityEngine;
using UnityEngine.UI;

public class Blood_Skill : Skill
{
   public bool speedUpLocked;
   [SerializeField] private UI_Skill_Slot speedUp;
   [Space(10)]
   public bool moreSpeedMoreDamageLocked;
   [SerializeField] private UI_Skill_Slot moreSpeedMoreDamage;
   [Space(10)]
   public bool changeWithEnemyLocked;
   [SerializeField] private UI_Skill_Slot changeWithEnemy;
   [Space(10)]
   public bool changeWithEnemyLeaveCloneLocked;
   [SerializeField] private UI_Skill_Slot changeWithEnemyLeaveClone;
   [Space(10)]

   [SerializeField] private UI_SkillUsed_Slot uI_SkillUsed_Slot;
   public bool speedUpSkillUsedLocked;

   protected override void Start()
   {
      base.Start();
      speedUp.GetComponent<Button>().onClick.AddListener(UnlockSpeedUp);
      moreSpeedMoreDamage.GetComponent<Button>().onClick.AddListener(UnlockmoreSpeedMoreDamage);
      changeWithEnemy.GetComponent<Button>().onClick.AddListener(UnlockchangeWithEnemy);
      changeWithEnemyLeaveClone.GetComponent<Button>().onClick.AddListener(UnlockchangeWithEnemyLeaveClone);
   }
   public override void UseSkill()
   {
      base.UseSkill();
   
   }
   protected override void CheckUnlock()
   {
      base.CheckUnlock();
      UnlockSpeedUp();
      UnlockmoreSpeedMoreDamage();
      UnlockchangeWithEnemy();
      UnlockchangeWithEnemyLeaveClone();
   }

   private void UnlockSpeedUp()
   {
      Debug.Log("尝试");
      if (speedUp.unLock)
      {
         Debug.Log("成功");
         speedUpLocked = true;
      }
   }

   private void UnlockmoreSpeedMoreDamage()
   {
      Debug.Log("尝试");
      if (moreSpeedMoreDamage.unLock)
      {
         Debug.Log("成功");
         moreSpeedMoreDamageLocked = true;
      }
   }
   private void UnlockchangeWithEnemy()
   {
      Debug.Log("尝试");
      if (changeWithEnemy.unLock)
      {
         Debug.Log("成功");
         changeWithEnemyLocked = true;
      }
   }
   private void UnlockchangeWithEnemyLeaveClone()
   {
      Debug.Log("尝试");
      if (changeWithEnemyLeaveClone.unLock)
      {
         Debug.Log("成功");
         changeWithEnemyLeaveCloneLocked = true;
      }
   }


   // private void OnDrawGizmos()
   // {
   //     Gizmos.DrawWireSphere(character.transform.position, findFarthestEnemyRadius);
   // }
}
