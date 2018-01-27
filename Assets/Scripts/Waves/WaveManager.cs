using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {

    List<Wave> Waves = new List<Wave>();
    private float time = 0.0f;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    time = Time.time; // Cache to use in thread.
	}
    
    public float EvaluateWaveHeight(Vector3 position)
    {
        float result = 0F;
        foreach (Wave w in Waves)
        {
            result += w.EvaluateWaveHeight(position, time);
        }
        return result;
    }

    public Vector3 EvaluateWaveGradient(Vector3 position)
    {
        Vector3 tmp;
        return EvaluateWaveGradient(position, out tmp);
    }

    public Vector3 EvaluateWaveGradient(Vector3 position, out Vector3 normal)
    {
        Vector3 gradient = Vector3.zero;
        foreach(Wave w in Waves)
        {
            gradient += w.EvaluateWaveGradient(position, time);
        }

        Vector3 xGradient = new Vector3(1F, -gradient.x, 0F);
        Vector3 zGradient = new Vector3(0F, -gradient.z, 1F);

        normal = Vector3.Cross(zGradient, xGradient).normalized;

        return gradient;
    }

    public void AddWave(Vector3 originPosition, float startTime)
    {
        Waves.Add(new Wave(originPosition, startTime));
    }
}
