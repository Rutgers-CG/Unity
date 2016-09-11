using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(CatmullRomSpline))]
public class CatmullRomInspector : Editor {

    private int selectedIndex = -1;
    private int steps = 10;

    CatmullRomSpline spline;
    Quaternion q;
    Transform t;

    public override void OnInspectorGUI() {
        EditorGUI.BeginChangeCheck();
        bool loop = EditorGUILayout.Toggle("Loop", spline.Loop);
        if (EditorGUI.EndChangeCheck()) {
            Undo.RecordObject(spline, "Toggle Loop");
            EditorUtility.SetDirty(spline);
            spline.Loop = loop;
        }
    }

	public void OnSceneGUI() {

        spline = target as CatmullRomSpline;
        t = spline.transform;
        q = Tools.pivotRotation == PivotRotation.Local ? t.rotation : Quaternion.identity;
        
        /* Draw control points for each curve */
        for (int i = 0; i < spline.Curves; i += 3) {

            Vector3 point0 = ShowPoint(i);
            Vector3 point1 = ShowPoint(i + 1);
            Vector3 point2 = ShowPoint(i + 2);
            Vector3 point3 = ShowPoint(i + 3);

            Handles.color = Color.gray;
            Handles.DrawLine(point0, point1);
            Handles.DrawLine(point1, point2);
            Handles.DrawLine(point2, point3);
        }

        /* Draw the curve */
        for(int i = 0; i < spline.ControlPointsCount; ++i) {
            if ((i == 0 || i >= spline.ControlPointsCount - 2) && !spline.Loop) {
                continue;
            }  else {

                int iMinusOne = i - 1, iPlusOne = i + 1, iPlusTwo = i + 2, points = spline.ControlPointsCount;

                /* We only care on negative values */
                if (i == 0) {
                    iMinusOne = spline.ControlPointsCount - 1;
                }
                /* Handle the overflow by modding around the array */
                iPlusOne = (i + 1) % points;
                iPlusTwo = (i + 2) % points;
                

                Vector3 p0 = t.TransformPoint(spline.GetPoint(iMinusOne));
                Vector3 p1 = t.TransformPoint(spline.GetPoint(i));
                Vector3 p2 = t.TransformPoint(spline.GetPoint(iPlusOne));
                Vector3 p3 = t.TransformPoint(spline.GetPoint(iPlusTwo));

                Vector3 startPoint = t.TransformPoint(spline.GetPoint(i));
                Handles.color = Color.green;
                Vector3 endPoint;
                for (int j = 0; j < steps; ++j) {
                    float norm = j / (float) steps;
                    endPoint = spline.GetCurvePoint(norm, p0, p1, p2, p3);
                    Handles.DrawLine(startPoint, endPoint);
                    startPoint = endPoint;
                }
                endPoint = spline.GetCurvePoint(1, p0, p1, p2, p3);
                Handles.DrawLine(startPoint, endPoint);
            }
        }

    }

    private Vector3 ShowPoint(int index) {

        Vector3 point = t.TransformPoint(spline.GetPoint(index));
        float firstPoint = index == 0 ? 2f : 1f;
        if (Handles.Button(point, q, 0.2f * firstPoint, 0.6f, Handles.DotCap)) {
            selectedIndex = index;
        }

        Handles.color = Color.gray;
        if(selectedIndex == index) {
            EditorGUI.BeginChangeCheck();
            point = Handles.DoPositionHandle(point, q);
            if(EditorGUI.EndChangeCheck()) {
                Undo.RecordObject(spline, "CatmullRom Moved");
                EditorUtility.SetDirty(spline);
                spline.SetPoint(index, t.InverseTransformPoint(point));
            }
        }
        return point;
    }
}
