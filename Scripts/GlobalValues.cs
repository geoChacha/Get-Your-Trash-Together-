using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates{Start, Wait, Action, End}
public enum Types{Reduce,Reuse,Recycle}
public class GlobalValues
{
public static float speed;
public static float score=Random.Range(45,60);
}
