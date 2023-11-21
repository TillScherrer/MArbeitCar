using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

[CustomPropertyDrawer(typeof(Grip), true)]
public class GripPropertyDrawer : ExtendedScriptableObjectDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {




        //SerializedObject serializedObject = new SerializedObject(property.objectReferenceValue);
        //SerializedProperty childProperty = serializedObject.FindProperty("endlessGrip");



        base.OnGUI(position, property, label);
    }
}




