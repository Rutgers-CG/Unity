using UnityEngine;
using UnityEditor;
using Curves;
using System.Collections;

/// <summary>
/// 
/// This is a custom inspector for our Line component
/// It will basically draw a line on the Scene viewer for us
/// to give us visual feedback of each Line object
/// 
/// It must:
///     1. Extend UnityEditor:Editor
///     2. Implement the CustomEditor attribute
///     3. Implement Unity's OnSceneGUI for feedback
///         This is a Unity's method which will be called when rendering the Scene view
/// </summary>

[CustomEditor(typeof(Line))]
public class LineInspector : Editor {

    private void OnSceneGUI() {

        Line line = target as Line;         // target is from parent's class

        /*  Convert the line's points from local space (relative to GameObject) 
            into World space for handling transforms 
        */
        Transform t = line.transform;
        Quaternion q = Tools.pivotRotation == PivotRotation.Local ? t.rotation : Quaternion.identity; // determine Tool Handles modes (global or local)
        Vector3 worldP0 = t.TransformPoint(line.p0);
        Vector3 worldP1 = t.TransformPoint(line.p1);
        
        /* This will add a handle to each point */
        EditorGUI.BeginChangeCheck();
        worldP0 = Handles.DoPositionHandle(worldP0, q);

            // if a change happened, convert ehe worldPoint into loval point and change the line
        if (EditorGUI.EndChangeCheck()) {
            /* Allow for undoing changes, record and set dirty */
            Undo.RecordObject(line, "Move Point0");
            EditorUtility.SetDirty(line);
            line.p0 = t.InverseTransformPoint(worldP0);
            EditorUtility.SetDirty(line);
        }

        EditorGUI.BeginChangeCheck();

        worldP1 = Handles.DoPositionHandle(worldP1, q);
        if(EditorGUI.EndChangeCheck()) {
            Undo.RecordObject(line, "Move Point1");
            EditorUtility.SetDirty(line);
            line.p1 = t.InverseTransformPoint(worldP1);
            EditorUtility.SetDirty(line);
        }

        /* Draw the line on the scene - remember that Hanldes operates in world space*/
        Handles.color = Color.white;
        Handles.DrawLine(worldP0, worldP1);
    }

}
