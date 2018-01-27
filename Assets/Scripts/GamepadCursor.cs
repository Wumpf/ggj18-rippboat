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

    public WaveManager WaveManager;

    public float CursorSpeed;

    public bool MakeWave { get; private set; }


    void Update()
    {
        MakeWave = false;

        if (Input.GetButtonDown(AxisFromPlayer("Wave", PlayerIndex)))
            MakeWave = true;
            
    }

    void FixedUpdate()
    {
        var horizontal = Input.GetAxis(AxisFromPlayer("Horizontal", PlayerIndex));
        var vertical = Input.GetAxis(AxisFromPlayer("Vertical", PlayerIndex));

        CursorOnSurface.position += new Vector3(horizontal * CursorSpeed, 0,vertical * CursorSpeed);
        CursorOnAir.position += new Vector3(horizontal * CursorSpeed, 0, vertical * CursorSpeed);


        CursorOnSurface.position = new Vector3(CursorOnSurface.position.x, WaveManager.EvaluateWaveHeight(CursorOnSurface.position), CursorOnSurface.position.z);

    }


    public static string AxisFromPlayer(string axisName, Player index)
    {
        return axisName + index.ToString().First() + index.ToString().Substring(1).ToLower();
    }

}


