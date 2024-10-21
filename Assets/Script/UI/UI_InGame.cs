using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace SK
{

    public class UI_InGame : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private Character_Stat character_Stat;

        [SerializeField] private Image FirstCoolDownImage;

        private float FirstCoolDown;
        [SerializeField] private Image parryCoolDownImage;

     

     

      

        [SerializeField] private TextMeshProUGUI lightingNumberText;
        [SerializeField] private TextMeshProUGUI expNumberText;
        [SerializeField] private TextMeshProUGUI levelNumberText;
        [SerializeField] private TextMeshProUGUI SkillPointNumberText;


        [SerializeField] private Image SecondCoolDownImage;
        private float SecondCoolDown;

        private UI uI;
        [SerializeField]private UI_SkillTree_Slot uI_SkillTree_Slot;

        [SerializeField]private List<UI_Skill_CoolDown_Slot> uI_Skill_CoolDown_Slots;




        private void Awake()
        {
            uI = GetComponentInParent<UI>();
        }

        private void Start()
        {

            character_Stat = Character_Controller.instance.character.GetComponent<Character_Stat>();
            character_Stat.OnHealthChange += UpdataHealthBar;
            // if (character_Stat != null)
            // {
            //     character_Stat.OnHealthChange += UpdataHealthBar;
            // }
            //???????? 报错
            // dashCoolDown = SkillManager.instance.dash_Skill.cooldown;
            // SecondCoolDown = SkillManager.instance.Second_Skill.cooldown;
            // parryCoolDown = skillManger.parry_Skill.cooldown;
            // crystalCoolDown = skillManger.crystal_Skill.cooldown;
            // swordCoolDown = skillManger.throwSword.cooldown;
            // blackholeCoolDown = skillManger.blackHole.cooldown;
        }


        // Update is called once per frame
        void Update()
        {

            lightingNumberText.text = Character_Controller.instance.GetLightingNumber().ToString("#,#");
            expNumberText.text = Character_Controller.instance.GetExp().ToString("#,#");
            levelNumberText.text = Character_Controller.instance.GetLevel().ToString("#,#");
            SkillPointNumberText.text = Character_Controller.instance.pointCanbeUsed.ToString();


            UpdateSkillShowedSlot();
            SetCoolDown();
            CheckCoolDown();
            // if (Input.GetKeyDown(KeyCode.LeftShift) && SkillManager.instance.dash_Skill.dashUnlocked && SkillManager.instance.dash_Skill.dashSkillUsedUnlocked)
            // {
            //     SetCoolDown(FirstCoolDownImage);
            // }
            // // if (Input.GetKeyDown(KeyCode.Q) && skillManger.parry_Skill.parryUnlock)
            // // {
            // //     SetCoolDown(parryCoolDownImage);
            // // }
            // // if (Input.GetKeyDown(KeyCode.F) && skillManger.crystal_Skill.crystalUnlock)
            // // {
            // //     SetCoolDown(crystalCoolDownImage);
            // // }
            // // if (Input.GetKeyUp(KeyCode.Mouse1))
            // // {
            // //     SetCoolDown(swordCoolDownImage);
            // // }
            // // if (Input.GetKeyDown(KeyCode.N))
            // // {
            // //     SetCoolDown(blackholeCoolDownImage);
            // // }
            // // if (Input.GetKeyDown(KeyCode.Alpha1) && Inventor.instance.GetSingleEquipment(Equipment.Flask) != null)
            // // {
            // //     SetCoolDown(flaskCoolDownImage);
            // // }
            // if (Input.GetKeyDown(KeyCode.C) && SkillManager.instance.clone_Skill.cloneUnlocked && SkillManager.instance.clone_Skill.cloneSkillUsedUnlock)
            // {
            //     SetCoolDown(SecondCoolDownImage);
            // }

            // CheckCoolDownOf(FirstCoolDownImage, SkillManager.instance.dash_Skill.cooldown);
            // // CheckCoolDownOf(parryCoolDownImage, parryCoolDown);
            // // CheckCoolDownOf(crystalCoolDownImage, crystalCoolDown);
            // // CheckCoolDownOf(swordCoolDownImage, swordCoolDown);
            // // CheckCoolDownOf(blackholeCoolDownImage, blackholeCoolDown);
            // // CheckCoolDownOf(flaskCoolDownImage, Inventor.instance.flaskCoolDown);
            // CheckCoolDownOf(SecondCoolDownImage, SkillManager.instance.clone_Skill.cooldown);
        }

        private void UpdateSkillShowedSlot()
        {
            // UI_SkillTree_Slot uI_SkillTree_Slot = uI.skillTree.GetComponent<UI_SkillTree_Slot>();
            UI_SkillUsed_Slot[] uI_SkillUsed_Slots = uI.uI_SkillUsed_Slots;
            int i = 0;
            foreach (UI_SkillUsed_Slot obj in uI_SkillUsed_Slots)
            {
                if (obj.Unlock)
                {
                    uI_Skill_CoolDown_Slots[i].SetUpSkillCoolDownSlot(obj.uI_Skill_Slot, obj.skill);
                    i++;
                }
            }
        }

        private void CheckCoolDown()
        {
            for(int i = 0; i <uI_Skill_CoolDown_Slots.Count ; i++) 
            {
                if(uI_Skill_CoolDown_Slots[i] != null)
                {
                    uI_Skill_CoolDown_Slots[i].CheckCoolDown();
                }
            }
        }
        private void SetCoolDown()
        {
             for(int i = 0; i <uI_Skill_CoolDown_Slots.Count ; i++) 
            {
                if(uI_Skill_CoolDown_Slots[i] != null)
                {
                    uI_Skill_CoolDown_Slots[i].SetCoolDown();
                }
            }
        }

        private void UpdataHealthBar()
        {
            slider.maxValue = character_Stat.GetMaxHealth();
            slider.value = character_Stat._currentHP;
        }
    }

}
