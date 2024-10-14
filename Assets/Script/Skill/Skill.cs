using System.Collections;
using System.Collections.Generic;
using SK;
using UnityEngine;

namespace SK
{

    public class Skill : MonoBehaviour
    {

        public float cooldown;
        protected float cooldowmTImer;
       
        // Start is called before the first frame update
        protected virtual void Start()
        {
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            cooldowmTImer -= Time.deltaTime;
        }


        public virtual bool CanUseSkill()
        {
            if (cooldowmTImer < 0)
            {
                UseSkill();
                cooldowmTImer = cooldown;
                return true;
            }
            return false;

        }

        public virtual void UseSkill()
        {

        }

        protected virtual void CheckUnlock()
        {

        }
    }
}
