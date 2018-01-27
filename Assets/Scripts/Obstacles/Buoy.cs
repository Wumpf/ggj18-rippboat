using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buoy : MonoBehaviour
{

    public float MotionRange;
    public float MovementScale = 0.02f;
    private Vector3 _startPosition;

	// Use this for initialization
	void Start ()
	{
	    _startPosition = this.transform.position;

	}

    void FixedUpdate()
    {
        var direction = _startPosition - this.transform.position;
        if (direction.magnitude > MotionRange)
        {
            this.transform.position += direction * MovementScale;

        }
    }
}
