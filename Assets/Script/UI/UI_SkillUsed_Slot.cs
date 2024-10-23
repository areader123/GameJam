using System.Collections;
using System.Collections.Generic;
using BayatGames.SaveGameFree;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace SK
{

    public class UI_SkillUsed_Slot : MonoBehaviour, ISaveManager
    {
        [SerializeField] public UI_Skill_Slot uI_Skill_Slot;
        [SerializeField] private UI_SkillTree_Slot uI_SkillTree_Slot;
        private Image image;
        public bool Unlock;//记录
        public Skill skill;
        public SkillName skillName;
          
        private void Awake()
        {
            uI_Skill_Slot = GetComponentInChildren<UI_Skill_Slot>();
            uI_SkillTree_Slot = GetComponentInParent<UI_SkillTree_Slot>();
            image = GetComponent<Image>();
            image.color = new Vector4(1, 1, 1, 0);
          
         
            
        }

        void OnApplicationQuit()
        {
          //  SaveGame.Save<bool>(thisName, Unlock);
        }
       
        private void Update()
        {
            if (skill == null)
            {
                skill = SkillManager.instance.GetSkillByName(skillName);
            }
        }
        void Start()
        {
            skill = SkillManager.instance.GetSkillByName(skillName);
            if(Unlock)
            {
                ShowSkillUsedSlot();
            }
        }

        public void ShowSkillUsedSlot()
        {
            image.color = new Vector4(1, 1, 1, 1);
            Unlock = true;
            uI_SkillTree_Slot.AddQueue(this);

        }

        public void HideSkillUsedSlot()
        {
            image.color = new Vector4(1, 1, 1, 0);
            Unlock = false;
        }

        public void LoadData(GameData _data)
        {
            if(SaveGame.Exists(skillName.ToString()))
            {
                Unlock = SaveGame.Load<bool>(skillName.ToString());
            }
        }

        public void SaveData(ref GameData _data)
        {
             SaveGame.Save<bool>(skillName.ToString(), Unlock);
        }
    }

}