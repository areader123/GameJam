using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using UnityEngine;
namespace SK
{

    public class Item_Script : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float damp;
        private Transform enemyTransform;
        public Rigidbody2D rigidbody2D_Item;

        private Vector2 velocity;
        private Vector2 direction;
        [SerializeField] private int amount;

        [SerializeField] private Transform transform1;
        [SerializeField] private float Radiu;
        private bool canBePickUp;
        [SerializeField] private float canBePickUpTime;
        private float timeCounter;
        public void SetUpItem(Transform transform, int amount)
        {
            enemyTransform = transform;
            direction = new Vector2(Random.Range(-100, 100), Random.Range(-100, 100));
            velocity = direction.normalized * speed;
            this.amount = amount;
        }

        private void SetVelocity()
        {
            if (rigidbody2D_Item.velocity.magnitude > 0f)
                rigidbody2D_Item.velocity -= damp * rigidbody2D_Item.velocity * Time.deltaTime;
            else
                rigidbody2D_Item.velocity = Vector2.zero;
        }
        void Start()
        {
            rigidbody2D_Item = GetComponent<Rigidbody2D>();
            rigidbody2D_Item.velocity = velocity;

            timeCounter = canBePickUpTime;
        }

        // Update is called once per frame
        void Update()
        {
            SetVelocity();
            Check();
            timeCounter -= Time.deltaTime;
        }

        private void Check()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform1.position, Radiu);
            if(timeCounter <0)
            {
                canBePickUp = true;
            }
            if (canBePickUp)
            {
                foreach (var hit in colliders)
                {
                    if (hit.GetComponent<Character>() != null)
                    {
                        for (int i = 0; i < amount; i++)
                        {
                            Character_Controller.instance.AddLightingNumber();
                        }
                        Character_Controller.instance.AddHealth();
                        Destroy(gameObject);
                    }
                }
            }
        }
        /// <summary>
        /// Callback to draw gizmos that are pickable and always drawn.
        /// </summary>  
    }
}
