using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;

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
	

	private BoxCollider spawnBox;


    public GameObject [] PlayerBoatParents;
	

	public void StartupSpawn(GameObserver.RandomPosGenerator gen)
	{

		
		List<Vector3> boatPositions = new List<Vector3>();
		
		foreach (var player in ((Player[]) Enum.GetValues(typeof(Player))).Take(NumPlayers))
		{
		    for (int i = 0; i < StartBoatsPerPlayer; ++i)
		    {
			    var boat = SpawnBoat(player, gen.Generate());

			    StartCoroutine(boat.GetComponent<Boat>().InvulnerableCountdown(2));

                boat.transform.parent = PlayerBoatParents[(int)player].transform;
			}

		}
	}

	public GameObject SpawnBoat(Player player, Vector3 position)
	{			
		var obj = GameObject.Instantiate(BoatLogic, position, Quaternion.identity);
		obj.GetComponent<Boat>().Owner = player;
		obj.transform.Rotate(0, Random.Range(0.0f, 360.0f), 0);
		
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

	    return obj;
	}
}
