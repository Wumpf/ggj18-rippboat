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

    private const float WaveCoolDownTime = 1.0f;
    private float timeSinceLastWave = WaveCoolDownTime;

    public float MaxCursorDistanceRange = 11.337f;


    void Start()
    {
        waveManager = FindObjectOfType<WaveManager>();
    }
    
    void Update()
    {
        if(Locked)
            return;

        timeSinceLastWave += Time.deltaTime;
        if (timeSinceLastWave > WaveCoolDownTime && Input.GetButtonDown(AxisFromPlayer("Wave", PlayerIndex)))
        {
            waveManager.AddWave(CursorOnSurface.position);
            timeSinceLastWave = 0.0f;
        }

        CursorOnSurface.gameObject.active = timeSinceLastWave > WaveCoolDownTime;
    }

    void FixedUpdate()
    {

        var horizontal = Input.GetAxis(AxisFromPlayer("Horizontal", PlayerIndex));
        var vertical = Input.GetAxis(AxisFromPlayer("Vertical", PlayerIndex));

        var targetCursorOnSurface = CursorOnSurface.position + new Vector3(horizontal * CursorSpeed, 0,vertical * CursorSpeed);
        targetCursorOnSurface = new Vector3(targetCursorOnSurface.x, waveManager.EvaluateWaveHeight(targetCursorOnSurface), targetCursorOnSurface.z);
        
        var targetCursorOnAir = CursorOnAir.position + new Vector3(horizontal * CursorSpeed, 0, vertical * CursorSpeed);
        var targetCursorOnAirRotation = Quaternion.LookRotation(-(Camera.main.transform.position - CursorOnAir.position).normalized, Vector3.up);

        if (Vector3.Distance(targetCursorOnAir, waveManager.transform.position) < MaxCursorDistanceRange)
        {
            CursorOnSurface.position = targetCursorOnSurface;

            CursorOnAir.position = targetCursorOnAir;
            CursorOnAir.rotation = targetCursorOnAirRotation;

        }
    }


    public static string AxisFromPlayer(string axisName, Player index)
    {
        return axisName + index.ToString().First() + index.ToString().Substring(1).ToLower();
    }

}


