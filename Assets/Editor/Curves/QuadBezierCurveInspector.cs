using UnityEngine;
using UnityEditor;
using System.Collections;
using Curves;

[CustomEditor(typeof(QuadBezierCurve))]
public class QuadBezierCurveInspector : Editor {

    private float directionVectorsScale = 2.5f;
    private QuadBezierCurve curve;
    private Transform mainHandleTranform;
    private Quaternion mainHandleRotation;
    private int lineSteps = 10;
    
    private void OnSceneGUI() {

        curve = target as QuadBezierCurve;
        mainHandleTranform = curve.transform;
        mainHandleRotation = Tools.pivotRotation == PivotRotation.Local ? mainHandleTranform.rotation : Quaternion.identity;

        Vector3 worldP0 = ShowPoint(0);
        Vector3 worldP1 = ShowPoint(1);
        Vector3 worldP2 = ShowPoint(2);

        Handles.color = Color.gray;
        Handles.DrawLine(worldP0, worldP1);
        Handles.DrawLine(worldP1, worldP2);

        /* Draw the curve and its velocity lines */
        Vector3 lineStart = curve.GetPoint(0f);
        Handles.color = Color.green;
        Handles.DrawLine(lineStart, lineStart + curve.GetDirection(0f) * directionVectorsScale);
        for (int i = 1; i <= lineSteps; i++) {  
            Vector3 lineEnd = curve.GetPoint(i / (float) lineSteps);

            /* Draw segment line from interpolations */
            Handles.color = Color.white;
            Handles.DrawLine(lineStart, lineEnd);
            
            /* Handle curve velocity */
            Handles.color = Color.green;
            Handles.DrawLine(lineEnd, lineEnd + curve.GetDirection( i / (float) lineSteps) * directionVectorsScale);

            lineStart = lineEnd;
        }
    }

    private Vector3 ShowPoint(int pIndx) {
        
        // Convert the point in Local (object) coordinates into world coordinates
        Vector3 point = mainHandleTranform.TransformPoint(curve.controlPoints[pIndx]);
        
        // Start recording changes
        EditorGUI.BeginChangeCheck();
        
        // Calculate the handle change and set it as the point
        point = Handles.DoPositionHandle(point, mainHandleRotation);

        if (EditorGUI.EndChangeCheck()) {
            Undo.RecordObject(curve, "Move Curve Point");
            
            // set the item as undo-able
            EditorUtility.SetDirty(curve);

            // convert the point back from World into Local coordinates
            curve.controlPoints[pIndx] = mainHandleTranform.InverseTransformPoint(point);
        }

        return point;
    }
	
}