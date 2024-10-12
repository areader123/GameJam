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

        [SerializeField] private Image dashCoolDownImage;
        [SerializeField] private Image dashCoolDownImageLocked;
        private float dashCoolDown;
        [SerializeField] private Image parryCoolDownImage;
        private float parryCoolDown;

        [SerializeField] private Image crystalCoolDownImage;
        private float crystalCoolDown;

        [SerializeField] private Image swordCoolDownImage;
        private float swordCoolDown;
        [SerializeField] private Image blackholeCoolDownImage;
        private float blackholeCoolDown;

        [SerializeField] private Image flaskCoolDownImage;
        private float flaskCoolDown;

        [SerializeField] private TextMeshProUGUI currencyText;

        [SerializeField] private Image cloneCoolDownImage;
        [SerializeField] private Image cloneCoolDownImageLocked;
        private float cloneCoolDown;


        private int currency;


        private void Awake()
        {
        }

        private void Start()
        {

            //character_Stat = Character_Controller.instance.character.GetComponent<Character_Stat>();

            // if (character_Stat != null)
            // {
            //     character_Stat.OnHealthChange += UpdataHealthBar;
            // }
            //???????? 报错
            // dashCoolDown = SkillManager.instance.dash_Skill.cooldown;
            // cloneCoolDown = SkillManager.instance.clone_Skill.cooldown;
            // parryCoolDown = skillManger.parry_Skill.cooldown;
            // crystalCoolDown = skillManger.crystal_Skill.cooldown;
            // swordCoolDown = skillManger.throwSword.cooldown;
            // blackholeCoolDown = skillManger.blackHole.cooldown;
        }


        // Update is called once per frame
        void Update()
        {

            //currencyText.text = Character_Controller.instance.GetCurrency().ToString("#,#");
            if( SkillManager.instance.dash_Skill.dashUnlocked)
            {
                dashCoolDownImageLocked.fillAmount =0;
            }
            if( SkillManager.instance.clone_Skill.cloneUnlocked)
            {
                cloneCoolDownImageLocked.fillAmount =0;
            }

            if (Input.GetKeyDown(KeyCode.LeftShift) && SkillManager.instance.dash_Skill.dashUnlocked)
            {
                SetCoolDown(dashCoolDownImage);
            }
            // if (Input.GetKeyDown(KeyCode.Q) && skillManger.parry_Skill.parryUnlock)
            // {
            //     SetCoolDown(parryCoolDownImage);
            // }
            // if (Input.GetKeyDown(KeyCode.F) && skillManger.crystal_Skill.crystalUnlock)
            // {
            //     SetCoolDown(crystalCoolDownImage);
            // }
            // if (Input.GetKeyUp(KeyCode.Mouse1))
            // {
            //     SetCoolDown(swordCoolDownImage);
            // }
            // if (Input.GetKeyDown(KeyCode.N))
            // {
            //     SetCoolDown(blackholeCoolDownImage);
            // }
            // if (Input.GetKeyDown(KeyCode.Alpha1) && Inventor.instance.GetSingleEquipment(Equipment.Flask) != null)
            // {
            //     SetCoolDown(flaskCoolDownImage);
            // }
            if (Input.GetKeyDown(KeyCode.C) && SkillManager.instance.clone_Skill.cloneUnlocked)
            {
                SetCoolDown(cloneCoolDownImage);
            }

            CheckCoolDownOf(dashCoolDownImage, SkillManager.instance.dash_Skill.cooldown);
            // CheckCoolDownOf(parryCoolDownImage, parryCoolDown);
            // CheckCoolDownOf(crystalCoolDownImage, crystalCoolDown);
            // CheckCoolDownOf(swordCoolDownImage, swordCoolDown);
            // CheckCoolDownOf(blackholeCoolDownImage, blackholeCoolDown);
            // CheckCoolDownOf(flaskCoolDownImage, Inventor.instance.flaskCoolDown);
            CheckCoolDownOf(cloneCoolDownImage, SkillManager.instance.clone_Skill.cooldown);
        }

        private void UpdataHealthBar()
        {
            slider.maxValue = character_Stat.GetMaxHealth();
            slider.value = character_Stat._currentHP;
        }

        private void SetCoolDown(Image _image)
        {
            if (_image.fillAmount <= 0)
            {
                _image.fillAmount = 1;
            }
        }

        private void CheckCoolDownOf(Image _image, float _coolDown)
        {
            if (_image.fillAmount > 0)
            {
                _image.fillAmount -= 1 / _coolDown * Time.deltaTime;
            }
        }
    }

}
