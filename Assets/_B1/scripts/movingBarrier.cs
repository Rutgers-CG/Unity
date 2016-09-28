using UnityEngine;
using System.Collections;

public class movingBarrier : MonoBehaviour {

    GameObject movingB;
    Vector3 startPos;
    Vector3 endPos;
    float startTime;
    float pathLength;

	// Use this for initialization
	void Start () {
        pathLength = 20.0f;
        startTime = Time.time;
        startPos = transform.position;
        endPos.x = startPos.x-24;
        endPos.y = startPos.y;
        endPos.z = startPos.z;

    }
	
	// Update is called once per frame
	void Update () {
        float distCovered = (Time.time - startTime) *1.5f;
        float fracJourney = distCovered / pathLength;
        transform.position = Vector3.Lerp(startPos, endPos, fracJourney);
        if (transform.position == endPos)
        {
            endPos = startPos;
            startPos = transform.position;
            startTime = Time.time;
        }

    }
}
