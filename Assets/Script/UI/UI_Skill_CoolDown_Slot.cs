using System;
using System.Collections;
using System.Collections.Generic;
using BayatGames.SaveGameFree;
using SK;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UI_Skill_CoolDown_Slot : MonoBehaviour, ISaveManager
{
    [SerializeField] private string coolDownSlotName;
    [SerializeField] private Image coolDown_Image;
    [SerializeField] private Image skill_Image;
    [SerializeField] private Color coolDownColor;

    private float cooldownTimeCounter;
    private UI_Skill_Slot skillSlot;
    private Skill skill;

    [SerializeField] private KeyCode keyCodeCoolDown;
    private string skillName;
    [SerializeField] private string skill_CoolDown_Slot_Name;
    UI uI;

    private void OnValidate()
    {
        ClearUp();
    }
    private void Awake()
    {
        //ClearUp();
    }
    private void Start()
    {
        uI = GetComponentInParent<UI>();
        if (skillName != null)
        {
            this.skill = SkillManager.instance.GetSkillByStringName(skillName);
            Debug.Log(skill.skillName.ToString());
            this.skillSlot = uI.GetSkillUsedSlotByName(skillName).uI_Skill_Slot;
        }

    }
    void OnApplicationQuit()
    {

    }

    public void SetUpSkillCoolDownSlot(UI_Skill_Slot skillSlot, Skill skill)
    {
        // if(skill == null)
        // return;
        if (skill != null && skillSlot != null)
        {
            this.skill = skill;
            this.skillSlot = skillSlot;
        }
    }

    private void Update()
    {

        if (skill != null)
        {
            skillName = skill.skillName.ToString();
            skill_Image.sprite = skillSlot.skillImage.sprite;
            skill_Image.color = skillSlot.skillImage.color;
            coolDown_Image.sprite = skillSlot.skillImage.sprite;
            coolDown_Image.color = coolDownColor;
            skill.keyCode = keyCodeCoolDown;
        }
    }

    public void CheckCoolDown()
    {
        if (coolDown_Image.sprite == null || skill == null)
        {
            return;
        }
        if (coolDown_Image.fillAmount > 0)
        {
            //  cooldownTimeCounter -= 1 / cooldown * Time.deltaTime;
            //  coolDown_Image.fillAmount = cooldownTimeCounter;
            coolDown_Image.fillAmount -= 1 / skill.cooldown * Time.deltaTime;
        }
    }

    public void SetCoolDown()
    {
        if (coolDown_Image == null || skill == null)
        {
            return;
        }
        if (coolDown_Image.fillAmount <= 0 && Input.GetKeyDown(keyCodeCoolDown) && skill.cooldowmTImer > 0.1f)
        {
            coolDown_Image.fillAmount = 1;
            cooldownTimeCounter = 1;
        }
    }

    private void ClearUp()
    {
        skill = null;
        skillSlot = null;
        coolDown_Image.sprite = null;
        coolDown_Image.color = Color.clear;
        skill_Image.sprite = null;
        skill_Image.color = Color.clear;
    }

    public void LoadData(GameData _data)
    {
        if (SaveGame.Exists(skill_CoolDown_Slot_Name))
        {
            skillName = SaveGame.Load<string>(skill_CoolDown_Slot_Name);

        }
    }

    public void SaveData(ref GameData _data)
    {
        SaveGame.Save<string>(skill_CoolDown_Slot_Name, skillName);
    }
}
