using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SK
{
    public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    public Dash_Skill dash_Skill{get;private set;}
    public Clone_Skill clone_Skill{get;private set;}

    private void Awake() 
    {
        if(instance != null) 
        {
           Destroy(instance);
        }
        else 
        {
            instance = this;
        }
    }

    private void Start() {
        dash_Skill = GetComponent<Dash_Skill>();
        clone_Skill = GetComponent<Clone_Skill>();
    }
}
}

