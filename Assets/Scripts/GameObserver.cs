using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;

public class GameObserver : MonoBehaviour
{
    public int CountdownDuration = 3;
    public Text Countdown;

    public string [] StartMessages;

    public GamepadCursor[] Cursor;

    public WinnerIconDisplayer WinnerIconDisplayer;

    private BoatSpawner _boatSpawner;
    private WaveVisualizer _waveVisualizer;
    public AnimationCurve _drainWaterAnimation;

    bool isIngame;

	// Use this for initialization
	void Start ()
	{
	    _boatSpawner = GetComponent<BoatSpawner>();
	    _waveVisualizer = FindObjectOfType<WaveVisualizer>();


        Countdown.text = "";
	    StartCoroutine(StartGame(2));

        isIngame = false;
    }
	
	// Update is called once per frame
	void Update ()
	{
        if (isIngame)
        {
            float waterDrainAnimationDuration = 2F;

            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log("START GAME, because KeyInput 'Restart'");
                StartCoroutine(StartGame(waterDrainAnimationDuration));
            }

            if (_boatSpawner.PlayerBoatParents[0].transform.childCount == 0)
            {
                Debug.Log("START GAME, because Player 1 sucks");
                WinnerIconDisplayer.DisplayWinnerIcon(Assets.Scripts.Player.TWO);
                StartCoroutine(DrainWater(waterDrainAnimationDuration));
            }
            else if (_boatSpawner.PlayerBoatParents[1].transform.childCount == 0)
            {
                Debug.Log("START GAME, because Player 2 sucks");
                WinnerIconDisplayer.DisplayWinnerIcon(Assets.Scripts.Player.ONE);
                StartCoroutine(DrainWater(waterDrainAnimationDuration));
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                Debug.Log("START GAME, because KeyInput 'Drain'");
                StartCoroutine(DrainWater(waterDrainAnimationDuration));
            }
        }
	}

    IEnumerator DrainWater(float waterAnimationTime)
    {
        isIngame = false;

        float currentTime = 0;

        var floatingObjects = FindObjectsOfType<FloatingBehavior>();

        foreach (var obj in floatingObjects)
        {
            obj.StopFloating = true;
            var boat = obj.GetComponent<Boat>();

            if (boat != null)
                boat.StopBouncing = true;

            var ridgid = obj.GetComponent<Rigidbody>();
            ridgid.useGravity = true;
            ridgid.constraints = RigidbodyConstraints.None;
        }


        while (currentTime < waterAnimationTime)
        {
            currentTime += Time.deltaTime;
            _waveVisualizer.transform.position = new Vector3(_waveVisualizer.transform.position.x, -_drainWaterAnimation.Evaluate(currentTime/waterAnimationTime)* 6.8f, _waveVisualizer.transform.position.z);
            yield return null;

        }

        yield return new WaitForSeconds(1.537F);

        StartCoroutine(StartGame(waterAnimationTime));
    }

    public void KillFloatingObjects()
    {
        var flObj = FindObjectsOfType<FloatingBehavior>();
        foreach (var child in flObj)
        {
            GameObject.Destroy(child.gameObject);
        }

    }

    IEnumerator StartGame(float waterAnimationTime)
    {
        isIngame = false;

        KillFloatingObjects();
        float currentTime = 0;

        while (currentTime < waterAnimationTime)
        {
            currentTime += Time.deltaTime;
            _waveVisualizer.transform.position = new Vector3(_waveVisualizer.transform.position.x, -(_drainWaterAnimation.Evaluate(1- (currentTime / waterAnimationTime)) * 6.8f), _waveVisualizer.transform.position.z);
            yield return null;

        }

        _boatSpawner.StartupSpawn();

        foreach (var playerCursor in Cursor)
            playerCursor.Locked = true;

        if (WinnerIconDisplayer != null)
        {
            WinnerIconDisplayer.ResetWinnerIcon();
        }

        for (int i = CountdownDuration; i >0 ; i--)
        {
            Countdown.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
        Countdown.text = StartMessages[Random.Range(0, StartMessages.Length)];

        foreach (var playerCursor in Cursor)
            playerCursor.Locked = false;

        yield return new WaitForSeconds(1F);

        Countdown.text = "";

        isIngame = true;
    }
}
