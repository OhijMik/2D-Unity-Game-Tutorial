using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private float speed = 2f;
    private int current_waypoint_index = 0;
    

    // Update is called once per frame
    private void Update()
    {
        if (Vector2.Distance(waypoints[current_waypoint_index].transform.position, transform.position) < 0.1f)
        {
            current_waypoint_index++;
            if (current_waypoint_index >= waypoints.Length)
            {
                current_waypoint_index = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[current_waypoint_index].transform.position, Time.deltaTime * speed);
    }
}
