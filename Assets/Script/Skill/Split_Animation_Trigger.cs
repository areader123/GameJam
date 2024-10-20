using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Split_Animation_Trigger : MonoBehaviour
{
    private void  Skill_Multi_Skill () {
        this.GetComponent<Enemy_MultiTransmit_Skill>().CanUseSkill();
    }
}
