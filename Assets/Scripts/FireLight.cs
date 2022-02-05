using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLight : MonoBehaviour
{
    public float intensityBase = 3f;
    public float intensityRange = 1f;
    public float intensitySpeed = 4f;
    private Light light;
    private bool isWarmedUp = false;
    private float t = 0f;

    void Start()
    {
        light = GetComponent<Light>();
    }

    private void OnDisable()
    {
        light.intensity = 0f;
        t = 0f;
        isWarmedUp = false;
    }

    void Update()
    {        
        if (isWarmedUp)
        {
            light.intensity = intensityBase + (intensityRange * Mathf.Sin(Time.time * intensitySpeed));
        } else
        {
            t += Time.deltaTime;
            light.intensity = Mathf.Lerp(0f, intensityBase, t);
            if (t >= 1.0f)
            {
                isWarmedUp = true;
            }
        }
    }
}
