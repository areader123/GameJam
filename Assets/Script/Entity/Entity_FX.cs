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

        private void Start()
        {
            sp = GetComponent<SpriteRenderer>();
            orignMaterial = sp.material;
        }
        private IEnumerator FlashFX()
        {
           // sp.material = hittedMaterial;
            Debug.Log("FX");
            Color orignColor = sp.color;
            sp.color = Color.black;
            yield return new WaitForSeconds(flashDuration);
            sp.color = orignColor;
            //sp.material = orignMaterial;
        }
        public void Entity_FX_White()
        {
           // StopAllCoroutines();
            StartCoroutine("FlashFX");
        }
    }
}
