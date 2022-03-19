using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallenObject : MonoBehaviour
{
    private Vector3 respawnLocation;
    private Vector3 noSpeed;
    public float thresholdHeight = -10f;

    // Start is called before the first frame update
    void Start()
    {
        respawnLocation = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < thresholdHeight)
        {
            transform.position = respawnLocation;
            gameObject.GetComponent<Rigidbody>().velocity = noSpeed;
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            gameObject.GetComponentInParent<FloatingBall>().isFloating = true;
        }
    }
}
