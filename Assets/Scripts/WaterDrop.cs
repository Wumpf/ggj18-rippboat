﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDrop : MonoBehaviour
{
    private WaveManager _waveManager;
    public GameObject SplashEffect;

    public WaveSpecs DropSpecs;
    // Use this for initialization
    void Start ()
	{
	    _waveManager = FindObjectOfType<WaveManager>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Ocean")
        {
            _waveManager.AddWave(transform.position,DropSpecs);
            Instantiate(SplashEffect, transform.position, Quaternion.identity);

            StartCoroutine(WaitDelete());
        }
    }

    IEnumerator WaitDelete()
    {
        float waitDuration = 0.5f;
        float _runDuration = 0;

        while (_runDuration < waitDuration)
        {
            _runDuration += Time.deltaTime;
            this.transform.localScale *= 0.3f;
            yield return null;

        }

        GameObject.Destroy(this.gameObject);
    }
}
