using System.Collections;
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
        yield return new WaitForSeconds(0.5f);
        GameObject.Destroy(this.gameObject);
    }
}
