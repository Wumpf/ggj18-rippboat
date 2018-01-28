using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FloatingBehavior)), RequireComponent(typeof(Buoy)), RequireComponent(typeof(Rigidbody))]
public class FallingBuoy : MonoBehaviour
{
    FloatingBehavior _floatingBehavior;

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
        Debug.Log("collision");

        WaveManager waveManager = collision.gameObject.GetComponent<WaveManager>();
        if (waveManager != null)
        {
            Debug.Log("collision with Wavemanager ");
            EnableFloatingBehaviour();
            waveManager.AddWave(transform.position);
        }
    }
}
