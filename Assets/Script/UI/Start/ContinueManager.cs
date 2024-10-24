using System.Collections;
using System.Collections.Generic;
using BayatGames.SaveGameFree;
using UnityEngine;
using UnityEngine.UI;

public class ContinueManager : MonoBehaviour
{
    [SerializeField] private Continue continue_Button;
    private bool newStart;
    // Start is called before the first frame update
    private void Awake()
    {
        if (SaveGame.Exists("NewStart"))
        {
            newStart = SaveGame.Load<bool>("NewStart");
        }
    }
    void Start()
    {
        if (newStart)
        {
            continue_Button.gameObject.SetActive(true);
        }
        else
        {
            continue_Button.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnApplicationQuit()
    {

    }
}
