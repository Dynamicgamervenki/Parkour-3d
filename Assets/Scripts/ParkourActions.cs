using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(menuName = "Parkour/actions")]
public class ParkourActions : ScriptableObject
{
    public string objectTag;
    public string animation;
    public float length;
    public bool isSwipeUp;
}
