using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct Wave
{
    public Vector2 OriginPosition;
    public float StartTime;

    public Wave(Vector2 originPosition, float startTime)
    {
        OriginPosition = originPosition;
        StartTime = startTime;
    }

    public float EvaluateWaveHeight(Vector2 position, float time)
    {
        float d = Vector2.Distance(OriginPosition, position);
        float t = time - StartTime;

        if (d > t * 2F)
            return 0;

        return t > 0 ? Mathf.Sin(t * 2F - d) / (t * 3F + 4F) * 6F : 0F;

    }

    public Vector2 EvaluateWaveGradient(Vector2 position, float time)
    {
        return Vector2.zero;
    }
}