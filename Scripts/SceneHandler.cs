using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public AudioSource button;
    private float timer = 0f; 
    private bool isPlayingAudio = false; 
    private string sceneToLoad;
    public void Play()
    {
        Time.timeScale = 1;
        button.Play();
        sceneToLoad = "Gameplay";
        isPlayingAudio = true;
        timer = 0f; 
        GlobalValues.score = Random.Range(45, 60);
    }

    public void Retry()
    {
        Time.timeScale = 1;
        button.Play();
        sceneToLoad = "Gameplay";
        isPlayingAudio = true;
        timer = 0f;
        Camera.main.GetComponent<Enviroment>().slider.value = 0;
        GlobalValues.score = Random.Range(45, 60); 
    }

    public void Home()
    {
        Time.timeScale = 1;
        button.Play();
        Debug.Log("One");
        sceneToLoad = "Home";
        isPlayingAudio = true;
        timer = 0f;
        Camera.main.GetComponent<Enviroment>().slider.value = 0;
        GlobalValues.score = Random.Range(45, 60);
    }

    void Update()
    {
        if (isPlayingAudio)
        {
            timer += Time.deltaTime; 
            if (timer >= button.clip.length)
            {
                isPlayingAudio = false;
                Debug.Log("Loading scene: " + sceneToLoad);
                SceneManager.LoadScene(sceneToLoad); 
            }
        }
    }
}
