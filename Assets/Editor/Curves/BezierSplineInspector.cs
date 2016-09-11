using UnityEngine;
using System.Collections;
using UnityEditor;
using System;

[CustomEditor(typeof(BezierSpline))]
public class BezierSplineInspector : Editor {

    /* This is to remove the Handle and add a button */
    private const float handleSize = 0.3f;
    private const float pickSize = 0.6f;
    private int selectedIndex = -1;

	private static Color[] modeColor = {
		Color.red,
		Color.magenta,
		Color.yellow
	};

    private float directionVectorsScale = 2.5f;
    private int steps = 10;
    private BezierSpline spline;
    private Transform t;
    private Quaternion q;

    /* Add a new button to the inspector to add a new curve */
    public override void OnInspectorGUI() {

		/* If we want all the members to be shown in the inspector */
		// DrawDefaultInspector(); 
        
		spline = target as BezierSpline;

        /* Add a Loop option to the inspector */
        EditorGUI.BeginChangeCheck();
        bool loop = EditorGUILayout.Toggle("Loop", spline.Loop);
        if (EditorGUI.EndChangeCheck()) {
            Undo.RecordObject(spline, "Toggle Loop");
            EditorUtility.SetDirty(spline);
            spline.Loop = loop;
        }

        /* Show on the inspector stats of the selected point */
        if (selectedIndex >= 0 && selectedIndex < spline.GetControlPointsCount) {
			// call this every time the inspector is redrawn
			DrawSelectedPointInspector();
		}
        if(GUILayout.Button("Add Curve")) {
            Undo.RecordObject(spline, "Add New Curve");
            spline.AddCurve();
            EditorUtility.SetDirty(spline);
        }
    }

    private void OnSceneGUI() {
        spline = target as BezierSpline;
        t = spline.transform;
        q = Tools.pivotRotation == PivotRotation.Local ? t.rotation : Quaternion.identity;
        
        /* Draw a line for every pair of ponits */
		for(int i = 0; i < spline.Curves * 3; i += 3) {
            
			Vector3 pA = ShowPoint(i);
            Vector3 pB = ShowPoint(i+1);
			Vector3 pC = ShowPoint(i+2);
			Vector3 pD = ShowPoint(i+3);

			/* Draw control points lines */
			Handles.color = modeColor[(int) spline.GetControlPointMode(i)];
			Handles.DrawLine(pA, pB);
			Handles.color = modeColor[(int) spline.GetControlPointMode(i+2)];
			Handles.DrawLine(pC,pD);
        }

        Vector3 lineStart = spline.GetPoint(0f);
        Handles.color = Color.green;
        Handles.DrawLine(lineStart, lineStart + spline.GetDirection(0f,0) * directionVectorsScale);
        
        for(int j = 0; j < spline.Curves; ++j) {
            for (int i = 1; i <= steps; i++) {
                float normalizedStep = i / (float)steps;
                Vector3 lineEnd = spline.GetPoint(normalizedStep, j);

				/* Draw curve */
                Handles.color = Color.white;
                Handles.DrawLine(lineStart, lineEnd);

				/* Draw Direction */
                Handles.color = Color.green;
                Handles.DrawLine(lineEnd, lineEnd + spline.GetDirection(normalizedStep, j) * directionVectorsScale);

                lineStart = lineEnd;
            }
        }
    }

    private Vector3 ShowPoint(int p) {
        Vector3 point = t.TransformPoint(spline.GetControlPoint(p));
        float zoom = 1f;
        /* If the handle is active, mark the index as currently selected for then, draw the Handle */
        Handles.color = modeColor[(int) spline.GetControlPointMode(p)];
        if(p == 0) {
            zoom *= 2f;
        }
        if (Handles.Button(point, q, handleSize * zoom, pickSize, Handles.DotCap)) {
            selectedIndex = p;
        }
		// refresh point in inspector - such that it is updated on the GUI upon selection
		Repaint();
        if(selectedIndex == p) {
            EditorGUI.BeginChangeCheck();
            point = Handles.DoPositionHandle(point, q);
            if(EditorGUI.EndChangeCheck()) {
                Undo.RecordObject(spline, "Move Cubic Curve Point");
                EditorUtility.SetDirty(spline);
                spline.SetControlPoint(p, t.InverseTransformPoint(point));
            }
        }
        return point;
    }

	private void DrawSelectedPointInspector() {
		GUILayout.Label("Selected Point");
		EditorGUI.BeginChangeCheck();
		// get the point modified on the inspector
		// add a Vector3 field
		Vector3 point = EditorGUILayout.Vector3Field("Position", spline.GetControlPoint(selectedIndex));
		if (EditorGUI.EndChangeCheck()) {
			Undo.RecordObject(spline, "Move Point");
			EditorUtility.SetDirty(spline);
			// set the new value if modified through inspector
			spline.SetControlPoint(selectedIndex, point);
		}

		EditorGUI.BeginChangeCheck();

		// Add a Pop up field to the inspector
		ControlPointMode mode = (ControlPointMode)
			EditorGUILayout.EnumPopup("Mode", spline.GetControlPointMode(selectedIndex));
		if (EditorGUI.EndChangeCheck()) {
			Undo.RecordObject(spline, "Change Point Mode");
			spline.SetControlPointMode(selectedIndex, mode);
			EditorUtility.SetDirty(spline);
		}
	}
	
}
