using System.Collections;
using System.Collections.Generic;
using BayatGames.SaveGameFree;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewStart : MonoBehaviour
{
    private Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
    }
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(OnButtonClick);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnDisable()
    {
        SaveGame.Save<bool>("NewStart", true); 
    }

    private void OnButtonClick()
    {
        SaveGame.DeleteAll();
        SceneManager.LoadScene("GameScene");
    }
    void OnApplicationQuit()
    {

    }
}
