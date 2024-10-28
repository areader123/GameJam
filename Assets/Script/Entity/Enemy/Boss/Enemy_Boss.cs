using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SK;
public class Enemy_Boss : Enemy
{
    public Boss_Idel_State boss_Idel_State { get; private set; }
    public Boss_Move_State boss_Move_State { get; private set; }
    public Boss_Battle_State boss_Battle_State { get; private set; }
    public Boss_Attack_one_State boss_Attack_One_State { get; private set; }
    public Boss_Attack_Two_State boss_Attack_Two_State { get; private set; }
    public Boss_Attack_Three_State boss_Attack_Three_State { get; private set; }
    public Boss_Dead_State boss_Dead_State { get; private set; }

    public Enemy_Boss enemy_Boss { get; private set; }


    [Header("Roll Info")]
    [SerializeField] private Transform rollTransform;
    [SerializeField] private float rollRange;

    [Header("Attack_Duration")]
    [SerializeField] public float Attack_One_Animation_Duration;
    [SerializeField] public float Attack_Two_Animation_Duration;
    [SerializeField] public float Attack_Three_Animation_Duration;
    [SerializeField] public float rollSpeed;
    [SerializeField] public float timePerRollDamage;
    private float PerRollDamageTimeCounter;

    protected override void Awake()
    {
        base.Awake();
        boss_Idel_State = new Boss_Idel_State(stateMachine, this, "Idel", this);
        boss_Move_State = new Boss_Move_State(stateMachine, this, "Move", this);
        boss_Battle_State = new Boss_Battle_State(stateMachine, this, "Battle", this);
        boss_Attack_One_State = new Boss_Attack_one_State(stateMachine, this, "Attack_One", this);
        boss_Attack_Two_State = new Boss_Attack_Two_State(stateMachine, this, "Attack_Two", this);
        boss_Attack_Three_State = new Boss_Attack_Three_State(stateMachine, this, "Attack_Three", this);
        boss_Dead_State = new Boss_Dead_State(stateMachine, this, "Dead", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Intialize(boss_Idel_State);
    }

    protected override void Update()
    {
        base.Update();
        PerRollDamageTimeCounter -= Time.deltaTime;
    }
    public override bool CanBeStunned()
    {
        if (base.CanBeStunned())
        {
            //stateMachine.ChangeState(Skeleton_StunnedState);
            return true;
        }
        return false;
    }
    public override void Damage(Skill skill, Entity_Stat entity_Stat)
    {
        if (!enemy_Stat.isDead)
        {
            base.Damage(skill, entity_Stat);
            fx.RedColorBlinkFor(.3f);
        }
    }


    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(boss_Dead_State);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public bool DoSkill_One()
    {

        if (Time.time >= lastTimeSkill_One + skill_One_Cooldown)
        {
            lastTimeSkill_One = Time.time;
            return true;
        }
        return false;
    }
    public bool DoSkill_Two()
    {

        if (Time.time >= lastTimeSkill_Two + skill_Two_Cooldown)
        {
            lastTimeSkill_Two = Time.time;
            return true;
        }
        return false;
    }

    // protected override void OnDrawGizmos()
    // {
    //     base.OnDrawGizmos();
    //     Gizmos.DrawWireSphere(rollTransform.position, rollRange);
    // }

    public void RollDamage()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(rollTransform.position, rollRange);
        if (colliders == null)
            return;


        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Character>() != null)
            {
                if (PerRollDamageTimeCounter < 0)
                {
                    hit.GetComponent<Character_Stat>().DoDamage(enemy_Stat);
                    PerRollDamageTimeCounter = timePerRollDamage;
                }
            }
        }
    }

}
