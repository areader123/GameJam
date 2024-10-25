using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SK
{
    public class Enemy_Drop : MonoBehaviour
    {
        private Enemy enemy;
        [SerializeField] GameObject lightingPrefab;
        [SerializeField][Range(0, 100)] float chanceToDropLighting;
        [SerializeField] private int lightingDropedMinNumber;
        [SerializeField] private int lightingDropedMaxNumber;
        [SerializeField][Range(0,20)] private int perLevelDropAdded;
        [SerializeField][Range(1,5)] private int lightingPrefabAmount;
        [SerializeField]private Transform itemDropedTransform;

        
        void Start()
        {
            enemy = GetComponent<Enemy>();
        }

        private int WhetherDropLighting()
        {
            if (Random.Range(0, 100) < chanceToDropLighting)
            {
                return Random.Range(lightingDropedMinNumber + perLevelDropAdded * Character_Controller.instance.GetLevel(), lightingDropedMaxNumber+ perLevelDropAdded * Character_Controller.instance.GetLevel());
            }
            return 0;
        }
        public void InstantiatePrefab()
        {
            for (int i = 0; i < Random.Range(1,lightingPrefabAmount); i++)
            {
                GameObject gameObject =Instantiate(lightingPrefab);
                gameObject.transform.position = enemy.transform.position;
                gameObject.GetComponent<Item_Script>().SetUpItem(enemy.transform,WhetherDropLighting());

            }
        }


    }

}