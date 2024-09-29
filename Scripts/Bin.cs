using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Bin : MonoBehaviour
{
    public Types newType;
    void Update()
    {
        if (transform.childCount >= 6)
        {
            transform.GetChild(0).DOComplete();
            Destroy(transform.GetChild(0).gameObject);
        }
    }
}
