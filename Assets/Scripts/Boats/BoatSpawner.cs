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
	
	public GameObject BoatLogic;
	public GameObject BoatONE;
	public GameObject BoatTWO;
	public GameObject BoatTHREE;
	public GameObject BoatFOUR;
	
	
	private WaveManager waveManager;
	private BoxCollider spawnBox; 
	
	// Use this for initialization
	void Start ()
	{
		waveManager = FindObjectOfType<WaveManager>();
		spawnBox = GetComponent<BoxCollider>();
		
		StartupSpawn();
	}

	public void StartupSpawn()
	{
		Vector3 scaledSize = spawnBox.size;
		scaledSize.Scale(transform.lossyScale);
		Vector3 min = spawnBox.center + transform.position + scaledSize * 0.5f;
		Vector3 max = spawnBox.center + transform.position - scaledSize * 0.5f;
		
		foreach (var player in ((Player[]) Enum.GetValues(typeof(Player))).Take(NumPlayers))
		{
			for (int i = 0; i < StartBoatsPerPlayer; ++i)
			{
				var pos = new Vector3(
					UnityEngine.Random.Range(min.x, max.x), 0,
					UnityEngine.Random.Range(min.z, max.z));
				SpawnBoat(player, pos);
			}
		}
	}

	public void SpawnBoat(Player player, Vector3 position)
	{			
		var obj = GameObject.Instantiate(BoatLogic, position, Quaternion.identity);
		obj.GetComponent<FloatingBehavior>().WaveManager = waveManager;
		obj.GetComponent<Boat>().Owner = player;

		switch (player)
		{
			case Player.ONE:
				GameObject.Instantiate(BoatONE, obj.transform);
				break;
			case Player.TWO:
				GameObject.Instantiate(BoatTWO, obj.transform);
				break;
			case Player.THREE:
				GameObject.Instantiate(BoatTHREE, obj.transform);
				break;
			case Player.FOUR:
				GameObject.Instantiate(BoatFOUR, obj.transform);
				break;
			default:
				throw new ArgumentOutOfRangeException(nameof(player), player, null);
		}
		
	}
}
