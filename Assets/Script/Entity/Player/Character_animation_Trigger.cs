using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SK
{

    public class Character_animation_Trigger : MonoBehaviour
    {
        Character character;
        Character_Stat character_Stat;

        private void Awake()
        {
            character = GetComponent<Character>();
            character_Stat = GetComponent<Character_Stat>();
        }
        private void AnimationFinishTrigger()
        {
            character.AnimationTrigger();
        }

        private void AttackTirgger()
        {
            Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(character.attackableTransform.position, character.attackRadius);
            foreach (var hit in collider2Ds)
            {
                if (hit.GetComponent<Enemy_Stat>() != null)
                {
                    hit.GetComponent<Enemy_Stat>().DoDamage(character_Stat);
                }
            }
        }

        private void DamageBullet()
        {
            if (SkillManager.instance.attack_SpeedUp_Skill.skilling_Four)
            {
                Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(character.destroyTransform.position, character.destroyRadius);
                Debug.Log("尝试DamageBullet");
                foreach (var hit in collider2Ds)
                {
                    if (hit.tag == "Bullet")
                    {
                        Debug.Log("Destroy(hit.gameObject)");
                        Destroy(hit.gameObject);
                    }
                }
            }
        }

    }
}
