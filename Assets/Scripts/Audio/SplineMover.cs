using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineMover : MonoBehaviour
{
    public Spline spline;
    public Transform followedObject;

    private Transform thisTransform;

    private void Start()
    {
        thisTransform = transform;
    }

    private void Update()
    {
        thisTransform.position = spline.WhereOnSpline(followedObject.position);
    }
}
