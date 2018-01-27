using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct WaveSpecs
{
    public float SpreadSpeed;
    public float BaseAmplitude;
    public float SpreadDistance;
    public float WaveFrequency;
    public float MaxDuration;

    public WaveSpecs(float spreadSpeed, float baseAmplitude, float spreadDistance, float waveFrequency, float maxDuration)
    {
        SpreadSpeed = spreadSpeed;
        BaseAmplitude = baseAmplitude;
        WaveFrequency = waveFrequency;
        SpreadDistance = spreadDistance;
        MaxDuration = maxDuration;
    }
}