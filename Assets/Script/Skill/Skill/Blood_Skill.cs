using System.Collections;
using System.Collections.Generic;
using SK;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Blood_Skill : Skill
{
   public bool bloodLocked;
   private float skillTimeCounter;
   [SerializeField] private int basicBlood_1;
   private bool basic_1;
   [SerializeField] private float skillDuration;
   [SerializeField] private int bloodPercent;
   [SerializeField] private int lightingCost;
   [SerializeField] private UI_Skill_Slot blood;
   [Space(10)]
   public bool morebloodLocked;
   [SerializeField] private int basicBlood_2;
   private bool basic_2;
   [SerializeField][Range(0, 1)] private float newBloodPercent;
   [SerializeField] private UI_Skill_Slot moreblood;
   [Space(10)]
   public bool bloodTransformedLightingLocked;
   [SerializeField] private int basicBlood_3;
   private bool basic_3;
   [SerializeField][Range(0, 1)] private float bloodToLightingPercent;
   private bool blooding_1;
   [SerializeField] private UI_Skill_Slot bloodTransformedLighting;
   [Space(10)]
   public bool bloodMoreLightingMoreTimeLocked;
   private bool blooding_2;
   [SerializeField] private int basicBlood_4;
   private bool basic_4;
   [SerializeField] private int newlightingCost;
   [SerializeField] private float newSkillDuration;
   [SerializeField] private UI_Skill_Slot bloodMoreLightingMoreTime;
   [Space(10)]
   [SerializeField] private UI_SkillUsed_Slot uI_SkillUsed_Slot;
   public bool bloodSkillUsedLocked;


   private Character_Stat character_Stat;
   private void Awake()
   {

      character_Stat = Character_Controller.instance.character.GetComponent<Character_Stat>();
   }

   protected override void Update()
   {
      base.Update();
      skillTimeCounter -= Time.deltaTime;
   }

   protected override void Start()
   {
      base.Start();
      blood.GetComponent<Button>().onClick.AddListener(Unlockblood);
      moreblood.GetComponent<Button>().onClick.AddListener(Unlockmoreblood);
      bloodTransformedLighting.GetComponent<Button>().onClick.AddListener(UnlockbloodTransformedLighting);
      bloodMoreLightingMoreTime.GetComponent<Button>().onClick.AddListener(UnlockbloodMoreLightingMoreTime);
   }
   public override void UseSkill()
   {
      base.UseSkill();
      Blood();
   }

   public void Blood()
   {
      if (bloodLocked && uI_SkillUsed_Slot.Unlock)
      {
         bloodSkillUsedLocked = uI_SkillUsed_Slot.Unlock;
         if (morebloodLocked)
         {
            if (Character_Controller.instance.UseSkillCostLighting(lightingCost))
               Blood_Two();
            //高比例
            if (bloodTransformedLightingLocked)
            {
               if (bloodMoreLightingMoreTimeLocked)
               {
                  if (Character_Controller.instance.UseSkillCostLighting((int)newlightingCost))
                  {
                     StopCoroutine("BloodToLightingCounter_2");
                     StartCoroutine("BloodToLightingCounter_2");
                  }
                  return;
                  //更多消耗 和 更多时间
               }

               if (Character_Controller.instance.UseSkillCostLighting(lightingCost))
               {
                  StopCoroutine("BloodToLightingCounter");
                  StartCoroutine("BloodToLightingCounter");
               }
               return;
               //吸血回复光亮
            }

         }
         if (Character_Controller.instance.UseSkillCostLighting(lightingCost))
            Blood_One();
         //低比例吸血
      }
   }



   private void Blood_One()
   {
      if (Character_Controller.instance.UseSkillCostLighting(lightingCost))
      {
         Debug.Log("Blood_One");
         character_Stat.IncreaseStatBy((int)bloodPercent, skillDuration, character_Stat.GetStat(StatType.Blood));
      }
   }
   private void Blood_Two()
   {
      if (Character_Controller.instance.UseSkillCostLighting(lightingCost))
      {

         if (Character_Controller.instance.UseSkillCostLighting(lightingCost))
         {
            character_Stat.IncreaseStatBy((int)newBloodPercent, skillDuration, character_Stat.GetStat(StatType.Blood));
         }
      }
   }


   private IEnumerator BloodToLightingCounter()
   {
      blooding_1 = true;
      yield return new WaitForSeconds(skillDuration);
      blooding_1 = false;
   }
   private IEnumerator BloodToLightingCounter_2()
   {
      blooding_2 = true;
      yield return new WaitForSeconds(newSkillDuration);
      blooding_2 = false;
   }
   public void BloodToLighting(int damage)
   {
      if (blooding_1)
      {
         if (character_Stat._currentHP == character_Stat.GetMaxHealth())
         {
            int times = (int)(damage * bloodToLightingPercent);
            for (int i = 0; i < times; i++)
            {
               if (Character_Controller.instance.GetLightingNumber() < Character_Controller.instance.GetMaxLightingNumber())
                  Character_Controller.instance.AddLightingNumber();
            }
            skillTimeCounter = skillDuration;
         }

      }

   }

   public void MoreLightingMoreDuration(int damage)
   {
      if (blooding_2)
      {
         // if (character_Stat._currentHP == character_Stat.GetMaxHealth())
         {
            int times = (int)(damage * bloodToLightingPercent);
            Debug.Log("times" + times);
            for (int i = 0; i < times; i++)
            {
               if (Character_Controller.instance.GetLightingNumber() < Character_Controller.instance.GetMaxLightingNumber())
                  Character_Controller.instance.AddLightingNumber();
            }
            skillTimeCounter = newSkillDuration;
         }
      }
   }


   protected override void CheckUnlock()
   {
      base.CheckUnlock();
      Unlockblood();
      Unlockmoreblood();
      UnlockbloodTransformedLighting();
      UnlockbloodMoreLightingMoreTime();
   }

   private void Unlockblood()
   {
      Debug.Log("尝试");
      if (blood.unLock)
      {
         Debug.Log("成功");
         bloodLocked = true;
         if (!basic_1)
         {
            character_Stat.GetStat(StatType.Blood).AddModifiers(basicBlood_1);
            cost = lightingCost;
            basic_1 = true;
         }
      }
   }

   private void Unlockmoreblood()
   {
      Debug.Log("尝试");
      if (moreblood.unLock)
      {
         Debug.Log("成功");
         morebloodLocked = true;
         if (!basic_2)
         {
            basic_2 = true;
            character_Stat.GetStat(StatType.Blood).AddModifiers(basicBlood_2);
         }
      }
   }
   private void UnlockbloodTransformedLighting()
   {
      Debug.Log("尝试");
      if (bloodTransformedLighting.unLock)
      {
         Debug.Log("成功");
         bloodTransformedLightingLocked = true;
         if (!basic_3)
         {
            basic_3 = true;
            character_Stat.GetStat(StatType.Blood).AddModifiers(basicBlood_3);
         }
      }
   }
   private void UnlockbloodMoreLightingMoreTime()
   {
      Debug.Log("尝试");
      if (bloodMoreLightingMoreTime.unLock)
      {
         Debug.Log("成功");
         bloodMoreLightingMoreTimeLocked = true;
         if (!basic_4)
         {
            basic_4 = true;
            cost = newlightingCost;
            character_Stat.GetStat(StatType.Blood).AddModifiers(basicBlood_4);
         }
      }
   }


   // private void OnDrawGizmos()
   // {
   //     Gizmos.DrawWireSphere(character.transform.position, findFarthestEnemyRadius);
   // }
}
