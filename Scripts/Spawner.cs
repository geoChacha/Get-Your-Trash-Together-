using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] trashObjects;
    public GameObject left,right;
    public AudioSource Sound;
    public float wait;
    void Start()
    {
     InvokeRepeating("CreateObjects",5f,wait);
    }
    void CreateObjects()
    {
        
            GameObject newObj= Instantiate(trashObjects[UnityEngine.Random.Range(0,15)],new Vector3(UnityEngine.Random.Range(right.transform.position.x,left.transform.position.x),   transform.position.y,transform.position.z),quaternion.identity);
             newObj.transform.SetParent(transform.parent);
             newObj.transform.position=new Vector3(newObj.transform.position.x,newObj.transform.position.y,-3);
            GlobalValues.score-=1;
            wait-=0.04f;
            Sound.Play();
    }

}
