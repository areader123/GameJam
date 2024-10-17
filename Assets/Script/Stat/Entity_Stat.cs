using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SK
{
    public enum StatType
    {
        strength,
        intelligence,
        vitality,
        critChance,
        critPower,
        damage,
        maxHP,
        armor,
        MagicResistance,
        agility
    }
    //本类执行 数值的储存 数值的计算函数 
    //被攻击者调用自身的计算函数 括号内函数参数（Entity_Stat）为攻击者的 
    //计算得到的伤害数值 给到TakeDamage（） 用于扣除自身血量

    //这里是物理和魔法攻击一起计算的
    public enum CanHitBack
    {
        can, canNot
    }
    public class Entity_Stat : MonoBehaviour
    {
        public float _currentHP;

        [Header("Major Stats")]
        public Stat strength;
        public Stat intelligence;
        public Stat vitality;
        public Stat agility;
        [Header("Offensive Stats")]
        public Stat critChance;
        public Stat critPower;
        public Stat damage;

        [Header("Defensive stats")]
        public Stat maxHP;
        public Stat armor;
        public Stat MagicResistance;

        [Header("Character")]

        public bool isDead;
        public bool canGetItem = true;
        private float canGetItemTimer;
        public float canGetItemCoolDown = 1f;
        public CanHitBack canHitBack;






        private Character character => GetComponent<Character>();


        public System.Action OnHealthChange;

        public virtual void DoDamage(Entity_Stat target)
        {
            CaculateAttack(target);
            CalculateMagic(target);
        }






        private float CheckTargetArmor(Entity_Stat target, float totalDamage)
        {

            totalDamage = totalDamage / (totalDamage + target.armor.GetValue()) * totalDamage;
            //totalDamage -= target.armor.GetValue();

            totalDamage = Mathf.Clamp(totalDamage, 0, int.MaxValue);
            return totalDamage;
        }

        private float CheckTargetMagicResistance(Entity_Stat target, float totalDamage)
        {
            totalDamage = totalDamage / (totalDamage + target.MagicResistance.GetValue()) * totalDamage;
            totalDamage = Mathf.Clamp(totalDamage, 0, int.MaxValue);
            return totalDamage;
        }

        private void CalculateMagic(Entity_Stat target)
        {
            float totalDamage = target.damage.GetValue() + target.intelligence.GetValue();
            totalDamage = CheckTargetMagicResistance(target, totalDamage);
        }

        private void CaculateAttack(Entity_Stat target)
        {
            float totalDamage = target.damage.GetValue() + target.strength.GetValue();
            // if (CritChance(target))
            // {
            //     Debug.Log("totalDamage:" + totalDamage);
            //     totalDamage *= (target.critPower.GetValue() + target.strength.GetValue()) * 0.01f;
            //     totalDamage = (int)totalDamage;
            //     Debug.Log("暴击" + totalDamage);
            // }
            totalDamage = CheckTargetArmor(target, totalDamage);
            DecreaseHealthOnly(totalDamage);
        }




        public bool CritChance(Entity_Stat target)
        {
            float totalCritChance = target.critChance.GetValue() + target.agility.GetValue();
            if (Random.Range(0, 100) < totalCritChance)
            {
                return true;
            }
            return false;
        }

        public virtual void TakeDamage(float damage)
        {
            DecreaseHealthOnly(damage);
            Debug.Log("受到" + damage + "伤害");
        }

        public virtual void IncreaseHealthOnly(int _amount)
        {
            _currentHP += _amount;

            if (_currentHP > GetMaxHealth())
            {
                _currentHP = GetMaxHealth();
            }
            if (OnHealthChange != null)
            {
                OnHealthChange();
            }

        }


        protected virtual void DecreaseHealthOnly(float damage)
        {
            _currentHP -= damage;
            if (OnHealthChange != null)
            {
                OnHealthChange();
            }
            if (_currentHP <= 0)
            {
                Die();
                Debug.Log("死亡");
            }
        }

        protected virtual void Start()
        {
            _currentHP = maxHP.GetValue();
            critPower.SetDefaultValue(150);
        }

        // Update is called once per frame
        protected virtual void Update()
        {


        }


        protected virtual void Die()
        {

        }


        public float GetMaxHealth()
        {
            return maxHP.GetValue() + vitality.GetValue() * 5;
        }

        public void SetUpGetItemTimer()
        {
            canGetItemTimer = canGetItemCoolDown;
        }

        private IEnumerator StatModCoroutine(int _amount, float _duration, Stat _stat)
        {
            _stat.AddModifiers(_amount);
            yield return new WaitForSeconds(_duration);
            _stat.RemoveModifiers(_amount);
        }

        public virtual void IncreaseStatBy(int _amount, float _duration, Stat _stat)
        {
            StartCoroutine(StatModCoroutine(_amount, _duration, _stat));
        }

        public Stat GetStat(StatType _statType)
        {
            if (_statType == StatType.strength)
                return strength;
            if (_statType == StatType.damage)
                return damage;
            if (_statType == StatType.agility)
                return agility;
            if (_statType == StatType.armor)
                return armor;
            if (_statType == StatType.critChance)
                return critChance;
            if (_statType == StatType.critPower)
                return critPower;
            if (_statType == StatType.intelligence)
                return intelligence;
            if (_statType == StatType.MagicResistance)
                return MagicResistance;
            if (_statType == StatType.maxHP)
                return maxHP;
            if (_statType == StatType.vitality)
                return vitality;
            return null;


        }
    }

}
