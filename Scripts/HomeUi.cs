using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeUi : MonoBehaviour
{
    public GameObject trash;
    public GameObject dump;
    public GameObject text;
    public GameObject button;
    public AudioSource buttonClick;
    public AudioSource bgSFX;

    void Start()
    {
        bgSFX.Play();
        InvokeRepeating("Animate", 0.3f, 0.7f);
    }
    public void Play()
    {
        buttonClick.Play();
        StartCoroutine(Coroutine());
    }
    IEnumerator Coroutine()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Gameplay");
        Time.timeScale = 1;
    }
    void Animate()
    {
        trash.transform.DOShakePosition(0.6f, 5f, 0, 0, false, true, ShakeRandomnessMode.Harmonic);
        dump.transform.DOShakePosition(0.6f, 5f, 0, 0, false, true, ShakeRandomnessMode.Harmonic);
        text.transform.DOShakeScale(0.6f, 0.07f, 0, 0, true, ShakeRandomnessMode.Harmonic);
        button.transform.DOShakeScale(0.6f, 0.07f, 0, 0, true, ShakeRandomnessMode.Harmonic);
    }
}
