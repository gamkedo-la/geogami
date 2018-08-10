using UnityEngine;

using UnityEditor;

[CustomEditor(typeof(TokenGridCreator))]
public class TokenGridCreatorEditor : Editor
{

    TokenGridCreator scriptTokenGridCreator;

    private void OnEnable()
    {
        scriptTokenGridCreator = (TokenGridCreator)target;
        //scriptTokenGridCreator.initializeVariables();

        scriptTokenGridCreator.initialize();

    }



    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        if (GUILayout.Button("Update Grid Size"))
        {
            scriptTokenGridCreator.updateGridSize();
            SceneView.RepaintAll();

        }

        if (GUILayout.Button("Instantiate Tokens!"))
        {
            scriptTokenGridCreator.instantiateTokenGrid();
            SceneView.RepaintAll();
        }

        //clearAllTokens
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        if (GUILayout.Button("Clear All Tokens"))
        {
            scriptTokenGridCreator.clearAllTokens();
        }
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
    }
}


