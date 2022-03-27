using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineChildDraw : MonoBehaviour
{
    Spline spline;

    private void OnDrawGizmosSelected()
    {
        spline = transform.parent.gameObject.GetComponent<Spline>();
        spline.OnDrawGizmosSelected();
    }
}
