using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using UnityEditor.Callbacks;
using UnityEngine;
namespace SK
{

    public class Item_Script : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float damp;
        private Transform enemyTransform;
        private new Rigidbody2D rigidbody2D;

        private Vector2 velocity;
        private Vector2 direction;
        [SerializeField] private int amount;
        public void SetUpItem(Transform transform, int amount)
        {
            enemyTransform = transform;
            direction = new Vector2(Random.Range(-100, 100), Random.Range(-100, 100));
            velocity = direction.normalized * speed;
            this.amount = amount;
        }

        private void SetVelocity()
        {
            if (rigidbody2D.velocity.magnitude > 0f)
                rigidbody2D.velocity -= damp * rigidbody2D.velocity * Time.deltaTime;
            else
                rigidbody2D.velocity = Vector2.zero;
        }
        void Start()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
            rigidbody2D.velocity = velocity;
        }

        // Update is called once per frame
        void Update()
        {
            SetVelocity();
        }

        private void OnTriggerEnter2D(Collider2D hit)
        {
            if (hit.GetComponent<Character>() != null)
            {
                for (int i = 0; i < amount; i++)
                {

                    Character_Controller.instance.AddLightingNumber();
                }
                Destroy(gameObject);
            }
        }
    }
}
