using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SK
{
    [System.Serializable]
    public class Stat
    {
        [SerializeField] private int value;

        [SerializeField] private List<int> modifiers;
        public float GetValue()
        {
            int finalValue = value;
            foreach (var obj in modifiers)
            {
                finalValue += obj;
            }
            return finalValue;
        }

        public void SetDefaultValue(int _value)
        {
            value = _value;
        }

        public void AddModifiers(int value)
        {
            modifiers.Add(value);
        }

        public void RemoveModifiers(int value)
        {
            modifiers.Remove(value);
        }
    }
}

