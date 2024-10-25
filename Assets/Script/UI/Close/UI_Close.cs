using System.Collections;
using System.Collections.Generic;
using SK;
using UnityEngine;
using UnityEngine.UI;

public class UI_Close : MonoBehaviour
{
    // Start is called before the first frame update
    Button button;
    UI uI;
    void Start()
    {
        button = GetComponent<Button>();
        uI = GetComponentInParent<UI>();
        button.onClick.AddListener(Close);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Close()
    {
        uI.SwitchToGame();
    }
}
