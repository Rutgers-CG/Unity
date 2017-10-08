using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicObstacleMovement : MonoBehaviour
{
    #region Variables
    public GameObject obstacle;

    public Transform[] wayPoints;
    private int currentWaypoint;

    public float lerpTime = 5f;
    private float currentLerpTime = 0f;

    #endregion
	
	// Update is called once per frame
	void Update ()
    {
        if (obstacle.transform.position != wayPoints[currentWaypoint].position)
        {
            currentLerpTime += Time.deltaTime;
            if(currentLerpTime >= lerpTime)
            {
                currentLerpTime = lerpTime;
            }

            float fracJourney = currentLerpTime / lerpTime;

            obstacle.transform.position = Vector3.Lerp(obstacle.transform.position, wayPoints[currentWaypoint].position, fracJourney);
        } else {
            currentLerpTime = 0;
            currentWaypoint = (currentWaypoint + 1) % wayPoints.Length;
        }
    }
}
