using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spline : MonoBehaviour
{
    private Vector3[] splinePoint;
    private int splineCount;

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

    public void OnDrawGizmosSelected()
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

    public Vector3 WhereOnSpline(Vector3 pos)
    {
        int ClosestSplinePoint = GetClosestSplinePoint(pos);

        if (ClosestSplinePoint == 0)
        {
            return splineSegment(splinePoint[0], splinePoint[1], pos);
        }

        else if (ClosestSplinePoint == splineCount - 1)
        {
            return splineSegment(splinePoint[splineCount - 1], splinePoint[splineCount - 2], pos);
        }

        else
        {
            Vector3 leftSeg = splineSegment(splinePoint[ClosestSplinePoint - 1], splinePoint[ClosestSplinePoint], pos);
            Vector3 rightSeg = splineSegment(splinePoint[ClosestSplinePoint + 1], splinePoint[ClosestSplinePoint], pos);

            if ((pos - leftSeg).sqrMagnitude <= (pos - rightSeg).sqrMagnitude)
            {
                return leftSeg;
            } else
            {
                return rightSeg;
            }
        }
    }

    private int GetClosestSplinePoint(Vector3 pos)
    {
        int closestPoint = -1;
        float shortestDistance = 0f;

        for (int i = 0; i < splineCount; i++)
        {
            float sqrDistance = (splinePoint[i] - pos).sqrMagnitude;
            if (shortestDistance == 0f || sqrDistance < shortestDistance)
            {
                shortestDistance = sqrDistance;
                closestPoint = i;
            }
        }
        return closestPoint;
    }

    public Vector3 splineSegment (Vector3 v1, Vector3 v2, Vector3 pos)
    {
        Vector3 v1ToPos = pos - v1;
        Vector3 seqDirection = (v2 - v1).normalized;

        float distanceFromV1 = Vector3.Dot(seqDirection, v1ToPos);

        if (distanceFromV1 < 0f)
        {
            return v1;
        }
        else if (distanceFromV1 * distanceFromV1 > (v2 - v1).sqrMagnitude)
        {
            return v2;
        } else
        {
            Vector3 fromV1 = seqDirection * distanceFromV1;
            return v1 + fromV1;
        }
    }
}
