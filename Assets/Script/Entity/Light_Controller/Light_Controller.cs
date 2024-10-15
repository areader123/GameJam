using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace SK
{

    public class Light_Controller : MonoBehaviour
    {
        [Header("Light Info")]
        public static Light_Controller instance;
        [SerializeField] private float minScale;
        [SerializeField] private float radius;
        [SerializeField] private float maxScale;

        [Header("LightNumber Info")]

        [SerializeField] private int DecreaseLightingNumber;
        [SerializeField] private float timeDuration;
        [Header("lightGrow Info")]
        [SerializeField] private float scaleSpeed;
        private Character character;

        private int _maxLightingNumber;
        private int _lightingNumber;
        private float finalScale;
        private float timeCounter;
        private float scaleTimeCounter;



        private void Awake()
        {
            if (instance != null)
            {
                if (instance != null)
                {
                    Destroy(instance.gameObject);
                }
                else
                {
                    instance = this;
                }
            }
        }

        private void Update()
        {
            if (Character_Controller.instance.canChangeLightScale())
            {

                CaculateScale();

            }
            DecreaseWhenDetectEnemy();
        }

        private void DecreaseWhenDetectEnemy()
        {
            if (DetectEnemy())
            {
                timeCounter -= Time.deltaTime;
                if (timeCounter <= 0)
                {
                    Character_Controller.instance.DecreaseLightingNumber(DecreaseLightingNumber);
                    timeCounter = timeDuration;
                }
            }
        }

        private void CaculateScale()
        {
            _lightingNumber = Character_Controller.instance.GetLightingNumber();
            _maxLightingNumber = Character_Controller.instance.GetMaxLightingNumber();
            finalScale = Mathf.Lerp(minScale, maxScale, (float)_lightingNumber / _maxLightingNumber);
            //  transform.localScale = Vector2.Lerp(new Vector2(minScale, minScale), new Vector2(finalScale, finalScale), scaleSpeed * Time.deltaTime);
            transform.localScale = new Vector2(finalScale, finalScale);

        }


        private bool DetectEnemy()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, finalScale * radius);
            if (colliders == null)
            {
                return false;
            }
            foreach (var collider in colliders)
            {
                if (collider.GetComponent<Enemy>() != null)
                {
                    return true;
                }
            }
            return false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, finalScale * radius);
        }



    }

}
