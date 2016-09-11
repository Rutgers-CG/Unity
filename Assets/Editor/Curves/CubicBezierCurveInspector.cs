using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(CubicBezierCurve))]
public class CubicBezierCurveInspector : Editor {

    private float directionVectorsScale = 2.5f;
    private int steps = 10;
    private CubicBezierCurve curve;
    private Transform t;
    private Quaternion q;

    private void OnSceneGUI() {
        curve = target as CubicBezierCurve;
        t = curve.transform;
        q = Tools.pivotRotation == PivotRotation.Local ? t.rotation : Quaternion.identity;
        
        Vector3 world0 = ShowPoint(0);
        Vector3 world1 = ShowPoint(1);
        Vector3 world2 = ShowPoint(2);
        Vector3 world3 = ShowPoint(3);

        Handles.color = Color.gray;
        Handles.DrawLine(world0, world1);
        Handles.DrawLine(world1, world2);
        Handles.DrawLine(world2, world3);

        Vector3 lineStart = curve.GetPoint(0f);
        Handles.color = Color.green;
        Handles.DrawLine(lineStart, lineStart + curve.GetDirection(0f) * directionVectorsScale);

        for (int i = 1; i <= steps; i++) {
            float normalizedStep = i / (float)steps;
            Vector3 lineEnd = curve.GetPoint(normalizedStep);

            Handles.color = Color.white;
            Handles.DrawLine(lineStart, lineEnd);

            Handles.color = Color.green;
            Handles.DrawLine(lineEnd, lineEnd + curve.GetDirection(normalizedStep) * directionVectorsScale);
            
            lineStart = lineEnd;
        }
        
    }

    private Vector3 ShowPoint(int p) {
        Vector3 point = t.TransformPoint(curve.controlPoints[p]);
        EditorGUI.BeginChangeCheck();
        point = Handles.DoPositionHandle(point, q);
        if(EditorGUI.EndChangeCheck()) {
            Undo.RecordObject(curve, "Move Cubic Curve Point");
            EditorUtility.SetDirty(curve);
            curve.controlPoints[p] = t.InverseTransformPoint(point);
        }
        return point;
    }
	
}
