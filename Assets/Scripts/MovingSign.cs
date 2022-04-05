using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSign : MonoBehaviour
{
    Vector3 center;


    // 1f rotationAmount is complete circle
    private float rotationAmplitude = 7.5f;
    private float frequency = 3f;

    private GameObject pauseMenu;

    private void Start()
    {
        center = transform.position;
        transform.RotateAround(center, transform.right, -7.5f);

        pauseMenu = GameObject.Find("UI").transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    private void Update()
    {
        // rotationAmount * Mathf.Sin((Time.fixedTime) * Mathf.PI * frequency)
        if (!pauseMenu.activeSelf)
        {
            float finalRotation = Mathf.Sin(Time.time * frequency) / rotationAmplitude;
            transform.RotateAround(center, transform.right, finalRotation);
        }
    }
}
