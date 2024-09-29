using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject gameOver;
    public GameObject youWon;
    public GameObject Tutorial;
    public Text remainingObjects;
    public AudioSource GameOver;
    public AudioSource YouWon;
    public AudioSource Collision;
    private bool resume=false;
    private bool gameHasEnded = false;

    void Start()
    {
        StartCoroutine(ShowTutorial());
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Escape) && !resume)
        {
          resume=true;
          Debug.Log("OK");
          Time.timeScale=0;
          return;
          
        }
        else if(Input.GetKey(KeyCode.Escape) && resume)
        {
            Time.timeScale=1;
            Debug.Log("Not OK");
            resume=false;
            return;
        }
        if (!gameHasEnded)
        {
            CheckGameStatus();
        }

        remainingObjects.text = GlobalValues.score.ToString();
    }
    void CheckGameStatus()
    {
        if (GlobalValues.score <= 0 && Camera.main.GetComponent<Enviroment>().slider.value < 1f)
        {
            YouWon.Play();
            youWon.SetActive(true);
            EndGame();
        }
        else if (Camera.main.GetComponent<Enviroment>().slider.value == 1f && GlobalValues.score > 0)
        {
            GameOver.Play();
            gameOver.SetActive(true);
            EndGame();
        }
        else if (Camera.main.GetComponent<Enviroment>().slider.value == 1f && GlobalValues.score == -1)
        {
            YouWon.Play();
            youWon.SetActive(true);
            remainingObjects.text = "0";
            EndGame();
        }
    }
    void EndGame()
    {
        gameHasEnded = true;
        Time.timeScale = 0;
    }
    IEnumerator ShowTutorial()
    {
        Tutorial.SetActive(true);
        yield return new WaitForSeconds(5f);
        Tutorial.SetActive(false);
    }
}
