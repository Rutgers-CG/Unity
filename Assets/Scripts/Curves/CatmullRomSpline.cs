using UnityEngine;
using System.Collections;
using System;

public class CatmullRomSpline : MonoBehaviour, ICurve {


    /*
    *   Catmull Rom splines have 4 control points
    *
    */
    [SerializeField]
    private Vector3[] controlPoints;

    [SerializeField]
    private int curves;

    [SerializeField]
    private bool loop;

    public int ControlPointsCount {
        get {
            return controlPoints.Length;
        }
    }

    public bool Loop {
        get {
            return loop;
        }
        set {
            loop = value;
            // handle reforming here if needed
        }
    }

    public int Curves {
        get {
            return curves;
        }
    }

    public Vector3 GetPoint(int i) {
        return controlPoints[i];
    }

    public void SetPoint(int i, Vector3 point) {
        controlPoints[i] = point;
    }

    public Vector3 GetCurvePoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3) {
        Vector3 a = 0.5f * (2f * p1);
        Vector3 b = 0.5f * (p2 - p0);
        Vector3 c = 0.5f * (2f * p0 - 5f * p1 + 4f * p2 - p3);
        Vector3 d = 0.5f * (-p0 + 3f * p1 - 3f * p2 + p3);

        Vector3 pos = a + (b * t) + (c * t * t) + (d * t * t * t);
                // first derivative: b + 2ct + 3dt^2
        return pos;
    }

    public Vector3 GetCurveDirection(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3) {

        Vector3 b = 0.5f * (p2 - p0);
        Vector3 c = 0.5f * (2f * p0 - 5f * p1 + 4f * p2 - p3);
        Vector3 d = 0.5f * (-p0 + 3f * p1 - 3f * p2 + p3);
        
        // first derivative: b + 2ct + 3dt^2
        Vector3 pos = b + 2f * c * t + 3f * d * (float) Math.Pow(t,2);
        return pos;
    }

    /* Unity method */
    public void Reset() {
        controlPoints = new Vector3[] {
            new Vector3(1f, 0f, 0f),
            new Vector3(3f, 3f, 0f),
            new Vector3(5f, 3f, 0f),
            new Vector3(7f, 0f, 0f)
        };
        curves += 1;
    }

    /* ICurve methods */

    private int currentControlPoint = 0;
    private float currentT = 0;
    private bool running = false;
    private int velocity = 20;
    private int calls = 0;

    public void SetSteps(int v) {
        velocity = v;
    }

    public int CurrentControlPoint {
        get {
            return currentControlPoint;
        }
    }

    public bool Running {
        get {
            return running;
        }
    }

    /* Returns the next point in the curve, and advances it forward */
    public Vector3 GetCurvePoint() {
        Vector3 point = Vector3.zero;
        if ((currentControlPoint != 0 && currentControlPoint < ControlPointsCount - 2) || loop) {
            int i = currentControlPoint, 
                    iMinusOne = i == 0 ? controlPoints.Length - 1 : i - 1, 
                    iPlusOne = (i + 1) % ControlPointsCount,
                    iPlusTwo = (i + 2) % ControlPointsCount;
            currentT = calls / (float) velocity;
            currentT = Mathf.Clamp01(currentT);
            point = GetCurvePoint(currentT, controlPoints[iMinusOne], controlPoints[i], controlPoints[iPlusOne], controlPoints[iPlusTwo]);
            if (calls == velocity) {
                currentControlPoint = currentControlPoint + 1 > controlPoints.Length - 1 ? 0 : currentControlPoint + 1;
                calls = 0;
            } else calls++;
        }
        return this.transform.TransformPoint(point);
    }

    /* Returns the direction of the current point  in the curve */
    public Vector3 GetCurveDirection() {
        Vector3 dir = Vector3.zero;
        int i = currentControlPoint,
                    iMinusOne = i == 0 ? controlPoints.Length - 1 : i - 1,
                    iPlusOne = (i + 1) % ControlPointsCount,
                    iPlusTwo = (i + 2) % ControlPointsCount;
        currentT = calls / (float)velocity;
        currentT = Mathf.Clamp01(currentT);
        dir = GetCurveDirection(currentT, controlPoints[iMinusOne], controlPoints[i], controlPoints[iPlusOne], controlPoints[iPlusTwo]);
        return this.transform.TransformPoint(dir).normalized;
    }

    public void ResetCurve() {
        currentControlPoint = loop ? 0 : 1;
        currentT = 0;
    }
}
