using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{

	public WaveManager WaveManager;

	public float GradientScale = 0.02f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{//transform.position = 0.



	}

    void FixedUpdate()
    {

        var currentGradient = WaveManager.EvaluateWaveGradient(transform.position) * GradientScale;
        Vector3 targetPosition = transform.position + new Vector3(currentGradient.x, 0, currentGradient.y);

        var currentHeight = WaveManager.EvaluateWaveHeight(targetPosition);

        targetPosition = new Vector3(targetPosition.x, currentHeight, targetPosition.z);

        transform.position = transform.position * 0.5f + targetPosition * 0.5f;


        transform.position = new Vector3(transform.position.x, Mathf.Max(currentHeight,transform.position.y), transform.position.z); //Ugly Code for Cord
        
    }
	
	void OnCollisionEnter(Collision collision)
	{
		if (collision.transform.tag != "Ocean")
		{
			GameObject.Destroy(this.gameObject);
		}

	}
}
