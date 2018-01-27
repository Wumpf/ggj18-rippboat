using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider)), RequireComponent(typeof(WaveManager))]
public class ClickListener : MonoBehaviour
{
    WaveManager WaveManager;

    Collider WaveCollider;

    public GamepadCursor[] Cursors;


    // Use this for initialization
    void Start()
    {
        WaveCollider = GetComponent<Collider>();
        WaveManager = GetComponent<WaveManager>();
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

        foreach (var cursor in Cursors)
        {
            if (cursor.MakeWave)
            {
                WaveManager.AddWave(cursor.CursorOnSurface.position, Time.time);
            }
        }
    }

   
}
