using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {

    List<Wave> Waves = new List<Wave>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }
    
    public float EvaluateWaveHeight(Vector3 position)
    {
        float result = 0F;
        foreach (Wave w in Waves)
        {
            result += w.EvaluateWaveHeight(position);
        }
        return result;
    }

    public Vector3 EvaluateWaveGradient(Vector3 position)
    {
        Vector3 result = Vector3.zero;
        foreach(Wave w in Waves)
        {
            result += w.EvaluateWaveGradient(position);
        }
        return result;
    }

    public void AddWave(Vector3 originPosition, float startTime)
    {
        Waves.Add(new Wave(originPosition, startTime));
    }
}
