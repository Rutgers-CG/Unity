using UnityEngine;
using System.Collections;
using System;

public class CubicBezierCurve : MonoBehaviour {

    public Vector3[] controlPoints;

    public void Reset() {
        controlPoints = new Vector3[] {
            new Vector3(1f, 0f, 0f),
            new Vector3(2f, 0f, 0f),
            new Vector3(3f, 0f, 0f),
            new Vector3(4f, 0f, 0f)
        };
    }

    public Vector3 GetPoint(float t) {
        return transform.TransformPoint(CubicBezierCurve.GetPoint(controlPoints[0], controlPoints[1], controlPoints[2], controlPoints[3], t)); 
    }

    public Vector3 GetDirection(float t) {
        return GetCurveVelocity(t).normalized;
    }
    
    public Vector3 GetCurveVelocity(float t) {
        return transform.TransformPoint(CubicBezierCurve.GetCurveRateOfChange(controlPoints[0], controlPoints[1], controlPoints[2], controlPoints[3], t)) - transform.position;
    }

    public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t) {
        t = Mathf.Clamp01(t);
        float oneMinus = 1f - t;
        return  
                (float) Math.Pow(oneMinus, 3) * p0 +
                3f * (float) Math.Pow(oneMinus,2) * t * p1 +
                3f * oneMinus * (float) Math.Pow(t,2) * p2 + 
                (float) Math.Pow(t,3) * p3;
    }

    public static Vector3 GetCurveRateOfChange(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t) {
        t = Mathf.Clamp01(t);
        float oneMinus = 1f - t;
        return
            3f * oneMinus * oneMinus * (p1 - p0) +
            6f * oneMinus * t * (p2 - p1) +
            3f * t * t * (p3 - p2);

    }
}
