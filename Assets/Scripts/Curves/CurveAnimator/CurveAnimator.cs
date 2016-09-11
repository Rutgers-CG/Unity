using UnityEngine;
using System.Collections;

public class CurveAnimator : MonoBehaviour {

    Transform t;
    Quaternion q;
    public int FrameRate = 30;

    float lastFrame = 0;

    public bool followCurve = true;
    public CatmullRomSpline curve;

	// Use this for initialization
	void Start () {
        t = this.transform;
        q = t.rotation;
        if (curve != null) {
            curve.ResetCurve();
            t.position = curve.GetCurvePoint();
        }
	}
	
	// Update is called once per frame
	void Update () {
	    if(followCurve) {
            if(lastFrame >= 1 / (float) FrameRate) {
                Vector3 p = curve != null ? curve.GetCurvePoint() : Vector3.zero;
                t.position = p == Vector3.zero ? t.position : p;
                lastFrame = 0f;
            } else {
                lastFrame += Time.deltaTime;
            }
        }
	}
}
