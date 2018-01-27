using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using System.Linq;

// The thing that spawns boats on startup.
// Everybody who wants to spawns boats should use the SpawnBoat func!
[RequireComponent(typeof(BoxCollider))]
public class BoatSpawner : MonoBehaviour
{
	[Range(2, 4)]
	public int NumPlayers = 2;

	[Range(1, 10)]
	public int StartBoatsPerPlayer = 2;
	
	public GameObject ShipPrefab;
	private WaveManager waveManager;
	
	// Use this for initialization
	void Start ()
	{
		StartupSpawn();
		
		waveManager = FindObjectOfType<WaveManager>();
	}

	public void StartupSpawn()
	{
		foreach (var player in ((Player[]) Enum.GetValues(typeof(Player))).Take(NumPlayers))
		{
			for (int i = 0; i < StartBoatsPerPlayer; ++i)
			{
			}
		}
	}

	public void SpawnBoat(Player player, Vector3 position)
	{			
		var obj = GameObject.Instantiate(ShipPrefab, position, Quaternion.identity);
		obj.GetComponent<FloatingBehavior>().WaveManager = waveManager;
	}
}
