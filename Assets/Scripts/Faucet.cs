using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faucet : MonoBehaviour
{

    public GameObject WaterDropFab; // so fabulous

    public float MinDropSpawnTime = 2;
    public float MaxDropSpawnTime = 5;

    private float _currentDropTime; // Feel the drop

    private float _targetDropTime;

    // Use this for initialization
    void Start ()
    {
        _targetDropTime = Random.Range(MinDropSpawnTime, MaxDropSpawnTime);

    }
	
	// Update is called once per frame
	void Update ()
	{
	    _currentDropTime += Time.deltaTime;
	    if (_currentDropTime > _targetDropTime)
	    {
	        _currentDropTime = 0;
	        _targetDropTime = Random.Range(MinDropSpawnTime, MaxDropSpawnTime);
	        var obj = GameObject.Instantiate(WaterDropFab);
	        obj.transform.position = transform.position;
	    }
	}


}
