using System.Collections;
using System.Collections.Generic;
using SK;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UI_Skill_CoolDown_Slot : MonoBehaviour
{
    [SerializeField] private Image coolDown_Image;
    [SerializeField] private Image skill_Image;
    [SerializeField] private Color coolDownColor;
    private float cooldown;
    private UI_Skill_Slot skillSlot;
    private Skill skill;
    private void Awake()
    {
        ClearUp();
    }

    public void SetUpSkillCoolDownSlot(UI_Skill_Slot skillSlot, Skill skill)
    {
        this.skill = skill;
        this.skillSlot = skillSlot;
    }

    private void Update()
    {
        if (skill != null )
        {
            skill_Image.sprite = skillSlot.skillImage.sprite;
            skill_Image.color = skillSlot.skillImage.color;
            coolDown_Image.sprite = skillSlot.skillImage.sprite;
            coolDown_Image.color = coolDownColor;
            if (Input.GetKeyDown(skill.keyCode))
            {
                SetCoolDown();
            }
            CheckCoolDown();
        }
        else
        {

        }
    }

    public void CheckCoolDown()
    {
        if (coolDown_Image.sprite == null && skill != null)
        {
            return;
        }
        if (coolDown_Image.fillAmount > 0)
        {
            //Debug.Log("CoolDown");
            coolDown_Image.fillAmount -= 1 / skill.cooldown * Time.deltaTime;
        }
    }

    public void SetCoolDown()
    {
        if (coolDown_Image.sprite == null)
        {
            return;
        }
        if (coolDown_Image.fillAmount <= 0 && Input.GetKeyDown(skill.keyCode))
        {
            coolDown_Image.fillAmount = 1;
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
}
