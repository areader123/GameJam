using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SK
{
    public enum StatType
    {
        strength,
        agility,
        intelligence,
        vitality,
        critChance,
        critPower,
        damage,
        maxHP,
        armor,
        evasion,
        MagicResistance,
        ignite,
        ice,
        lighting
    }
    //本类执行 数值的储存 数值的计算函数 
    //被攻击者调用自身的计算函数 括号内函数参数（Entity_Stat）为攻击者的 
    //计算得到的伤害数值 给到TakeDamage（） 用于扣除自身血量

    //这里是物理和魔法攻击一起计算的
    public class Entity_Stat : MonoBehaviour
    {
        public float _currentHP;

        [Header("Major Stats")]
        public Stat strength;
        public Stat agility;
        public Stat intelligence;
        public Stat vitality;

        [Header("Offensive Stats")]
        public Stat critChance;
        public Stat critPower;
        public Stat damage;

        [Header("Defensive stats")]
        public Stat maxHP;
        public Stat armor;
        public Stat evasion;
        public Stat MagicResistance;

        [Header("Magic Attack")]
        public Stat ignite;
        public Stat ice;
        public Stat lighting;


        [Header("Magic Effect")]


        private float igniteTimer;
        public float igniteDamageCoolDown;
        private float igniteDamageTimer;
        private float iceTimer;
        private float lightingTimer;





        public bool isIgnite;
        public bool isIce;
        public bool isLighting;

        [SerializeField] private float AilmentDuration = 4f;


        private float fireDamage;

        [Header("Character")]

        public bool isDead;
        public bool canGetItem = true;
        private float canGetItemTimer;
        public float canGetItemCoolDown = 1f;






        private Character character => GetComponent<Character>();


        public System.Action OnHealthChange;

        public virtual void DoDamage(Entity_Stat target)
        {
            if (TargetCanAvoidAttack())
            {
                return;
            }

            CaculateAttack(target);
            CaculateMagic(target);

        }

        public virtual void DoMagicDamage(Entity_Stat target)
        {
            if (TargetCanAvoidAttack())
            {
                return;
            }
            CaculateMagic(target);
        }


        public void CaculateMagic(Entity_Stat target)
        {
            float igniteDamage = target.ignite.GetValue();
            float iceDamage = target.ice.GetValue();
            float lightingDamage = target.lighting.GetValue();
            float totalDamage = target.intelligence.GetValue() + igniteDamage + lightingDamage + iceDamage;
            totalDamage -= target.MagicResistance.GetValue() + target.intelligence.GetValue() * 3;
            totalDamage = Mathf.Clamp(totalDamage, 0, int.MaxValue);
            DecreaseHealthOnly(totalDamage);
            if (Mathf.Max(igniteDamage, iceDamage, lightingDamage) == 0)
            {
                return;
            }


            bool canIgnite = igniteDamage > iceDamage && igniteDamage > lightingDamage;
            bool canIce = iceDamage > lightingDamage && iceDamage > igniteDamage;
            bool canLighting = lightingDamage > iceDamage && lightingDamage > igniteDamage;

            while (canIgnite || canIce || canLighting)
            {
                if (Random.value < .3f && igniteDamage > 0)
                {
                    igniteTimer = 4;
                    canIgnite = true;
                    SetFireDamage(target);
                    //AppAliment(canIgnite, canIce, canLighting);
                    Debug.Log("火");
                    return;
                }

                if (Random.value < .5f && iceDamage > 0)
                {
                    iceTimer = 4;
                    canIce = true;
                    //AppAliment(canIgnite, canIce, canLighting);
                    Debug.Log("冰");
                    return;
                }
                if (Random.value < .5f && lightingDamage > 0)
                {
                    lightingTimer = 4;
                    canLighting = true;
                    Debug.Log(canLighting);
                    Debug.Log("雷");
                    // AppAliment(canIgnite, canIce, canLighting);
                    return;
                }
            }





        }



        public void AppAliment(bool _isIgnite, bool _isIce, bool _isLighting)
        {
            if (!_isIce && !_isLighting && !_isIgnite)
            {
                Debug.Log("isIgnite" + _isIgnite);
                Debug.Log("_isIce" + _isIce);
                Debug.Log("_isLighting" + _isLighting);
                Debug.Log("退出");
                return;
            }
            if (_isIgnite)
            {
                isIgnite = _isIgnite;
                isIce = false;
                isLighting = false;
                //fx.CancelInvoke();
                //fx.IgniteFXFor(AilmentDuration);
                Debug.Log("isIgnite" + isIgnite);
            }
            if (_isIce)
            {
                isIce = _isIce;
                isIgnite = false;
                isLighting = false;
                Debug.Log("isIce" + isIce);
                //fx.CancelInvoke();
                //fx.ChillFXFor(AilmentDuration);
            }
            if (_isLighting)
            {
                isLighting = _isLighting;
                isIce = false;
                isIgnite = false;
                // fx.CancelInvoke();
                //  fx.LightingFXFor(AilmentDuration);
                Debug.Log("isLighting" + isLighting);
            }

        }
        private float CheckTargetArmor(Entity_Stat target, float totalDamage)
        {
            if (isIce)
            {
                totalDamage -= target.armor.GetValue() * .8f;
            }
            else
            {
                totalDamage -= target.armor.GetValue();
            }
            totalDamage = Mathf.Clamp(totalDamage, 0, int.MaxValue);
            Debug.Log("totalDamage:" + totalDamage);
            return totalDamage;
        }

        private void CaculateAttack(Entity_Stat target)
        {
            float totalDamage = target.damage.GetValue() + target.strength.GetValue();
            if (CritChance(target))
            {
                Debug.Log("totalDamage:" + totalDamage);
                totalDamage *= (target.critPower.GetValue() + target.strength.GetValue()) * 0.01f;
                totalDamage = (int)totalDamage;
                Debug.Log("暴击" + totalDamage);
            }
            totalDamage = CheckTargetArmor(target, totalDamage);
            TakeDamage(totalDamage);
        }

        protected virtual bool TargetCanAvoidAttack()
        {
            float totalEvasion = agility.GetValue() + evasion.GetValue();
            if (isLighting)
            {
                totalEvasion -= 20;
            }
            else
            {
                totalEvasion = agility.GetValue() + evasion.GetValue();
            }
            Debug.Log(totalEvasion + ",");
            Debug.Log(agility.GetValue() + ",");
            Debug.Log(evasion.GetValue() + ",");
            if (Random.Range(0, 100) < totalEvasion)
            {
                Debug.Log("闪避成功");
                OnEvasion();
                return true;
            }

            return false;
        }

        public virtual void OnEvasion()
        {

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
            igniteDamageCoolDown = .3f;
            //fx = GetComponent<EntityFX>();
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            igniteDamageTimer -= Time.deltaTime;
            igniteTimer -= Time.deltaTime;
            canGetItemTimer -= Time.deltaTime;

            iceTimer -= Time.deltaTime;
            lightingTimer -= Time.deltaTime;




            if (canGetItemTimer > 0)
            {
                canGetItem = false;
            }
            else
            {
                canGetItemTimer = 0;
                canGetItem = true;
            }

            if (igniteTimer < 0)
            {
                isIgnite = false;
            }
            if (igniteDamageTimer < 0 && isIgnite)
            {
                Debug.Log("以下为火焰伤害");
                DecreaseHealthOnly(fireDamage);
                igniteDamageTimer = igniteDamageCoolDown;
            }
            if (iceTimer < 0)
            {
                isIce = false;
            }
            if (lightingTimer < 0)
            {
                isLighting = false;
            }


        }

        private void SetFireDamage(Entity_Stat target) => fireDamage = target.ignite.GetValue() * 0.2f;

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
            if (_statType == StatType.evasion)
                return evasion;
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
            if (_statType == StatType.ignite)
                return ignite;
            if (_statType == StatType.ice)
                return ice;
            if (_statType == StatType.lighting)
                return lighting;

            return null;


        }
    }

}
