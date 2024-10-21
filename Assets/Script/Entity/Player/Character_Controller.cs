using System.Collections;
using System.Collections.Generic;
using SK;
using Unity.Mathematics;
using UnityEngine;

namespace SK
{

    public class Character_Controller : MonoBehaviour
    {
        public static Character_Controller instance;

        public Character character;
        //此处为光亮值
        [Header("Current Value")]
        [SerializeField] [Range(0,Mathf.Infinity)]private int lightingNumber;
        [SerializeField] private int exp;
        [SerializeField] private int level;
        private int pointToSkill;
        private int pointToSkillUsed;
        public int pointCanbeUsed;
        [Header("Max Value")]

        [SerializeField] private int maxLightingNumber;
        [SerializeField] private int maxExpNumber_First;
        [SerializeField] private int perLevelMaxExpNumberAdded;
        [SerializeField] private int perLevelAddSkillPoint;
        public int maxSkillNumber;


        private bool canChangeLightScaleFlag = true;



        private void Awake()
        {
            if (instance != null)
            {
                Destroy(instance.gameObject);
            }
            else
            {
                instance = this;
            }
        }
        public int GetLightingNumber() => lightingNumber;
        public int GetExp() => exp;
        public int GetLevel() => level;
        public int GetMaxExp() => maxExpNumber_First + perLevelMaxExpNumberAdded * level;
        public int GetMaxLightingNumber() => maxLightingNumber;

        public bool canChangeLightScale() => canChangeLightScaleFlag;

        public bool HaveEnoughSkillPoint(int _price,bool unLock)
        {
            if(unLock)
            {
                return false;
            }
            return UseSkillPoint(_price);
        }

        // public void GiveBackSkillPoint()
        // {
        //     //退还所有技能点
        //     pointCanbeUsed = pointToSkill;
        //     pointToSkillUsed = 0;
        //     //取消所有的技能

        // }
        public bool CheckSkillPoint () 
        {
            if (pointToSkill > pointToSkillUsed)
            {
                return true;
            }
            return false;
        }
        public bool UseSkillPoint(int _price = 1)
        {
            if (pointToSkill >= pointToSkillUsed + _price)
            {
                pointToSkillUsed += _price;
                pointCanbeUsed = pointToSkill - pointToSkillUsed;
                return true;
            }
            return false;
        }

        public bool AddLightingNumber()
        {
            if (lightingNumber < maxLightingNumber)
            {
                lightingNumber += 1;
                if (lightingNumber == maxLightingNumber)
                {
                    canChangeLightScaleFlag = true;
                }
                return false;
            }
            else
            {
                AddExp();
                return true;
            }

        }

        public bool DecreaseLightingNumber(int _delet)
        {
            if (lightingNumber - _delet > 0)
            {
                lightingNumber -= _delet;
                return true;
            }
            else
            {
                lightingNumber = 0;
                canChangeLightScaleFlag = false;
                return false;
            }
        }

        private void AddExp()
        {
            if (exp < GetMaxExp())
            {
                exp += 1;
                if (exp == GetMaxExp())
                {
                    exp = 0;
                    level += 1;

                    pointToSkill = level / perLevelAddSkillPoint;
                    pointCanbeUsed = pointToSkill;
                }
            }
        }

        public void UpdateLightingNumberPerSecond()
        {
            if (lightingNumber > 0)
                lightingNumber -= (int)(1 / Time.deltaTime);
        }

        public bool CheckIfLightingNumberFilled()
        {
            if (GetLightingNumber() == maxLightingNumber)
            {
                return true;
            }
            else
            {
                return false;
            }
        }




        private void Update()
        {

        }


        //数据保存和加载
        // public void LoadData(GameData _data)
        // {
        //     this.currency = _data.currency;
        // }

        // public void SaveData(ref GameData _data)
        // {
        //     _data.currency = this.currency;
        // }
    }
}
