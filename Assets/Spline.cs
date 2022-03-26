using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spline : MonoBehaviour
{
    private Vector3[] splinePoint;
    private int splineCount;

    public bool debug_drawSpline = true;

    private void Start()
    {
        setupSpline();
    }

    private void setupSpline()
    {
        splineCount = transform.childCount;
        splinePoint = new Vector3[splineCount];

        for (int i = 0; i < splineCount; i++)
        {
            splinePoint[i] = transform.GetChild(i).position;
        }
    }

    private void OnDrawGizmosSelected()
    {
        setupSpline();

        Gizmos.color = Color.red;
        if (splineCount > 1)
        {
            for (int i = 1; i < splineCount; i++)
            {
                Gizmos.DrawLine(splinePoint[i - 1], splinePoint[i]);
            }
        }
    }
}
