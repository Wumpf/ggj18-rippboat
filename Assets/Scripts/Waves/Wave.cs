using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct Wave
{
    Vector3 OriginPosition;
    public float StartTime;
    public WaveSpecs Specs;

    public Wave(Vector3 originPosition, float startTime, WaveSpecs specs)
    {
        OriginPosition = new Vector3(originPosition.x, 0F, originPosition.z);
        StartTime = startTime;
        Specs = specs;
    }
    
    public float EvaluateWaveHeight(Vector3 position, float time)
    {
        position = new Vector3(position.x, 0F, position.z);
        float d = Vector3.Distance(OriginPosition, position);
        float t = time - StartTime;

        if (0F >= t * Specs.SpreadSpeed - d)
            return 0;

        float timeFactor = Mathf.Max(0F, (1 - t / Specs.MaxDuration));
        float distanceFactor = Mathf.Max(0F, (1 - d / Specs.SpreadDistance));
        return Mathf.Sin((Specs.SpreadSpeed * t - d) * Specs.WaveFrequency) * Specs.BaseAmplitude * timeFactor * distanceFactor; // fabolous function
    }

    public Vector3 EvaluateWaveGradient(Vector3 position, float time)
    {
        Vector3 tmp;
        return EvaluateWaveGradient(position, time, out tmp);
    }
    public Vector3 EvaluateWaveGradient(Vector3 position, float time, out Vector3 normal)
    {
        position = new Vector3(position.x, 0F, position.z);
        Vector3 distanceVector = position - OriginPosition;
        float d = distanceVector.magnitude;
        float t = time - StartTime;

        if (0F >= t * Specs.SpreadSpeed - d)
        {
            normal = Vector3.up;
            return Vector3.zero;
        }
        // ok, what I did here is derivating the EvaluateWaveHeight-Function

        float timeFactor = Mathf.Max(0F, (1 - t / Specs.MaxDuration));
        float distanceFactor = Mathf.Max(0F, (1 - d / Specs.SpreadDistance));

        float alpha = (Specs.SpreadSpeed * t - d) * Specs.WaveFrequency;
        float derivativeFactor = -timeFactor * Specs.BaseAmplitude / d * ((Specs.WaveFrequency * Mathf.Cos((alpha)) * distanceFactor) + Mathf.Sin(alpha) / Specs.SpreadDistance);
        
        Vector3 gradient = new Vector3(distanceVector.x * derivativeFactor, 0F, distanceVector.z * derivativeFactor);// fabolous function #2
        
        Vector3 xGradient = new Vector3(1F, gradient.x, 0F);
        Vector3 zGradient = new Vector3(0F, gradient.z, 1F);
        normal = Vector3.Cross(zGradient, xGradient).normalized;

        

        return gradient;
    }
}