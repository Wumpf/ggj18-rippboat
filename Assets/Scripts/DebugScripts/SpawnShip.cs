using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnShip : MonoBehaviour
{

    public GameObject ShipPrefab;
    public GameObject WaveManager;

    private bool blocked = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	    if (Input.GetMouseButton(1))
	    {
            if (!blocked)
	        {
	        
	            blocked = true;
	            var obj = GameObject.Instantiate(ShipPrefab);
	            obj.GetComponent<FloatingBehavior>().WaveManager = WaveManager.GetComponent<WaveManager>();
	        }
	    }
	    else
	    {
	        blocked = false;

	    }
	}

    void OnGUI()
    {
        GUI.Label(new Rect(10,10,200,100),"Right click: new ship" );

    }
}
