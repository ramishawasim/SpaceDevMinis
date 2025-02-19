using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public List<Transform> waypoints;
    public float moveSpeed;
    public int target;
    public Vector3 publicPlatformVectorDirection;


    void Update()
    {
        //recieve data

        publicPlatformVectorDirection = transform.position - Vector3.MoveTowards(transform.position, waypoints[target].position, moveSpeed);
        transform.position = Vector3.MoveTowards(transform.position, waypoints[target].position, moveSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        //use data
        if (transform.position == waypoints[target].position)
        {
            if (target == waypoints.Count - 1)
            {
                target = 0;
            }
            else
            {
                target += 1;
            }
        }
    }
}
