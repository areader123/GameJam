using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SK
{

    public class UI : MonoBehaviour
    {
        [SerializeField] private GameObject character;
        [SerializeField] private GameObject skillTree;
        [SerializeField] private GameObject craft;
        [SerializeField] private GameObject setting;

        [SerializeField] private GameObject game;



        public UI_Stat_Tool_Tip uI_Stat_Tool_Tip;


        public UI_Skill_Tool ui_Skill_Tip;
        // Start is called before the first frame update
        void Start()
        {
            SwithTo(game);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                SwithWithKeyTo(character);
            }
            // if (Input.GetKeyDown(KeyCode.B))
            // {
            //     SwithWithKeyTo(craft);
            // }
            if (Input.GetKeyDown(KeyCode.K))
            {
                SwithWithKeyTo(skillTree);
            }
            // if (Input.GetKeyDown(KeyCode.O))
            // {
            //     SwithWithKeyTo(setting);
            // }
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

            SwithTo(_menu);
        }

        public void CheckForGameUI()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).gameObject.activeSelf)
                    return;
            }

            SwithTo(game);
        }
    }
}
