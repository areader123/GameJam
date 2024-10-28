using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D.IK;
using UnityEngine.UI;


namespace SK
{
    public class UI_Stat_Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private UI ui;
        [SerializeField] private string statName;
        [SerializeField] private StatType statType;
        [SerializeField] private TextMeshProUGUI statNameText;
        [SerializeField] private TextMeshProUGUI statValueText;
        [SerializeField]private int amount;
       
        [SerializeField] private Button button;
        [SerializeField] private ColorBlock buttonFailColor;
        [SerializeField] private ColorBlock buttonSuccessColor;

        [TextArea]
        [SerializeField] private string statDescription;
        private void OnValidate()
        {
            gameObject.name = statName;
            statNameText.text = statName;
        }
        void Start()
        {
            UpdateValue();
            ui = GetComponentInParent<UI>();
            
            button.onClick.AddListener(CheckUpgrade);
            CheckButtoncolor();
        }

        private void CheckUpgrade()
        {
            if (Character_Controller.instance.UseSkillPoint())
            {
                UpGradeStat();
                UpdateValue();
                CheckButtoncolor();
            }
        }
        private void CheckButtoncolor()
        {
            if (!Character_Controller.instance.CheckSkillPoint())
            {
                button.colors = buttonFailColor;
            }
            else
            {
                button.colors = buttonSuccessColor;
            }
        }

        private void Update()
        {

        }

        public void UpdateValue()
        {
            Character_Stat player_Stat = Character_Controller.instance.character.GetComponent<Character_Stat>();
            if (player_Stat != null)
            {
                statValueText.text = player_Stat.GetStat(statType).GetValue().ToString();
                if (statType == StatType.maxHP)
                {
                    statValueText.text = player_Stat.GetMaxHealth().ToString();
                }
                if (statType == StatType.damage)
                {
                    statValueText.text = (player_Stat.damage.GetValue() + player_Stat.strength.GetValue()).ToString();
                }
                if (statType == StatType.critPower)
                {
                    statValueText.text = (player_Stat.critPower.GetValue() + player_Stat.strength.GetValue()).ToString();
                }
                if (statType == StatType.critChance)
                {
                    statValueText.text = (player_Stat.critChance.GetValue() + player_Stat.agility.GetValue()).ToString();
                }
                if (statType == StatType.MagicResistance)
                {
                    statValueText.text = (player_Stat.MagicResistance.GetValue() + player_Stat.intelligence.GetValue()).ToString();
                }
                if (statType == StatType.armor)
                {
                    statValueText.text = player_Stat.armor.GetValue().ToString();
                }
                if (statType == StatType.intelligence)
                {
                    statValueText.text = player_Stat.intelligence.GetValue().ToString();
                }
                if(statType == StatType.Blood)
                {
                    statValueText.text = player_Stat.blood.GetValue().ToString();
                }
                if(statType == StatType.strength)
                {
                    statValueText.text = player_Stat.strength.GetValue().ToString();
                }

            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ui.uI_Stat_Tool_Tip.ShowStatToolTip(statDescription);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ui.uI_Stat_Tool_Tip.HideToolTip();
        }

        private void UpGradeStat()
        {
            Character_Stat player_Stat = Character_Controller.instance.character.GetComponent<Character_Stat>();
            if (statType == StatType.maxHP)
            {
                player_Stat.GetStat(statType).AddModifiers(amount);
                Character_Controller.instance.character.GetComponent<Character_Stat>().IncreaseHealthOnly(amount);
            }
            if (statType == StatType.critPower)
            {
                player_Stat.GetStat(statType).AddModifiers(amount);
            }
            if (statType == StatType.critChance)
            {
                player_Stat.GetStat(statType).AddModifiers(amount);
            }
            if (statType == StatType.armor)
            {
                player_Stat.GetStat(statType).AddModifiers(amount);
            }
            if (statType == StatType.intelligence)
            {
                player_Stat.GetStat(statType).AddModifiers(amount);
            }
            if(statType == StatType.damage)
            {
                player_Stat.GetStat(statType).AddModifiers(amount);
            }
            if(statType == StatType.strength)
            {
                player_Stat.GetStat(statType).AddModifiers(amount);
            }
        }

    }
}
