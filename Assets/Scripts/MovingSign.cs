using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSign : MonoBehaviour
{
    Vector3 center;


    // 1f rotationAmount is complete circle
    public float rotationAmount = 0.075f;
    public float frequency = 1f;
    public float tiltAroundX = -15f;

    private void Start()
    {
        center = transform.position;
        transform.rotation = Quaternion.Euler(tiltAroundX, 0f, 0f);
    }

    // Update is called once per frame
    private void Update()
    {
        transform.RotateAround(center, Vector3.right, rotationAmount * Mathf.Sin((Time.fixedTime) * Mathf.PI * frequency));
    }
}
