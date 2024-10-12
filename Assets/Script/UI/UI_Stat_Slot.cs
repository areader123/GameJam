using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;


namespace SK
{

    public class UI_Stat_Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private UI ui;
        [SerializeField] private string statName;
        [SerializeField] private StatType statType;
        [SerializeField] private TextMeshProUGUI statNameText;
        [SerializeField] private TextMeshProUGUI statValueText;

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
                if (statType == StatType.evasion)
                {
                    statValueText.text = (player_Stat.evasion.GetValue() + player_Stat.agility.GetValue()).ToString();
                }
                if (statType == StatType.MagicResistance)
                {
                    statValueText.text = (player_Stat.MagicResistance.GetValue() + player_Stat.intelligence.GetValue()).ToString();
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
    }
}
