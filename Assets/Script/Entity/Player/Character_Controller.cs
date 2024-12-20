using System;
using System.Collections;
using System.Collections.Generic;
using BayatGames.SaveGameFree;
using SK;
using Unity.Mathematics;
using UnityEngine;

namespace SK
{

    public class Character_Controller : MonoBehaviour,ISaveManager
    {
        public System.Action UpdateBar;
        public static Character_Controller instance;

        public Character character;
        //此处为光亮值
        [Header("Current Value")]
        [SerializeField] private int lightingNumber;//记录
        [SerializeField] private int exp = 1;//记录
        [SerializeField] private int level;//记录
        private int pointToSkill;
        private int pointToSkillUsed;
        public int pointCanbeUsed;//记录
        [Header("Max Value")]

        [SerializeField] private int maxLightingNumber;
       // [SerializeField] private int maxExpNumber_First;
        [SerializeField] private int perLevelMaxExpNumberAdded;
        [SerializeField]private int perLightHpAdded;
        [SerializeField] private int perLevelAddSkillPoint;
        public int maxSkillNumber;

        [Header("Kill Info")]
        public bool ifEnemyKilled;
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
        void OnApplicationQuit()
        {
           
        }
        public int GetLightingNumber() => lightingNumber;
        public int GetExp() => exp;
        public int GetLevel() => level;
        public int GetMaxExp() => level * level * level +  perLevelMaxExpNumberAdded * level;
        public int GetMaxLightingNumber() => maxLightingNumber;

        public bool canChangeLightScale() => canChangeLightScaleFlag;

        private void Start() 
        {
            UpdateBar();    
        }

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
        public bool UseSkillCostLighting(int _delet)
        {
            if (lightingNumber - _delet >= 0)
            {
                lightingNumber -= _delet;
                UpdateBar();
                return true;
            }
            else
            {
                return false;
            }
        }

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
            if (pointCanbeUsed >=  _price)
            {
              //  pointToSkillUsed += _price;
                pointCanbeUsed -= _price;
                return true;
            }
            return false;
        }

        public bool AddLightingNumber()
        {
            if (lightingNumber < maxLightingNumber)
            {
                lightingNumber += 1;
                
                UpdateBar();
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
                UpdateBar();
                return true;
            }
            else
            {
                lightingNumber = 0;
                UpdateBar();
                canChangeLightScaleFlag = false;
                return false;
            }
        }

        private void AddExp()
        {
            if (exp < GetMaxExp())
            {
                exp += 1;
                UpdateBar();
                if (exp == GetMaxExp())
                {
                    exp = 0;
                    level += 1;
                    UpdateBar();
                    //pointToSkill = level / perLevelAddSkillPoint;
                    pointCanbeUsed += perLevelAddSkillPoint;
                }
            }
        }

        public void UpdateLightingNumberPerSecond()
        {
            if (lightingNumber > 0)
            {
                lightingNumber -= (int)(1 / Time.deltaTime);
                UpdateBar();
            }
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

        public void LoadData(GameData _data)
        {
             if(SaveGame.Exists("lightingNumber"))
                {
                    lightingNumber = SaveGame.Load<int>("lightingNumber");
                }
                if(SaveGame.Exists("exp"))
                {
                    exp = SaveGame.Load<int>("exp");
                }
                if(SaveGame.Exists("level"))
                {
                    level = SaveGame.Load<int>("level");
                }
                if(SaveGame.Exists("pointCanbeUsed"))
                {
                    pointCanbeUsed = SaveGame.Load<int>("pointCanbeUsed");
                }
        }

        public void SaveData(ref GameData _data)
        {
             SaveGame.Save<int>("lightingNumber", lightingNumber);
            SaveGame.Save<int>("exp", exp);
            SaveGame.Save<int>("level", level);
            SaveGame.Save<int>("pointCanbeUsed", pointCanbeUsed);
        }

        public void AddHealth()
        {
            Character_Controller.instance.character.GetComponent<Character_Stat>().IncreaseHealthOnly(perLightHpAdded);
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
