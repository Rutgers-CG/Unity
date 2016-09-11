using UnityEngine;
using System.Collections;

public interface ICurve {

    Vector3 GetCurvePoint();

    Vector3 GetCurveDirection();

    void ResetCurve();

    void SetSteps(int v);

}
