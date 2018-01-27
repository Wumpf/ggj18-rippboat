using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Mesh))]
[RequireComponent(typeof(WaveManager))]
public class WaveVisualizer : MonoBehaviour
{
	private WaveManager waveManager;
	private Mesh mesh;

	public int MeshResolution = 64;
	public const float PlaneSize = 10.0f;

	void Start ()
	{
		waveManager = GetComponent<WaveManager>();
		mesh = GetComponent<MeshFilter>().mesh;
		
		ReGenerateMesh();
	}

	void ReGenerateMesh()
	{
		Vector3[] vertices = new Vector3[MeshResolution * MeshResolution];
		
		for (int y = 0; y < MeshResolution; ++y)
		{
			for (int x = 0; x < MeshResolution; ++x)
			{
				vertices[x + y * MeshResolution] = new Vector3(
					(float) x / (MeshResolution - 1) * PlaneSize - PlaneSize * 0.5f,
					0.0f,
					(float) y / (MeshResolution - 1) * PlaneSize - PlaneSize * 0.5f);
			}
		}
		
		
		int[] indices = new int[(MeshResolution -1)* (MeshResolution-1) * 6];
		int i = 0;
		for (int y = 0; y < MeshResolution - 1; ++y)
		{
			for (int x = 0; x < MeshResolution - 1; ++x)
			{
				int vi = x + y * MeshResolution;
				indices[i] = vi;
				indices[i + 3] = indices[i + 2] = vi + 1;
				indices[i + 4] = indices[i + 1] = vi + MeshResolution;
				indices[i + 5] = vi + MeshResolution + 1;
				i += 6;
			}
		}

		mesh.vertices = vertices;
		mesh.triangles = indices;
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
		//mesh.RecalculateBounds();
		mesh.RecalculateNormals();
	}
}
