using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {

    List<Wave> Waves = new List<Wave>();
    private float time = 0.0f;

    [SerializeField]
    WaveSpecs ClickWaveSpecs;

    // Use this for initialization
    void Start ()
    {
		
	}

    // Update is called once per frame
    void Update()
    {
        time = Time.time; // Cache to use in thread.

        for (int i = 0; i < Waves.Count; ++i)
        {
            Wave w = Waves[i];
            if (time - w.StartTime > w.Specs.MaxDuration)
            {
                Waves.RemoveAt(i);
                --i;
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Waves.Clear();
        }
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
        foreach (Wave w in Waves)
        {
            gradient += w.EvaluateWaveGradient(position, time);
        }
        normal = Vector3.up;
        
        Vector3 xGradient = new Vector3(1F, gradient.x, 0F);
        Vector3 zGradient = new Vector3(0F, gradient.z, 1F);

        normal = Vector3.Cross(zGradient, xGradient).normalized;
        
        return gradient;
    }

    public void AddWave(Vector3 originPosition)
    {
        AddWave(originPosition, ClickWaveSpecs));
    }
    
    public void AddWave(Vector3 originPosition, WaveSpecs customSpecs)
    {
        Waves.Add(new Wave(originPosition, Time.time, customSpecs));
    }

    private void OnDrawGizmos()
    {
        Color tmp = Gizmos.color;

        float StepSizeX = 0.5F;
        float StepSizeZ = 0.5F;

        for (float x = -10F; x < 10F; x += StepSizeX)
        {
            for (float z = -10F; z < 10F; z += StepSizeZ)
            {
                Vector3 position = new Vector3(x, EvaluateWaveHeight(new Vector3(x, 0F, z)), z);
                Vector3 normal;
                Vector3 gradient = EvaluateWaveGradient(position, out normal);
                
                Gizmos.color = Color.red;
                Vector3 gradientX = new Vector3(1F, gradient.x, 0F);
                Gizmos.DrawLine(position, position + 0.1F * gradientX);

                Gizmos.color = Color.blue;
                Vector3 gradientZ = new Vector3(0F, gradient.z, 1F);
                Gizmos.DrawLine(position, position + 0.1F * gradientZ);

                Gizmos.color = Color.green;
                Gizmos.DrawLine(position, position + 0.1F * normal);

            }
        }
        Gizmos.color = tmp;
    }
}
