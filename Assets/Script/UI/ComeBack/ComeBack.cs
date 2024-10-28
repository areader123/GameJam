using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ComeBack : MonoBehaviour
{
    private Button button;
   
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnDisable()
    {
        
    }

    private void OnButtonClick()
    {
        SceneManager.LoadScene("StartScene");
    }
}