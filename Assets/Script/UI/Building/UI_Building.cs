using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SK;
using TMPro;
public class UI_Building : MonoBehaviour
{
    [SerializeField] private GameObject textMesh;
    [SerializeField] private UI uI;
    private float timeCounter;
    private void Start()
    {
        textMesh.SetActive(false);
        uI.SwithTo(uI.game);
    }
    void Awake()
    {
    }

    private void Update()
    {
        if (!monsterSpawner.instance.isResting)
        {
             textMesh.SetActive(false);
        }
        if (textMesh.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                uI.SwithWithKeyTo(uI.character);
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                uI.SwithWithKeyTo(uI.skillTree);
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.GetComponent<Character>() != null)
        {
            if (monsterSpawner.instance.isResting)
            {
                textMesh.SetActive(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D hit)
    {
        if (hit.GetComponent<Character>() != null)
        {
            textMesh.SetActive(false);
        }
    }



}
