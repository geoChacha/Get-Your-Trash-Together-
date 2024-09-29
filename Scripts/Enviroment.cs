using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Enviroment : MonoBehaviour
{
    public Slider slider;
    public GameObject[] realObjects;
    public GameObject[] replacedObjects;
    private bool[] isReplaced;
    private int[] shuffledIndices;
    private int currentReplacedCount = 0;
    private float previousSliderValue;
    private bool isReplacing = false;

    void Start()
    {
        isReplaced = new bool[realObjects.Length];
        shuffledIndices = new int[realObjects.Length];
        for (int i = 0; i < shuffledIndices.Length; i++) 
            shuffledIndices[i] = i;

        ShuffleArray(shuffledIndices);
    }

    void Update()
    {
        if (!Mathf.Approximately(previousSliderValue, slider.value) && !isReplacing)
        {
            if (slider.value > previousSliderValue)
            {
                if (slider.value >= 0.3f && slider.value < 0.5f)
                {
                    StartCoroutine(ReplaceObjects(7));
                }
                else if (slider.value >= 0.5f && slider.value < 0.7f)
                {
                    StartCoroutine(ReplaceObjects(11));
                }
                else if (slider.value >= 0.7f && slider.value < 0.92f)
                {
                    StartCoroutine(ReplaceObjects(16));
                }
            }
            else
            {
                if (slider.value < 0.3f)
                {
                    StartCoroutine(ResetObjects(0));
                }
                else if (slider.value >= 0.3f && slider.value < 0.5f)
                {
                    StartCoroutine(ResetObjects(7));
                }
                else if (slider.value >= 0.5f && slider.value < 0.7f)
                {
                    StartCoroutine(ResetObjects(11));
                }
                else if (slider.value >= 0.7f && slider.value < 0.92f)
                {
                    StartCoroutine(ResetObjects(16));
                }
            }

            previousSliderValue = slider.value;
        }
    }

    IEnumerator ReplaceObjects(int totalObjectsToReplace)
    {
        isReplacing = true;
        int newObjectsToReplace = totalObjectsToReplace - currentReplacedCount;

        if (newObjectsToReplace > 0)
        {
            int replacedCount = 0;

            for (int i = 0; i < shuffledIndices.Length && replacedCount < newObjectsToReplace; i++)
            {
                int currentIndex = shuffledIndices[i]; 

                if (!isReplaced[currentIndex])
                {
                    realObjects[currentIndex].transform
                        .DOShakeScale(0.3f, 0.0005f, 0, 0, true, ShakeRandomnessMode.Harmonic)
                        .OnComplete(() => {
                            realObjects[currentIndex].SetActive(false);
                            replacedObjects[currentIndex].SetActive(true);
                        });

                    replacedObjects[currentIndex].transform
                        .DOShakeScale(0.3f, 0.0005f, 0, 0, true, ShakeRandomnessMode.Harmonic)
                        .OnComplete(() => {
                            isReplaced[currentIndex] = true;
                            replacedCount++;
                        });

                    yield return new WaitForSeconds(0.4f);
                }
            }

            currentReplacedCount += replacedCount;
        }
        isReplacing = false;
    }

    IEnumerator ResetObjects(int totalObjectsToReset)
    {
        isReplacing = true;

        if (currentReplacedCount > totalObjectsToReset)
        {
            int objectsToReset = currentReplacedCount - totalObjectsToReset;

            for (int i = shuffledIndices.Length - 1; i >= 0 && objectsToReset > 0; i--)
            {
                int currentIndex = shuffledIndices[i];

                if (isReplaced[currentIndex])
                {
                    replacedObjects[currentIndex].transform
                        .DOShakeScale(0.1f, 0.0005f, 0, 0, true, ShakeRandomnessMode.Harmonic)
                        .OnComplete(() => {
                            replacedObjects[currentIndex].SetActive(false);
                            realObjects[currentIndex].SetActive(true);
                        });

                    realObjects[currentIndex].transform
                        .DOShakeScale(0.1f, 0.0005f, 0, 0, true, ShakeRandomnessMode.Harmonic)
                        .OnComplete(() => {
                            isReplaced[currentIndex] = false;
                        });

                    objectsToReset--;
                    currentReplacedCount--;

                    yield return new WaitForSeconds(0.4f);
                }
            }
        }

        isReplacing = false;
    }

    void ShuffleArray(int[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            int temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }
}
