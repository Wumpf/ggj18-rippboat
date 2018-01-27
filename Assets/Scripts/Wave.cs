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
    
    public float EvaluateWaveHeight(Vector3 position, float time)
    {
        position = new Vector3(position.x, 0F, position.z);
        float d = Vector3.Distance(OriginPosition, position);
        float t = time - StartTime;

        if (d > t * 2F)
            return 0;

        return Mathf.Sin(2F * t - d) * 6F / (3F * t + 4F); // fabolous function
    }

    public Vector3 EvaluateWaveGradient(Vector3 position, float time)
    {
        position = new Vector3(position.x, 0F, position.z);
        Vector3 distance = position - OriginPosition;
        float d = distance.magnitude;
        float t = time - StartTime;

        if (d > t * 2F)
            return Vector3.zero;

        // ok, what I did here is derivating the EvaluateWaveHeight-Function
        // f(d) = sin(2t - d) * 6 / (3t + 4), replace d with sqrt(x^2 + y^2)
        // f(x,y) = sin(2t - sqrt(x^2 + y^2)) * 6 / (3t + 4)
        // f(x,y) / dx = -x/d * cos(2t - d) * 6 / (3t + 4) .... analogously for y

        float derivativeFactor = 1F / d * Mathf.Cos(2F * t - d) * 6F / (3F * t + 4F);
        return new Vector3(distance.x * derivativeFactor, 0F, distance.z * derivativeFactor); // fabolous function #2
    }
}