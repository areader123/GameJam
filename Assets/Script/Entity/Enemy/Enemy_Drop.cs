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
        [SerializeField][Range(0, 20)] private int lightingDropedMinNumber;
        [SerializeField][Range(0, 20)] private int lightingDropedMaxNumber;
        [SerializeField]private Transform itemDropedTransform;

        
        void Start()
        {
            enemy = GetComponent<Enemy>();
        }

        private int WhetherDropLighting()
        {
            if (Random.Range(0, 100) < chanceToDropLighting)
            {
                return Random.Range(lightingDropedMinNumber, lightingDropedMaxNumber);
            }
            return 0;
        }
        public void InstantiatePrefab()
        {
            for (int i = 0; i < WhetherDropLighting(); i++)
            {
                GameObject gameObject =Instantiate(lightingPrefab);
                gameObject.transform.position = enemy.transform.position;
                gameObject.GetComponent<Item_Script>().SetUpItem(enemy.transform);

            }
        }


    }

}