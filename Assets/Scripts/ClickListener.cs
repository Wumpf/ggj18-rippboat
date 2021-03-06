﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider)), RequireComponent(typeof(WaveManager))]
public class ClickListener : MonoBehaviour
{
    WaveManager WaveManager;

    // Use this for initialization
    void Start()
    {
        WaveManager = GetComponent<WaveManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if(Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hitInfo))
        {
            WaveManager.AddWave(hitInfo.point);
        }
    }

   
}
