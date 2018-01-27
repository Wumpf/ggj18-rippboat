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
	{//transform.position = 0.

        Vector3 normal;
        WaveManager.EvaluateWaveGradient(transform.position, out normal);
        transform.up = normal;
	}

	
	void OnCollisionEnter(Collision collision)
	{
		if (collision.transform.tag == "Reef")
		{
			GameObject.Destroy(this.gameObject);
		}

	}
}
