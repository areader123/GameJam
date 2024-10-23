using System.Collections;
using System.Collections.Generic;
using BayatGames.SaveGameFree;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SK
{

    public class UI_Skill_Slot : MonoBehaviour,
     ISaveManager,
     IPointerEnterHandler,
      IPointerExitHandler
    {
        [SerializeField] private string skillName;
        [TextArea]
        [SerializeField] private string skillDescription;

        [SerializeField] private Color skillLockedColor;
        [SerializeField] private int skillPrice;
        public bool unLock;//记录
        [SerializeField] private UI_Skill_Slot[] shouldBeLock;
        [SerializeField] private UI_Skill_Slot[] shouldBeUnlock;

        [SerializeField] public Image skillImage;

        private UI_SkillUsed_Slot uI_SkillUsed_Slot;


        private UI ui => GetComponentInParent<UI>();

        private void Awake()
        {
            
            Button button = GetComponent<Button>();
            button.onClick.AddListener(() => UnlockSkillSlot());
            skillImage = GetComponent<Image>();
        }

        void OnApplicationQuit()
        {
            
        }

        private void Start()
        {
            skillImage.color = skillLockedColor;

            if (unLock)
            {
                skillImage.color = Color.white;
            }
            if (GetComponentInParent<UI_SkillUsed_Slot>() != null)
            {
                uI_SkillUsed_Slot = GetComponentInParent<UI_SkillUsed_Slot>();
            }

        }
        private void OnValidate()
        {
            gameObject.name = "SkillTree_Name:" + skillName;
        }

        public void UnlockSkillSlot()
        {
            SwitchSkillUsedSlot();
            for (int i = 0; i < shouldBeLock.Length; i++)
            {
                if (shouldBeLock[i].unLock == true)
                {
                    // Debug.Log("有技能已经解锁了");
                    return;
                }
            }

            for (int i = 0; i < shouldBeUnlock.Length; i++)
            {
                if (shouldBeUnlock[i].unLock == false)
                {
                    //Debug.Log("有技能未解锁");
                    return;
                }
            }

            if (Character_Controller.instance.HaveEnoughSkillPoint(skillPrice,unLock) == false)
            {
                return;
            }
            unLock = true;
            skillImage.color = Color.white;
        }

        private void SwitchSkillUsedSlot()
        {
            if (unLock)
            {
                if (uI_SkillUsed_Slot != null)
                {
                    uI_SkillUsed_Slot.ShowSkillUsedSlot();
                }
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {

            Vector2 mousePosition = Input.mousePosition;
            // Debug.Log(mousePosition);

            float xOffset = 0;
            float yOffset = 0;

            if (mousePosition.x > 800)
            {
                xOffset = -150;
            }
            else
            {
                xOffset = 150;
            }
            if (mousePosition.y > 500)
            {
                yOffset = -150;
            }
            else
            {
                yOffset = 150;
            }

            ui.uI_Stat_Tool_Tip.transform.position = new Vector2(mousePosition.x + xOffset, mousePosition.y + yOffset);
            ui.ui_Skill_Tip.ShowSkillTip(skillDescription, skillName);

        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ui.ui_Skill_Tip.HideSkillTip();
        }

        public void LoadData(GameData _data)
        {
            if(SaveGame.Exists(skillName))
            {
                unLock = SaveGame.Load<bool>(skillName);
            }
        }

        public void SaveData(ref GameData _data)
        {
            SaveGame.Save<bool>(skillName,unLock);
        }

        // public void LoadData(GameData _data)
        // {
        //     Debug.Log("加载skill");
        //     if (_data.skillTree.TryGetValue(skillName, out bool value))
        //     {
        //         unLock = value;
        //     }
        // }

        // public void SaveData(ref GameData _data)
        // {
        //     if (_data.skillTree.TryGetValue(skillName, out bool value))
        //     {
        //         Debug.Log("保存skill");
        //         _data.skillTree.Remove(skillName);
        //         _data.skillTree.Add(skillName, unLock);
        //     }
        //     else
        //     {
        //         Debug.Log("保存skill");
        //         _data.skillTree.Add(skillName, unLock);
        //     }
        // }
    }
}
