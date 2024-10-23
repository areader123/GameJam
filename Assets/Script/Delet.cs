using System.Collections;
using System.Collections.Generic;
using BayatGames.SaveGameFree;
using UnityEngine;

public class Delet : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        SaveGame.DeleteAll();

    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}
