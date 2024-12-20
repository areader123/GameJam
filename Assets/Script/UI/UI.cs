using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
namespace SK
{

    public class UI : MonoBehaviour
    {
        [SerializeField] public GameObject character;
        [SerializeField] public GameObject skillTree;
        [SerializeField] private GameObject esc;

        [SerializeField] public GameObject game;
        [SerializeField] public GameObject dead;



        public UI_Stat_Tool_Tip uI_Stat_Tool_Tip;
        public UI_Skill_Tool ui_Skill_Tip;

        public UI_SkillUsed_Slot[] uI_SkillUsed_Slots;
        public bool ifTimeStop;
        // Start is called before the first frame update
        void Start()
        {
            SwithTo(game);
            Character_Controller.instance.character.animator.speed = 1;
            Time.timeScale = 1;
        }

        // Update is called once per frame
        void Update()
        {

            // if (Input.GetKeyDown(KeyCode.B))
            // {
            //     SwithWithKeyTo(craft);
            // }

            // if (Input.GetKeyDown(KeyCode.O))
            // {
            //     SwithWithKeyTo(setting);
            // }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SwithWithKeyTo(esc);
            }
        }
        public void SwithTo(GameObject _menu)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

            if (_menu != null)
            {
                _menu.SetActive(true);
            }
        }
        public void SwithWithKeyTo(GameObject _menu)
        {
            if (_menu != null && _menu.activeSelf)
            {
                _menu.SetActive(false);
                CheckForGameUI();
                return;
            }
            if (_menu == esc)
            {
                Character_Controller.instance.character.animator.speed = 0;
                Time.timeScale = 0;
            }
            ifTimeStop = true;
            SwithTo(_menu);
        }

        public void CheckForGameUI()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).gameObject.activeSelf)
                    return;
            }
            Character_Controller.instance.character.animator.speed = 1;
            Time.timeScale = 1;
            ifTimeStop = false;
            SwithTo(game);
        }

        public UI_SkillUsed_Slot GetSkillUsedSlotByName(string _skillName)
        {
            foreach (UI_SkillUsed_Slot obj in uI_SkillUsed_Slots)
            {
                if (obj.skillName.ToString() == _skillName)
                {
                    return obj;
                }
            }
            return null;
        }

        public void SwitchToGame()
        {
            SwithWithKeyTo(game);
        }

        public void OpenDead()
        {
            dead.SetActive(true);
        }
    }
}
