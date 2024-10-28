using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Audio : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private float time;
    [SerializeField]private float timeCounter;
    [SerializeField]private bool sw;   
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // PlayMusic();
    }


    public void PlayMusic()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
            StartCoroutine("StartSlowlyMusic");
        }
    }

    public void StopMusic()
    {
       // if (audioSource.isPlaying)
       // {
          sw = true;
        //}
    }
    private void Update() {
        if(audioSource.volume == 0)
        {
            audioSource.Stop();
            sw = false;
        }
        timeCounter += Time.deltaTime;
        if(sw)
        {
            audioSource.volume -= 1 / time * Time.deltaTime;
        }
    }

    private IEnumerator StartSlowlyMusic()
    {
        while (audioSource.volume < 1)
        {
            audioSource.volume += 1 / time * Time.deltaTime;
            yield return null;
        }
    }

   
}
