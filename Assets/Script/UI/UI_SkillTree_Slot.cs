using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SK
{

    public class UI_SkillTree_Slot : MonoBehaviour
    {
        [SerializeField] private UI_SkillUsed_Slot[] uI_SkillUsed_Slots;
        [SerializeField] private Queue<UI_SkillUsed_Slot> queue;
        //[SerializeField] private int maxQueueSize;
        private void Awake()
        {
            uI_SkillUsed_Slots = GetComponentsInChildren<UI_SkillUsed_Slot>();
            queue = new Queue<UI_SkillUsed_Slot>();
        }

        public bool AddQueue(UI_SkillUsed_Slot uI_SkillUsed_Slot)
        {
            if (queue.Contains(uI_SkillUsed_Slot))
            {
                return false;
            }
            else
            {
                if (queue.Count < Character_Controller.instance.maxSkillNumber)
                {
                    queue.Enqueue(uI_SkillUsed_Slot);
                }
                else
                {
                    queue.Dequeue().HideSkillUsedSlot();
                    queue.Enqueue(uI_SkillUsed_Slot);
                }
                    return true;
            }
        }

    }

}
