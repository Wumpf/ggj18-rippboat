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
    
    public float EvaluateWaveHeight(Vector2 position)
    {
        float result = 0F;
        foreach (Wave w in Waves)
        {
            result += w.EvaluateWaveHeight(position);
        }
        return result;
    }

    public Vector2 EvaluateWaveGradient(Vector2 position)
    {
        Vector2 result = Vector2.zero;
        foreach(Wave w in Waves)
        {
            result += w.EvaluateWaveGradient(position);
        }
        return result;
    }

    public void AddWave(Vector2 originPosition, float startTime)
    {
        Waves.Add(new Wave(originPosition, startTime));
    }
}
