using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FloatingBehavior)), RequireComponent(typeof(Buoy)), RequireComponent(typeof(Rigidbody))]
public class FallingBuoy : MonoBehaviour
{
    FloatingBehavior _floatingBehavior;
    [SerializeField]
    WaveSpecs HitWaveSpecs;

    // Use this for initialization
    void Start ()
	{
        _floatingBehavior = GetComponent<FloatingBehavior>();
        _floatingBehavior.StopFloating = true;

    }

    void FixedUpdate()
    {
    }

    void EnableFloatingBehaviour()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;
        rb.useGravity = false;

        _floatingBehavior.StopFloating = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (this.enabled)
        {
            WaveManager waveManager = collision.gameObject.GetComponent<WaveManager>();
            if (waveManager != null)
            {
                EnableFloatingBehaviour();
                waveManager.AddWave(transform.position, HitWaveSpecs);

                this.enabled = false;
            }
        }
    }
}
