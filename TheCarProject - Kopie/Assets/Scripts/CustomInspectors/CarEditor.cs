using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.UIElements;
using Unity.VisualScripting;
using UnityEditorInternal;

[CustomEditor(typeof(Car))]
[CanEditMultipleObjects]
public class CarEditor : Editor
{
    Car car;

    SerializedProperty showFrontWheelSettings;
    SerializedProperty frontRightWheelCenter;
    SerializedProperty frontWheelRadius;
    SerializedProperty frontUse3DWheelPhysics;
    SerializedProperty frontWheelInwardThickness;
    SerializedProperty frontWheelOutwardThickness;
    SerializedProperty frontWheelShapeAccuracy;
    SerializedProperty frontSuspDist;
    SerializedProperty frontDamping;
    SerializedProperty frontSuspHardCap;
    SerializedProperty showRearWheelSettings;
    SerializedProperty rearRightWheelCenter;
    SerializedProperty rearWheelRadius;
    SerializedProperty rearUse3DWheelPhysics;
    SerializedProperty rearWheelInwardThickness;
    SerializedProperty rearWheelOutwardThickness;
    SerializedProperty rearWheelShapeAccuracy;
    SerializedProperty rearSuspDist;
    SerializedProperty rearDamping;
    SerializedProperty rearSuspHardCap;

    SerializedProperty springsByDefaultGravity;
    SerializedProperty springsByOtherValue;

    SerializedProperty solidGroundLayer;
    SerializedProperty looseGroundLayer;

    Color colorForPerformance = new Color(0.5f, 0.7f, 0.5f); //green
    Color colorForAccuracy = new Color(0.5f, 0.5f, 0.7f); //blue
    Color colorForRealism = new Color(0.5f, 0.7f, 0.7f); //yellow
    Color colorForArcade = new Color(0.7f, 0.5f, 0.5f); //red



    SerializedProperty endlessFrontWheelGrip;
    SerializedProperty endlessBackWheelGrip;
    SerializedProperty frontWheelGrip;
    SerializedProperty backWheelGrip;

    CarGizmos carGizmos;

