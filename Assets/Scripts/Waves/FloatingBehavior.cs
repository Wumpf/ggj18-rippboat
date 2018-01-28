using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingBehavior : MonoBehaviour {

    private WaveManager WaveManager;

    Vector3 velocity = Vector3.zero;

    public float GradientScale = 0.02f;
    [Range(0F, 1F)]
    public float InertiaFactor = 0.2f;


    public bool StopFloating { get; set; } = false;

    private void Start()
    {
        WaveManager = FindObjectOfType<WaveManager>();
    }

    void FixedUpdate()
    {
        if (StopFloating)
            return;

        var currentPosition = transform.position;

        // get gradient
        var currentGradient = WaveManager.EvaluateWaveGradient(transform.position) * GradientScale;

        // estimate targetPosition
        Vector3 targetPosition = new Vector3(currentPosition.x - currentGradient.x, 0, currentPosition.z - currentGradient.z);
        var targetHeight = WaveManager.EvaluateWaveHeight(targetPosition);
        targetPosition = targetPosition + targetHeight * Vector3.up;

        // determine velocity for this UpdateStep
        velocity = Vector3.Lerp(targetPosition - transform.position, velocity, InertiaFactor);

        transform.position = transform.position + velocity;

        transform.position = new Vector3(transform.position.x, Mathf.Max(targetHeight, transform.position.y), transform.position.z); //Ugly Code for Cord
    }
}
