using System.Collections;
using System.Collections.Generic;
using BayatGames.SaveGameFree;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewStart : MonoBehaviour
{
    private Button button;
 
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    
    void OnDisable()
    {
         SaveGame.Save<bool>("NewStart", true); 
    }

    private void OnButtonClick()
    {
        SceneManager.LoadScene("TeachScene");
        SaveGame.Save<bool>("NewStart", true); 
        SaveGame.DeleteAll();
    }
    
}
