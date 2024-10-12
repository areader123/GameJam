using System.Collections;
using System.Collections.Generic;
using SK;
using UnityEngine;
namespace SK
{
    public class Character_Stat : Entity_Stat
{
   Character character;
    

    

    
    protected override void Start()
    {
        base.Start();
        character = GetComponent<Character>();
    }

     public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        character.Damage();
    }

    protected override void Die()
    {
        base.Die();
        //死亡状态
       //  character.stateMachine.ChangeState(player.DieState);
        isDead = true;
    }

    protected override void DecreaseHealthOnly(float damage)
    {
        base.DecreaseHealthOnly(damage);
        //当受伤害时 调用防具的效果
        // Item_Equipment armor = Inventor.instance.GetSingleEquipment(Equipment.Armor);
        // if(armor != null)
        // {
        //     armor.Effect(PlayerManger.instance.player.transform);
        // }
    }

    public override void OnEvasion()
    {
        Debug.Log("闪避成功");
        //闪避
        //SkillManger.Instance.dodge_Skill.CreateMirageOnDodge();
    }
}
}

