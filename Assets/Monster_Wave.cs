using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
[CreateAssetMenu(fileName = "Monster_Wave", menuName = "GameJam/Monster_Wave", order = 0)]
public class Monster_Wave : ScriptableObject
{
    public List<GameObject> monsters;
}

