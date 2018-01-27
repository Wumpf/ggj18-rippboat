using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class SpawnShip : MonoBehaviour
{
    public GameObject WaveManager;

	private BoatSpawner spawner;

	// Use this for initialization
	void Start ()
	{
		spawner = FindObjectOfType<BoatSpawner>();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown(1))
		{
			spawner.SpawnBoat(Player.ONE, Vector3.zero);
		}
	}

    void OnGUI()
    {
        GUI.Label(new Rect(10,10,200,100),"Right click: new ship" );

    }
}
