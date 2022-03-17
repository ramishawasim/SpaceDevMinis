using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingEgg : MonoBehaviour
{
    public float degreesPerSecond = 15.0f;
    public float amplitude = 0.5f;
    public float frequency = 1f;

    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    float extraOffset;

    // Start is called before the first frame update
    void Start()
    {
        posOffset = transform.position;
        extraOffset = (transform.position.x - transform.position.z) / 4f;
    }

    // Update is called once per frame
    void Update()
    {
        // Spin object around Y-Axis
        // transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);

        // Float up/down with a Sin()
        tempPos = posOffset;
        tempPos.y += Mathf.Sin((Time.fixedTime + extraOffset) * Mathf.PI * frequency) * amplitude;

        transform.position = tempPos;
    }
}
