using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SK
{

    public class Entity_FX : MonoBehaviour
    {
        [SerializeField] private Material hittedMaterial;
        private Material orignMaterial;
        [SerializeField] private float flashDuration;
        SpriteRenderer sp;
        private Entity entity;

        private void Start()
        {
            sp = GetComponent<SpriteRenderer>();
            orignMaterial = sp.material;
        }
        private IEnumerator FlashFX()
        {
            // sp.material = hittedMaterial;
            Color orignColor = sp.color;
            sp.color = Color.black;
            Debug.Log("FX Start");
            yield return new WaitForSeconds(flashDuration);
            sp.color = orignColor;
            Debug.Log("FX end");
            //sp.material = orignMaterial;
        }
        public void Entity_FX_White()
        {
            StopAllCoroutines();
            StartCoroutine("FlashFX");
        }

        public void CancleColorChange()
        {
            CancelInvoke();
            sp.color = Color.white;
        }

        private void RedColorBlink()
        {
            if (sp.color != Color.white)
            {
                sp.color = Color.white;
            }
            else
                sp.color = Color.red;
        }

        public void RedColorBlinkFor (float _second) 
        {
            InvokeRepeating("RedColorBlink",0,.15f);
            Invoke("CancleColorChange",_second);    
        }

     


    }
}
