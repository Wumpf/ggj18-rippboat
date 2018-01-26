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

    public float EvaluateWaveHeight(Vector2 position)
    {
        return 0;
    }

    public Vector2 EvaluateWaveGradient(Vector2 position)
    {
        return Vector2.zero;
    }
}