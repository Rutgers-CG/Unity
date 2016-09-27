using UnityEngine;
using System.Collections;

public class MoveBetweenTwoPoints : MonoBehaviour {

    public Vector3 pos1;
    public Vector3 pos2;
    public float speed = 1.0f;

    void Update()
    {
        transform.position = Vector3.Lerp(pos1, pos2, (Mathf.Sin(speed * Time.time) + 1.0f) / 2.0f);
    }
}
