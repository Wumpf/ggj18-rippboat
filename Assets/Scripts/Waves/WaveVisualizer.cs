using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
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
		Vector2[] texcoords = new Vector2[MeshResolution * MeshResolution];
		
		for (int y = 0; y < MeshResolution; ++y)
		{
			for (int x = 0; x < MeshResolution; ++x)
			{
				int index = x + y * MeshResolution;
				vertices[index] = new Vector3(
					(float) x / (MeshResolution - 1) * PlaneSize - PlaneSize * 0.5f,
					0.0f,
					(float) y / (MeshResolution - 1) * PlaneSize - PlaneSize * 0.5f);
				texcoords[index] = new Vector3(vertices[index].x, vertices[index].z);
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
		mesh.uv = texcoords;
	}

	void Update ()
	{
		Vector3[] vertices = mesh.vertices;
		Vector3 scale = transform.localScale; // nobody plans on scaling the parent!!!

		System.Threading.Tasks.Parallel.For(0, vertices.Length, i =>
		{
			var oldVertex = vertices[i];
			float newHeight = waveManager.EvaluateWaveHeight(new Vector3(oldVertex.x * scale.x, 0F, oldVertex.z * scale.z)); // new hate.
			vertices[i] = new Vector3(oldVertex.x, newHeight / scale.y, oldVertex.z);
		});
		mesh.vertices = vertices;
		//mesh.RecalculateBounds();
		mesh.RecalculateNormals();
		mesh.RecalculateTangents();
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "WaterDrop")
        {
            Debug.Log("Gggs");
        }
    }
}
