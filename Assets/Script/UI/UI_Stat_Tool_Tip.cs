using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace SK
{

     public class UI_Stat_Tool_Tip : MonoBehaviour
     {
          [SerializeField] private TextMeshProUGUI description;
          public void ShowStatToolTip(string _text)
          {
               description.text = _text;
               gameObject.SetActive(true);
          }
          public void HideToolTip()
          {
               description.text = "";
               gameObject.SetActive(false);
          }

          private void Start()
          {
               HideToolTip();
          }
     }
}
