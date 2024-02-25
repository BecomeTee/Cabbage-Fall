using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPoint : MonoBehaviour
{

    [SerializeField] private GameObject[] PointWay;
    private int currentPointWayIndex = 0;

    [SerializeField] private float speedPlatform = 2f;

    // Update is called once per frame
    private void Update()
    {
        if (Vector2.Distance(PointWay[currentPointWayIndex].transform.position, transform.position) < .1f)
        {
            currentPointWayIndex++;
            if (currentPointWayIndex >= PointWay.Length)
            {
                currentPointWayIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, PointWay[currentPointWayIndex].transform.position, Time.deltaTime * speedPlatform);
    }
}
