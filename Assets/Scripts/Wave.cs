using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct Wave
{
    public Vector3 OriginPosition;
    public float StartTime;

    public Wave(Vector3 originPosition, float startTime)
    {
        OriginPosition = new Vector3(originPosition.x, 0F, originPosition.z);
        StartTime = startTime;
    }

    public float EvaluateWaveHeight(Vector3 position)
    {
        position = new Vector3(position.x, 0F, position.z);
        float d = Vector3.Distance(OriginPosition, position);
        float t = Time.time - StartTime;

        if (d > t * 2F)
            return 0;

        return t > 0 ? Mathf.Sin(t * 2F - d) / (t * 3F + 4F) * 6F : 0F;

    }

    public Vector3 EvaluateWaveGradient(Vector3 position)
    {
        return Vector3.zero;
    }
}