using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SK;
public class zuobi : MonoBehaviour
{
    Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SkillPoint);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void SkillPoint()
    {
        Character_Controller.instance.pointCanbeUsed += 1000;
    }
}
