using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{

	public WaveManager WaveManager;

	public float GradientScale;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		var current2DPosition = new Vector2(transform.position.x, transform.position.z);
		
		var currentHeight = WaveManager.EvaluateWaveHeight(current2DPosition);
		var currentGradient = WaveManager.EvaluateWaveGradient(current2DPosition)*GradientScale;
		
		transform.position += new Vector3(currentGradient.x, 0, currentGradient.y);
		
		transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);
		
		
	}
	
	void OnCollisionEnter(Collision collision)
	{
		if (collision.transform.tag != "Ocean")
		{
			GameObject.Destroy(this.gameObject);
		}

	}
}
