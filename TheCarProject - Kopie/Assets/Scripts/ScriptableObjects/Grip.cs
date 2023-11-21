using System;
using UnityEditor;
using UnityEngine;




[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GripProperty", order = 1)]
public class Grip : ScriptableObject
{
    public bool alwaysMaxGrip;
    public float maxGrip;
    public float slideGripThreshold;
    public float slideGrip;

}
