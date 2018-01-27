using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using UnityEngine;

public class GamepadCursor : MonoBehaviour
{
    public Player PlayerIndex;

    public Transform CursorOnSurface;
    public Transform CursorOnAir;

    private WaveManager waveManager;

    public float CursorSpeed;

    public bool Locked { get; set; }

    void Start()
    {
        waveManager = FindObjectOfType<WaveManager>();
    }
    
    void Update()
    {
        if(Locked)
            return;

        if (Input.GetButtonDown(AxisFromPlayer("Wave", PlayerIndex)))
            waveManager.AddWave(CursorOnSurface.position);
    }

    void FixedUpdate()
    {

        var horizontal = Input.GetAxis(AxisFromPlayer("Horizontal", PlayerIndex));
        var vertical = Input.GetAxis(AxisFromPlayer("Vertical", PlayerIndex));

        CursorOnSurface.position += new Vector3(horizontal * CursorSpeed, 0,vertical * CursorSpeed);
        CursorOnSurface.position = new Vector3(CursorOnSurface.position.x, waveManager.EvaluateWaveHeight(CursorOnSurface.position), CursorOnSurface.position.z);
        
        CursorOnAir.position += new Vector3(horizontal * CursorSpeed, 0, vertical * CursorSpeed);
        CursorOnAir.rotation = Quaternion.LookRotation(-(Camera.main.transform.position - CursorOnAir.position).normalized, Vector3.up);
    }


    public static string AxisFromPlayer(string axisName, Player index)
    {
        return axisName + index.ToString().First() + index.ToString().Substring(1).ToLower();
    }

}


