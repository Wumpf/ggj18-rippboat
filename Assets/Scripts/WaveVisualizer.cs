using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Mesh))]
[RequireComponent(typeof(WaveManager))]
public class WaveVisualizer : MonoBehaviour
{
	private WaveManager waveManager;
	private Mesh mesh;

	void Start ()
	{
		waveManager = GetComponent<WaveManager>();
		mesh = GetComponent<MeshFilter>().mesh;
	}

	void Update ()
	{
		Vector3[] vertices = mesh.vertices;
		for(int i=0; i < vertices.Length; ++i)
		{
			var oldVertex = vertices[i];
			float newHeight = waveManager.EvaluateWaveHeight(new Vector2(oldVertex.x, oldVertex.z)); // new hate.
			vertices[i] = new Vector3(oldVertex.x, newHeight, oldVertex.z);
		}
		mesh.vertices = vertices;
		mesh.RecalculateBounds();
		mesh.RecalculateNormals();
	}
}
