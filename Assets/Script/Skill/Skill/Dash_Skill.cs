using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


namespace SK
{
    public class Dash_Skill : Skill
    {

        public bool dashUnlocked;
        [SerializeField] private int dashUseWithLightingAmountUsed;
        private bool basic_1;
        [SerializeField] public UI_Skill_Slot dash_UI_Skill_Slot;
        public bool dashUseWithOutLightingLocked;
        private bool basic_2;
        [SerializeField] public UI_Skill_Slot dashUseWithOutLighting;
        public bool dashWithCloneLocked;
        [SerializeField] public UI_Skill_Slot dashWithClone;
        public bool dashWithObjectToDamageLocked;

        [SerializeField] private GameObject prefab;
        [SerializeField] private int amount;
        [SerializeField] private int angleRange;
        [SerializeField] private int deltaAngle;
        [SerializeField] private float transmitDamage;

        [SerializeField] private float transmitExistTime;


        [SerializeField] private float transmitSpeed;
        [SerializeField] private bool destroySelfAfterDamage;
        [SerializeField] private float damagepPerTime;

        [SerializeField] public UI_Skill_Slot dashWithObjectToDamage;
        public bool unHittableDuringDashLocked;
        [SerializeField] private float unhittableTime;
        [SerializeField] public UI_Skill_Slot unHittableDuringDash;
        public bool lowCoolingDownWhileHighLighting;
        [SerializeField] private int lowCoolingDownWhileHighLightingBoundary;
        [SerializeField] private float lowCoolingDown;
        private float orignalCoolDown;
        [SerializeField] public UI_Skill_Slot lowCoolingDownButHighLighting;

        public bool dashSkillUsedUnlocked;
        [SerializeField] private UI_SkillUsed_Slot uI_SkillUsed_Slot;

        public float dashDuration;
        public float dashSpeed;
        private Character character;
        private void Awake()
        {
        }
        protected override void Start()
        {
            base.Start();
            character = Character_Controller.instance.character;
            dash_UI_Skill_Slot.GetComponent<Button>().onClick.AddListener(UnlockDash);
            dashUseWithOutLighting.GetComponent<Button>().onClick.AddListener(UnlockDashUseWithOutLighting);
            dashWithClone.GetComponent<Button>().onClick.AddListener(UnlockdashWithClone);
            dashWithObjectToDamage.GetComponent<Button>().onClick.AddListener(UnlockDashWithObjectToDamage);
            unHittableDuringDash.GetComponent<Button>().onClick.AddListener(UnlockUnHittableDuringDash);
            lowCoolingDownButHighLighting.GetComponent<Button>().onClick.AddListener(UnlockLowCoolingDownWhileHighLighting);

        }

        protected override void Update()
        {
            base.Update();
        }

        public override void UseSkill()
        {
            base.UseSkill();
            if (dashUnlocked && uI_SkillUsed_Slot.Unlock)
            {
                Debug.Log("dashSkillUsedUnlocked" + dashSkillUsedUnlocked);
                dashSkillUsedUnlocked = uI_SkillUsed_Slot.Unlock;
                if (dashUseWithOutLightingLocked)
                {
                    character.stateMachine.ChangeState(character.player_Dash_State);
                    if (dashWithCloneLocked)
                    {
                        Debug.Log("创造克隆体");
                        SkillManager.instance.clone_Skill.UseByOutside();
                        //创造克隆体
                        if (dashWithObjectToDamageLocked)
                        {
                            for (int i = 0; i < amount; i++)
                            {
                                Debug.Log("创造弹幕");
                                GameObject gameObject = Instantiate(prefab, Character_Controller.instance.character.transform.position, Quaternion.AngleAxis(angleRange / amount * i, Vector3.forward));
                                Dash_Skill_Controller dash_Skill_Controller = gameObject.GetComponent<Dash_Skill_Controller>();
                                dash_Skill_Controller.SetArrow(transmitDamage, transmitExistTime, transmitSpeed, damagepPerTime, destroySelfAfterDamage, this);
                            }
                            //创造弹幕  
                            if (unHittableDuringDashLocked)
                            {
                                //冲刺免疫
                                //协程写
                                StopAllCoroutines();
                                StartCoroutine("UnhittableWhileDash");
                                if (lowCoolingDownWhileHighLighting)
                                {
                                    //低光照降低冷却
                                    if (Character_Controller.instance.GetLightingNumber() >= lowCoolingDownWhileHighLightingBoundary)
                                    {
                                        orignalCoolDown = cooldown;
                                        cooldown = lowCoolingDown;
                                    }
                                    else
                                    {
                                        cooldown = orignalCoolDown;
                                    }
                                }
                            }

                        }
                    }
                    return;
                }

                //消耗光亮值 否则无法释放
                if (Character_Controller.instance.DecreaseLightingNumber(dashUseWithLightingAmountUsed))
                {
                    character.stateMachine.ChangeState(character.player_Dash_State);
                }
            }
            // character.stateMachine.ChangeState();
        }

        public IEnumerator UnhittableWhileDash()
        {
            character.capsuleCollider2D.enabled = false;
            Debug.Log("免疫");
            yield return new WaitForSeconds(unhittableTime);
            character.capsuleCollider2D.enabled = true;
        }


        protected override void CheckUnlock()
        {
            UnlockDash();
            UnlockLowCoolingDownWhileHighLighting();
            UnlockUnHittableDuringDash();
            UnlockDashWithObjectToDamage();
            UnlockdashWithClone();
            UnlockDashUseWithOutLighting();
        }
        public void UnlockLowCoolingDownWhileHighLighting()
        {
            Debug.Log("尝试");
            if (lowCoolingDownButHighLighting.unLock)
            {
                Debug.Log("成功");
                lowCoolingDownWhileHighLighting = true;
            }
        }
        public void UnlockUnHittableDuringDash()
        {
            Debug.Log("尝试");
            if (unHittableDuringDash.unLock)
            {
                Debug.Log("成功");
                unHittableDuringDashLocked = true;
            }
        }

        public void UnlockDashWithObjectToDamage()
        {
            Debug.Log("尝试");
            if (dashWithObjectToDamage.unLock)
            {
                Debug.Log("成功");
                dashWithObjectToDamageLocked = true;
            }
        }

        public void UnlockdashWithClone()
        {
            Debug.Log("尝试");
            if (dashWithClone.unLock)
            {
                Debug.Log("成功");
                dashWithCloneLocked = true;
            }
        }

        public void UnlockDashUseWithOutLighting()
        {
            Debug.Log("尝试");
            if (dashUseWithOutLighting.unLock)
            {

                Debug.Log("成功");
                dashUseWithOutLightingLocked = true;
                if (!basic_2)
                {
                    basic_2 = true;
                    cost =0;
                }
            }
        }


        public void UnlockDash()
        {
            Debug.Log("尝试");
            if (dash_UI_Skill_Slot.unLock)
            {
                Debug.Log("成功");
                dashUnlocked = true;
                if (!basic_1)
                {
                    cost = dashUseWithLightingAmountUsed;
                    basic_1 = true;
                }
            }
        }
    }
}

