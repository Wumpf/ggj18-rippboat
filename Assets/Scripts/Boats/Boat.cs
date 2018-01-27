using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{//transform.position = 0.



	}

	
	void OnCollisionEnter(Collision collision)
	{
		if (collision.transform.tag != "Ocean")
		{
			GameObject.Destroy(this.gameObject);
		}

	}
}
