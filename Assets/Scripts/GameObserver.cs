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

	// Use this for initialization
	void Start ()
	{
	    _boatSpawner = GetComponent<BoatSpawner>();
	    _waveVisualizer = FindObjectOfType<WaveVisualizer>();


        Countdown.text = "";
	    StartCoroutine(StartGame(2));
    }
	
	// Update is called once per frame
	void Update ()
	{
	    if (Input.GetKeyDown(KeyCode.R))
	        StartCoroutine(StartGame(2));

        if (_boatSpawner.PlayerBoatParents[0].transform.childCount == 0)
	    {
	        Debug.Log("Player 2 wins");
	        //StartCoroutine(StartGame());
	        //StartCoroutine(DrainWater(2));
	    }else if (_boatSpawner.PlayerBoatParents[0].transform.childCount == 0)
	    {
	        Debug.Log("Player 1 wins");
            //StartCoroutine(StartGame());
	        //StartCoroutine(DrainWater(2));
        }

	    if (Input.GetKeyDown(KeyCode.C))
	        StartCoroutine(DrainWater(2));

	}

    IEnumerator DrainWater(float time)
    {
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


        while (currentTime < time)
        {
            currentTime += Time.deltaTime;
            _waveVisualizer.transform.position = new Vector3(_waveVisualizer.transform.position.x, -_drainWaterAnimation.Evaluate(currentTime/time)* 6.3f, _waveVisualizer.transform.position.z);
            yield return null;

        }

    }

    public void KillFloatingObjects()
    {
        var flObj = FindObjectsOfType<FloatingBehavior>();
        foreach (var child in flObj)
        {
            GameObject.Destroy(child.gameObject);
        }

    }

    IEnumerator StartGame(float time)
    {

        KillFloatingObjects();
        float currentTime = 0;

        while (currentTime < time)
        {
            currentTime += Time.deltaTime;
            _waveVisualizer.transform.position = new Vector3(_waveVisualizer.transform.position.x, -(_drainWaterAnimation.Evaluate(1- (currentTime / time)) * 6.3f), _waveVisualizer.transform.position.z);
            yield return null;

        }

        _boatSpawner.StartupSpawn();

        foreach (var playerCursor in Cursor)
            playerCursor.Locked = true;

        WinnerIconDisplayer.ResetWinnerIcon();

        for (int i = CountdownDuration; i >0 ; i--)
        {
            Countdown.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
        Countdown.text = StartMessages[Random.Range(0, StartMessages.Length)];

        foreach (var playerCursor in Cursor)
            playerCursor.Locked = false;

        yield return new WaitForSeconds(1);

        Countdown.text = "";

    }
}
