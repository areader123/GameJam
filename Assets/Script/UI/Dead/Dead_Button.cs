using System.Collections;
using System.Collections.Generic;
using BayatGames.SaveGameFree;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Dead_Button : MonoBehaviour
{
    private Button button;
    // Start is called before the first frame update
    private void Awake()
    {
        button = GetComponent<Button>();

    }
    void Start()
    {
        button.onClick.AddListener(OnButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnButtonClicked()
    {
        SaveGame.DeleteAll();
        SaveGame.Save<bool>("NewStart", false);
        SceneManager.LoadScene("StartScene");
    }

    private void OnApplicationQuit()
    {
        SaveGame.DeleteAll();
        SaveGame.Save<bool>("NewStart", false);
    }
}
