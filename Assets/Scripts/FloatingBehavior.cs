using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingBehavior : MonoBehaviour {

    public WaveManager WaveManager;

    public float GradientScale = 0.02f;

    void FixedUpdate()
    {

        var currentGradient = WaveManager.EvaluateWaveGradient(transform.position) * GradientScale;
        Vector3 targetPosition = transform.position + new Vector3(currentGradient.x, 0, currentGradient.z);

        var currentHeight = WaveManager.EvaluateWaveHeight(targetPosition);

        targetPosition = new Vector3(targetPosition.x, currentHeight, targetPosition.z);

        transform.position = transform.position * 0.5f + targetPosition * 0.5f;


        transform.position = new Vector3(transform.position.x, Mathf.Max(currentHeight, transform.position.y), transform.position.z); //Ugly Code for Cord

    }
}
