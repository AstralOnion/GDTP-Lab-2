using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(ShapeManager))]
public class ShapeManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ShapeManager manager = (ShapeManager)target;

        // Draw standard fields (arrays and sizes)
        DrawDefaultInspector();

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("Size Validation Warnings", EditorStyles.boldLabel);

        // Warning Logic
        if (manager.cubeSize > 2.0f)
        {
            EditorGUILayout.HelpBox("Warning: The cubes' sizes cannot be bigger than 2!", MessageType.Warning);
        }

        if (manager.sphereSize < 1.0f)
        {
            EditorGUILayout.HelpBox("Warning: The spheres' radius cannot be smaller than 1!", MessageType.Error);
        }

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("Selection Tools", EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Select All Cubes"))
        {
            SelectObjects(manager.cubes);
        }
        if (GUILayout.Button("Select All Spheres"))
        {
            SelectObjects(manager.spheres);
        }
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Clear Selection"))
        {
            Selection.activeObject = null;
        }

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("State Management", EditorStyles.boldLabel);

        Color originalColor = GUI.backgroundColor;

        // Cubes Enable/Disable Button (Green if active, Red if inactive)
        bool cubesActive = AreAnyEnabled(manager.cubes);
        GUI.backgroundColor = cubesActive ? Color.green : Color.red;
        if (GUILayout.Button(cubesActive ? "Disable All Cubes" : "Enable All Cubes"))
        {
            SetObjectsActive(manager.cubes, !cubesActive);
        }

        // Spheres Enable/Disable Button (Green if active, Red if inactive)
        bool spheresActive = AreAnyEnabled(manager.spheres);
        GUI.backgroundColor = spheresActive ? Color.green : Color.red;
        if (GUILayout.Button(spheresActive ? "Disable All Spheres" : "Enable All Spheres"))
        {
            SetObjectsActive(manager.spheres, !spheresActive);
        }

        // Reset GUI color back to default
        GUI.backgroundColor = originalColor;

        // Automatically update scales if sizes change in the inspector
        if (GUI.changed)
        {
            EditorUtility.SetDirty(manager);
            ApplySizes(manager);
        }
    }

    private void SelectObjects(GameObject[] objects)
    {
        List<Object> validObjects = new List<Object>();
        foreach (var obj in objects)
        {
            if (obj != null) validObjects.Add(obj);
        }
        Selection.objects = validObjects.ToArray();
    }

    private bool AreAnyEnabled(GameObject[] objects)
    {
        foreach (var obj in objects)
        {
            if (obj != null && obj.activeSelf) return true;
        }
        return false;
    }

    private void SetObjectsActive(GameObject[] objects, bool state)
    {
        Undo.RecordObjects(objects, "Toggle Shape States");
        foreach (var obj in objects)
        {
            if (obj != null) obj.SetActive(state);
        }
    }

    private void ApplySizes(ShapeManager manager)
    {
        foreach (var cube in manager.cubes)
        {
            if (cube != null) cube.transform.localScale = Vector3.one * manager.cubeSize;
        }
        foreach (var sphere in manager.spheres)
        {
            if (sphere != null) sphere.transform.localScale = Vector3.one * manager.sphereSize;
        }
    }
}