using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayBin : MonoBehaviour
{
    public Image[] allBins =new Image[3];
    public Image toBeReplaced;
    public Types type;
    void Update()
    {
        for(int i=0;i<3;i++)
        {
            if(type==allBins[i].GetComponent<BinOnDisplay>().newType)
            {
                toBeReplaced.GetComponent<Image>().color=allBins[i].GetComponent<Image>().color;
            }
        }
    }
}
