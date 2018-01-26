﻿using System.Collections;
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
        float d = Vector2.Distance(OriginPosition, position);
        float t = Time.time - StartTime;
        return t > 0 ? Mathf.Sin(t * 2F - d) / (t + 10F) * 4F : 0F;
    }

    public Vector2 EvaluateWaveGradient(Vector2 position)
    {
        return Vector2.zero;
    }
}