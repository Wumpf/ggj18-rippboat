using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class Boat : MonoBehaviour
{
    private WaveManager WaveManager;
	public Player Owner = Player.ONE;

    // Use this for initialization
    void Start () {
        WaveManager = FindObjectOfType<WaveManager>();
	}
	
	// Update is called once per frame
	void Update ()
	{
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

	
	void OnCollisionEnter(Collision collision)
	{
		if (collision.transform.tag == "Reef" || collision.gameObject.GetComponent<Boat>())
		{
			GameObject.Destroy(this.gameObject);
		}

	}
}
