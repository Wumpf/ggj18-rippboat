using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Boat : MonoBehaviour
{
    private WaveManager WaveManager;
    public Player Owner = Player.ONE;
    private Health _health;

    private Vector3 _bounceForce;

    public float BounceMagnitude = 0.85f;
    public float BounceFriction = 0.96f;

    private bool dead = false;

    // Use this for initialization
    void Start()
    {
        WaveManager = FindObjectOfType<WaveManager>();
        _health = GetComponentInChildren<Health>();

    }

    // Update is called once per frame
    void Update()
    {
        if (dead)
            return;
        
        //transform.position = 0.

        Vector3 normal;
        Vector3 gradient = WaveManager.EvaluateWaveGradient(transform.position, out normal);
		// more magnitude, more rotating! This also protects against weirdness when nothing is going on! :)
		float rotationFactor = gradient.sqrMagnitude * 6.0f;
		if (rotationFactor > 0.000001f) // preventing errros and stuff
		{
			// rotate towards gradient. hackidihack!!!
			Quaternion current = Quaternion.LookRotation(transform.forward, normal);
			Quaternion target = Quaternion.LookRotation(-gradient.normalized, normal) * Quaternion.FromToRotation(Vector3.right, -gradient.normalized);
			//http://www.rorydriscoll.com/2016/03/07/frame-rate-independent-damping-using-lerp/
			transform.rotation = Quaternion.Slerp(current, target, 1.0f - Mathf.Exp(-Time.deltaTime * rotationFactor));
		}
	}

    void FixedUpdate()
    {
        this.transform.position += _bounceForce;
        _bounceForce *= BounceFriction;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Reef" || collision.gameObject.GetComponent<Boat>())
        {
            _health.AddDamage(1);
            if (_health.HP <= 0)
            {
                StartCoroutine(Sink());
                return;
            }

            var gradient = WaveManager.EvaluateWaveGradient(transform.position);
            float bounceFactor = Mathf.Log(gradient.magnitude + 1.1f) * BounceMagnitude;

            _bounceForce += collision.contacts[0].normal * bounceFactor;
        }
    }

    IEnumerator Sink()
    {
        // make this guy unusable :)
        foreach (var col in GetComponents<Collider>())
            col.enabled = false;
        foreach (var col in GetComponents<FloatingBehavior>())
            col.enabled = false;
        dead = true;

        while (transform.position.y > -5.0f)
        {
            transform.position = transform.position - new Vector3(0.0f, Time.deltaTime * 2.0f, 0.0f);
            yield return null;
        }
        
        Destroy(this);
    }
}