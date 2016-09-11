using UnityEngine;
using System.Collections;

namespace Curves {

    public class QuadBezierCurve : MonoBehaviour {
    
        public Vector3[] controlPoints;

        /* Special Unity method - Creation or Reset */
        public void Reset() {

            controlPoints = new Vector3[] {
                new Vector3(1f,0f,0f),
                new Vector3(2f,0f,0f),
                new Vector3(3f,0f,0f)
            };

        }

        public Vector3 GetPoint(float t) {
            return transform.TransformPoint(QuadBezierCurve.GetPoint(controlPoints[0], controlPoints[1], controlPoints[2], t));
        }

        /* Computing First Derivative (curve rate of change) */

        public Vector3 GetDirection(float t) {
            return this.GetCurveVelocity(t).normalized;
        }

        public Vector3 GetCurveVelocity(float t) {
            // we substract the transform.position such that we only get the slope instead of a Vector3 relative in World's coordinates
            return transform.TransformPoint(QuadBezierCurve.GetPointRateOfChange(controlPoints[0], controlPoints[1], controlPoints[2], t)) - transform.position;
        }

        /// <summary>
        /// 
        /// Given by the quadratic relation between 3 control points
        /// We derived each resulting point at time t simply by its
        /// linear interpolation relationship: B(t) = (1 - t)^2 P0 + 2 (1 - t) t P1 + t^2 P2
        /// 
        /// </summary>
        /// <param name="p0"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="t"></param>
        /// <returns></returns>

        public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t) {
            t = Mathf.Clamp01(t);
            float oneMinus = 1f - t;
            return
                    oneMinus * oneMinus * p0 +
                    2f * t * oneMinus * p1 +
                    t * t * p2;
            /*  Another way to do this is to Lerp twice for each pair of points
                return Vector3.Lerp(Vector3.Lerp(p1,p2,t), Vector3.Lerp(p2, p3, t), t);
            */
        }

        /// <summary>
        /// 
        /// Because we know the quadratic equation for a Bezier curve,
        /// we can calculate each point's, at any given t, direction
        /// using its first derivative: B'(t) = 2 (1 - t) (P1 - P0) + 2 t (P2 - P1)
        /// 
        /// This function produces lines tangent to the curve, which can be interpreted 
        /// as the speed with which we move along the curve. So now we can add a 
        /// GetVelocity method to Bezier
        /// 
        /// </summary>
        /// <param name="p0"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Vector3 GetPointRateOfChange(Vector3 p0, Vector3 p1, Vector3 p2, float t) {
            t = Mathf.Clamp01(t);
            float oneMinus = 1f - t;
            return
				2f * oneMinus * (p1 - p0) +
                2f * t * (p2 - p1);
        }

    }

}
