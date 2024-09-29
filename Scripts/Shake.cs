using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Shake : MonoBehaviour
{
    void Start()
    {
        InvokeRepeating("Shaky",1f,0.6f);
    }
    void Shaky()
    {
        transform.DOShakePosition(0.4f,Random.Range(0.0001f,0.0005f),0,90,false,true,ShakeRandomnessMode.Harmonic);
    }
}
