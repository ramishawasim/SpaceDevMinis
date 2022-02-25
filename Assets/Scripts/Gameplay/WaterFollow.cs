using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFollow : MonoBehaviour
{
    public GameObject player;
    [SerializeField]
    public float y;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        Vector3 positionWithY = player.transform.position + offset;
        positionWithY.y = y;

        transform.position = positionWithY;
    }
}
