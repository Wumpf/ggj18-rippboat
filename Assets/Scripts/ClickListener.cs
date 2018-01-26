using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ClickListener : MonoBehaviour
{
    [SerializeField]
    WaveManager WaveManager;

    Collider WaveCollider;


    // Use this for initialization
    void Start()
    {
        WaveCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if(Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hitInfo) && hitInfo.collider == WaveCollider)
        {
            WaveManager.AddWave(hitInfo.point, Time.time);
        }
    }

   
}