    void OnEnable()
    {
        car = target as Car;

        //front wheel settings
        showFrontWheelSettings = serializedObject.FindProperty("showFrontWheelSettings");
        frontRightWheelCenter = serializedObject.FindProperty("frontRightWheelCenter");
        frontWheelRadius = serializedObject.FindProperty("frontWheelRadius");
        frontUse3DWheelPhysics = serializedObject.FindProperty("frontUse3DWheelPhysics");
        frontWheelInwardThickness = serializedObject.FindProperty("frontWheelInwardThickness");
        frontWheelOutwardThickness = serializedObject.FindProperty("frontWheelOutwardThickness");
        frontWheelShapeAccuracy = serializedObject.FindProperty("frontWheelShapeAccuracy");
        frontSuspDist = serializedObject.FindProperty("frontWheelSuspensionDistanceToLiftCarWeight");
        frontDamping = serializedObject.FindProperty("frontWheelDamping");
        frontSuspHardCap = serializedObject.FindProperty("frontSuspHardCap");
        //rear wheel settings
        showRearWheelSettings = serializedObject.FindProperty("showRearWheelSettings");
        rearRightWheelCenter = serializedObject.FindProperty("rearRightWheelCenter");
        rearWheelRadius = serializedObject.FindProperty("rearWheelRadius");
        rearUse3DWheelPhysics = serializedObject.FindProperty("rearUse3DWheelPhysics");
        rearWheelInwardThickness = serializedObject.FindProperty("rearWheelInwardThickness");
        rearWheelOutwardThickness = serializedObject.FindProperty("rearWheelOutwardThickness");
        rearWheelShapeAccuracy = serializedObject.FindProperty("rearWheelShapeAccuracy");
        rearSuspDist = serializedObject.FindProperty("rearWheelSuspensionDistanceToLiftCarWeight");
        rearDamping = serializedObject.FindProperty("rearWheelDamping");
        rearSuspHardCap = serializedObject.FindProperty("rearSuspHardCap");

        springsByDefaultGravity = serializedObject.FindProperty("springsByDefaultGravity");
        springsByOtherValue = serializedObject.FindProperty("springsByOtherValue");

        solidGroundLayer = serializedObject.FindProperty("solidGround");
        looseGroundLayer = serializedObject.FindProperty("looseGround");


        endlessFrontWheelGrip = serializedObject.FindProperty("endlessFrontWheelGrip");
        endlessBackWheelGrip = serializedObject.FindProperty("endlessBackWheelGrip");
        frontWheelGrip = serializedObject.FindProperty("frontWheelGrip");
        backWheelGrip = serializedObject.FindProperty("backWheelGrip");
        
        carGizmos = target.GetComponentInChildren<CarGizmos>();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        showFrontWheelSettings.boolValue = EditorGUILayout.Foldout(showFrontWheelSettings.boolValue, "Front Wheel Settings");
        if (showFrontWheelSettings.boolValue)
        {
            EditorGUI.indentLevel++;
            frontRightWheelCenter.vector3Value = EditorGUILayout.Vector3Field("PositionR", frontRightWheelCenter.vector3Value);
            frontWheelRadius.floatValue = EditorGUILayout.FloatField("Radius", frontWheelRadius.floatValue);
            frontSuspDist.floatValue = EditorGUILayout.FloatField("Loose Spring Range", frontSuspDist.floatValue);
            frontDamping.floatValue=  EditorGUILayout.Slider("Damping", frontDamping.floatValue, 0, 10);
            frontSuspHardCap.floatValue = EditorGUILayout.FloatField("Upward Suspension Stop", frontSuspHardCap.floatValue);

            
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("", GUILayout.Width(10));
            var oldColor = GUI.backgroundColor;
            if(!frontUse3DWheelPhysics.boolValue) GUI.backgroundColor = colorForPerformance;
            bool switchToRayPhysics = GUILayout.Button("Use performant Raycast");
            if (switchToRayPhysics) frontUse3DWheelPhysics.boolValue = false;
            GUI.backgroundColor = frontUse3DWheelPhysics.boolValue ? GUI.backgroundColor = colorForAccuracy : oldColor;
            bool switchTo3DWheel = GUILayout.Button("3D Wheel Collider");
            if (switchTo3DWheel) frontUse3DWheelPhysics.boolValue = true;
            EditorGUILayout.EndHorizontal();
            if (frontUse3DWheelPhysics.boolValue)
            {
                EditorGUI.indentLevel++;
                frontWheelInwardThickness.floatValue = EditorGUILayout.FloatField("Inward Thickness", frontWheelInwardThickness.floatValue);
                frontWheelOutwardThickness.floatValue = EditorGUILayout.FloatField("Outward Thickness", frontWheelOutwardThickness.floatValue);
                frontWheelShapeAccuracy.intValue = EditorGUILayout.IntSlider("Shape Accuracy", frontWheelShapeAccuracy.intValue, 4, 12);
                EditorGUI.indentLevel--;
            }
            GUI.backgroundColor = oldColor;
            EditorGUI.indentLevel--;
        }

        showRearWheelSettings.boolValue = EditorGUILayout.Foldout(showRearWheelSettings.boolValue, "Rear Wheel Settings");
        if (showRearWheelSettings.boolValue)
        {
            EditorGUI.indentLevel++;
            rearRightWheelCenter.vector3Value = EditorGUILayout.Vector3Field("PositionR", rearRightWheelCenter.vector3Value);
            rearWheelRadius.floatValue = EditorGUILayout.FloatField("Radius", rearWheelRadius.floatValue);
            rearSuspDist.floatValue = EditorGUILayout.FloatField("Loose Spring Range", rearSuspDist.floatValue);
            rearDamping.floatValue=  EditorGUILayout.Slider("Damping", rearDamping.floatValue, 0, 10);
            rearSuspHardCap.floatValue = EditorGUILayout.FloatField("Upward Suspension Stop", rearSuspHardCap.floatValue);

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("", GUILayout.Width(10));
            var oldColor = GUI.backgroundColor;
            if (!rearUse3DWheelPhysics.boolValue) GUI.backgroundColor = colorForPerformance;
            bool switchToRayPhysics = GUILayout.Button("Use performant Raycast");
            if (switchToRayPhysics) rearUse3DWheelPhysics.boolValue = false;
            GUI.backgroundColor = rearUse3DWheelPhysics.boolValue ? GUI.backgroundColor = colorForAccuracy : oldColor;
            bool switchTo3DWheel = GUILayout.Button("3D Wheel Collider");
            if (switchTo3DWheel) rearUse3DWheelPhysics.boolValue = true;
            EditorGUILayout.EndHorizontal();
            if (rearUse3DWheelPhysics.boolValue)
            {
                EditorGUI.indentLevel++;
                rearWheelInwardThickness.floatValue = EditorGUILayout.FloatField("Inward Thickness", rearWheelInwardThickness.floatValue);
                rearWheelOutwardThickness.floatValue = EditorGUILayout.FloatField("Outward Thickness", rearWheelOutwardThickness.floatValue);
                rearWheelShapeAccuracy.intValue = EditorGUILayout.IntSlider("Shape Accuracy", rearWheelShapeAccuracy.intValue, 4, 12);
                EditorGUI.indentLevel--;
            }
            GUI.backgroundColor = oldColor;
            EditorGUI.indentLevel--;
        }


        GUILayout.Space(10);
        GUILayout.Label("For which Gravity did you adjust the Suspension Springs?");
        EditorGUILayout.BeginHorizontal();
        var previousColor = GUI.backgroundColor;
        if (springsByDefaultGravity.boolValue) GUI.backgroundColor = colorForPerformance;
        bool switchToDefaultGravity = GUILayout.Button("Szene Default Gravity");
        if (switchToDefaultGravity) springsByDefaultGravity.boolValue = true;
        GUI.backgroundColor = springsByDefaultGravity.boolValue ?  previousColor : colorForPerformance;
        bool switchToCustomGravity = GUILayout.Button("Custom Gravity Value");
        if (switchToCustomGravity) springsByDefaultGravity.boolValue = false;
        EditorGUILayout.EndHorizontal();
        EditorGUI.indentLevel++;
        if (springsByDefaultGravity.boolValue)
        {
            EditorGUILayout.LabelField(("I Adjusted Spings for "+Physics.gravity.magnitude+"m/s^2 Gravity"));
        }
        else
        {
            //springsByOtherValue.floatValue = EditorGUILayout.FloatField("Adjusted Spings for Gravity: ", springsByOtherValue.floatValue);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("I Adjusted Spings for", GUILayout.MaxWidth(135));
            springsByOtherValue.floatValue = EditorGUILayout.FloatField("", springsByOtherValue.floatValue, GUILayout.MaxWidth(50));
            EditorGUILayout.LabelField("m/s^2 Gravity");
            //GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }
        GUI.backgroundColor = previousColor;
        EditorGUI.indentLevel--;
        GUILayout.Space(15);


        EditorGUILayout.PropertyField(solidGroundLayer);
        EditorGUILayout.PropertyField(looseGroundLayer);


        //endlessGrip.editable = false;
        EditorGUILayout.PropertyField(endlessFrontWheelGrip);
        if (!endlessFrontWheelGrip.boolValue)
        {
            EditorGUILayout.PropertyField(frontWheelGrip);
        }

        EditorGUILayout.PropertyField(endlessBackWheelGrip);
        if (!endlessBackWheelGrip.boolValue)
        {
            EditorGUILayout.PropertyField(backWheelGrip);
        }


        //prevent user from setting invalide values
        if (frontRightWheelCenter.vector3Value.x<0) frontRightWheelCenter.vector3Value += Vector3.left*frontRightWheelCenter.vector3Value.x;
        if (frontRightWheelCenter.vector3Value.z < 0) frontRightWheelCenter.vector3Value += Vector3.back * frontRightWheelCenter.vector3Value.z;
        if (frontWheelRadius.floatValue < 0.01f) frontWheelRadius.floatValue = 0.01f;
        if (frontSuspDist.floatValue < 0.01f) frontSuspDist.floatValue = 0.01f;
        if (frontSuspHardCap.floatValue < 0.01f) frontSuspHardCap.floatValue = 0.01f;
        if (frontWheelInwardThickness.floatValue < 0.01f) frontWheelInwardThickness.floatValue = 0.01f;
        if (frontWheelOutwardThickness.floatValue < 0.01f) frontWheelOutwardThickness.floatValue = 0.01f;

        if (rearRightWheelCenter.vector3Value.x < 0) rearRightWheelCenter.vector3Value += Vector3.left * rearRightWheelCenter.vector3Value.x;
        if (rearRightWheelCenter.vector3Value.z > 0) rearRightWheelCenter.vector3Value += Vector3.back * rearRightWheelCenter.vector3Value.z;
        if (rearWheelRadius.floatValue < 0.01f) rearWheelRadius.floatValue = 0.01f;
        if (rearSuspDist.floatValue < 0.01f) rearSuspDist.floatValue = 0.01f;
        if (rearSuspHardCap.floatValue < 0.01f) rearSuspHardCap.floatValue = 0.01f;
        if (rearWheelInwardThickness.floatValue < 0.01f) rearWheelInwardThickness.floatValue = 0.01f;
        if (rearWheelOutwardThickness.floatValue < 0.01f) rearWheelOutwardThickness.floatValue = 0.01f;
        if (springsByOtherValue.floatValue < 0.01f) springsByOtherValue.floatValue = 0.01f;

        PreventBeingInBothLayerMasks();
        



        serializedObject.ApplyModifiedProperties();
        EditorGUILayout.LabelField("(Below this object)");


        //Gizmo Visualisation
        //carGizmos.VisualiseWheels(frontRightWheelCenter.vector3Value, rearRightWheelCenter.vector3Value, frontWheelRadius.floatValue, rearWheelRadius.floatValue, frontSuspDist.floatValue, rearSuspDist.floatValue);
        carGizmos.VisualiseWheels(showFrontWheelSettings.boolValue, frontRightWheelCenter.vector3Value, frontSuspDist.floatValue, frontWheelRadius.floatValue, frontSuspHardCap.floatValue,
                                  frontUse3DWheelPhysics.boolValue, frontWheelInwardThickness.floatValue, frontWheelOutwardThickness.floatValue,
                                  showRearWheelSettings.boolValue, rearRightWheelCenter.vector3Value, rearSuspDist.floatValue, rearWheelRadius.floatValue, rearSuspHardCap.floatValue,
                                  rearUse3DWheelPhysics.boolValue, rearWheelInwardThickness.floatValue, rearWheelOutwardThickness.floatValue);


        if (EditorApplication.isPlaying)
        {
            car.UpdateDependendParameters();
            car.UpdateArrayAccessibleParameters();
        }

    }


    public static bool IsInLayerMask(int layerMask, int layer)
    {
        return (layerMask & (1 << layer)) != 0;
    }

    void PreventBeingInBothLayerMasks()
    {
        //LayerMask solidLayer = EditorGUILayout.MaskField(InternalEditorUtility.LayerMaskToConcatenatedLayersMask(solidGroundLayer), InternalEditorUtility.layers);
        //solidGroundLayer.
        LayerMask solidLayerCopy = solidGroundLayer.intValue;

        for (int i = 0; i < 32; i++)
        {
            if (IsInLayerMask(looseGroundLayer.intValue, i)){
                //Debug.Log("Layer" + i + "is in loose Layer!");
            }
            if (IsInLayerMask(looseGroundLayer.intValue, i) && IsInLayerMask(solidGroundLayer.intValue, i))
            {
                Debug.Log("A Layer can not be solid ground and loose ground at once. Accordingly Layer" + i + " ("+ LayerMask.LayerToName(i) + ") has been removed from the solid Ground mask!");
                solidLayerCopy &= ~(1 << i);
            }
        }
        solidGroundLayer.intValue = solidLayerCopy;
    }
}




//EditorGUILayout.BeginHorizontal();
//EditorGUILayout.LabelField("Loose Spring Range");
//frontSuspDist.floatValue = EditorGUILayout.FloatField("", frontSuspDist.floatValue, GUILayout.Width(50));
//EditorGUILayout.EndHorizontal();
