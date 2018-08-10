using UnityEngine;

using UnityEditor;

[CustomEditor(typeof(FlippingTool))]
public class FlippingToolEditor : Editor
{

    private bool inFlipMode = false;
    private bool oldinFlipMode = false;
    private bool needsRepaint;
    public float gamePlaneZ = 0;

    int selectedChildIndex = 0; // Selected from dropdown menu

    FlippingTool scriptFlippingTool;
    GameObject goFlippingTool;
    GameObject goShapeContainer;

    private void OnEnable()
    {
        scriptFlippingTool = (FlippingTool)target;
        scriptFlippingTool.selectFirstShapeInShapeContainer();

        goFlippingTool = scriptFlippingTool.gameObject;
        goShapeContainer = scriptFlippingTool.shapesContainer;
    }

    void OnSceneGUI()
    {
        if (inFlipMode)
        {


            Event guiEvent = Event.current;

            Ray mouseRay = HandleUtility.GUIPointToWorldRay(guiEvent.mousePosition);
            float distToGamePlane = (gamePlaneZ - mouseRay.origin.z) / mouseRay.direction.z;
            Vector3 mousePosition = mouseRay.GetPoint(distToGamePlane);


            if (guiEvent.type == EventType.MouseDown && guiEvent.button == 0)
            {

                needsRepaint = true;
                scriptFlippingTool.flipShapeInEditor(mousePosition);

            }



            // Do not deselect object if clicking in editor
            if (guiEvent.type == EventType.Layout)
            {
                HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
            }

        }



        // Repaint editor
        if (needsRepaint)
        {
            // TODO repaint editor window, find current version
            needsRepaint = false;
        }
    }


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("LEVEL FLIPPING TOOLS: ");
        inFlipMode = EditorGUILayout.Toggle("Activate Flip Mode", inFlipMode);

        if (inFlipMode && !oldinFlipMode) // Just activated toggle
        {

            // Instantiate all shapes to PaintCanvas
            scriptFlippingTool.paintAllShapesToPaintSurfaceEditor();

        }
        oldinFlipMode = inFlipMode;


        // Dropdown menu to select Player shape
        // Loop through children to get string names
        string[] optionsForDropDown = new string[goShapeContainer.transform.childCount];
        int index = 0;
        foreach (Transform child in goShapeContainer.transform)
        {
            optionsForDropDown[index] = child.gameObject.name;
            index++;
        }
        int childIndex = EditorGUILayout.Popup(selectedChildIndex, optionsForDropDown);
        if (childIndex != selectedChildIndex)
        {
            selectedChildIndex = childIndex;
            scriptFlippingTool.selectNewShape(goShapeContainer.transform.GetChild(selectedChildIndex).gameObject);
        }



    }
}