using UnityEngine;

using UnityEditor;

[CustomEditor(typeof(EraseTool))]
public class EraseToolEditor : Editor
{

    EraseTool scriptEraseTool;

    private void OnEnable()
    {
        scriptEraseTool = (EraseTool)target;
    }


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();



        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("WARNING: Will erase all tokens, barriers, and white canvas. ");
        EditorGUILayout.LabelField("(Does not erase player shapes) ");

        GUILayout.BeginHorizontal();
        GUILayout.Space(100);
        if (GUILayout.Button("Clear Entire Canvas", GUILayout.Width(150)))
		{	if (EditorUtility.DisplayDialog ("Warning", "Are you sure? ", "Yes", "No")) {
				scriptEraseTool.clearAllPaintSurface();
				scriptEraseTool.clearAllBarriers();
				scriptEraseTool.clearAllTokens();
			}

		}

        GUILayout.EndHorizontal();

    }
}