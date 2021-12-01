using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLight : MonoBehaviour
{
    public float intensityBase = 3f;
    public float intensityRange = 1f;
    public float intensitySpeed = 4f;
    private Light light;

    void Start()
    {
        light = GetComponent<Light>();
    }

    void Update()
    {
        light.intensity = intensityBase + (intensityRange * Mathf.Sin(Time.time * intensitySpeed));
    }
}
