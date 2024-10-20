using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using SK;
public class Ui_SkillPoint_Slot : MonoBehaviour
{
     [SerializeField] private TextMeshProUGUI SkillPointNumberText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SkillPointNumberText.text = Character_Controller.instance.pointCanbeUsed.ToString();
    }
}
