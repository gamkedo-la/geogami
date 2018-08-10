using UnityEngine;

using UnityEditor;

[CustomEditor(typeof(BarrierGridCreator))]
public class BarrierGridCreatorEditor : Editor
{

    BarrierGridCreator scriptBarrierGridCreator;

    private void OnEnable()
    {
        scriptBarrierGridCreator = (BarrierGridCreator)target;
        //scriptBarrierGridCreator.initializeVariables();

        scriptBarrierGridCreator.initialize();

    }



    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        if (GUILayout.Button("Update Grid Size"))
        {
            scriptBarrierGridCreator.updateGridSize();
            SceneView.RepaintAll();

        }

        if (GUILayout.Button("Instantiate Barriers!"))
        {
            scriptBarrierGridCreator.instantiateBarrierGrid();
            SceneView.RepaintAll();
        }


        //clearAllBarriers
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        if (GUILayout.Button("Clear All Barriers"))
        {
            scriptBarrierGridCreator.clearAllBarriers();
        }
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
    }
}


