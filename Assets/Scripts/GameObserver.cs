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

    public FallingObstacle FallingObstacleTemplate;
    public FallingObstacle RubberDuck;

    bool isIngame;

    public class RandomPosGenerator
    {
        readonly List<Vector3> generatedPositions = new List<Vector3>();
        private Vector3 min;
        private Vector3 max;
        
        public RandomPosGenerator(BoxCollider area)
        {
            Vector3 scaledSize = area.size;
            min = area.center - scaledSize * 0.5f;
            max = area.center + scaledSize * 0.5f;
        }
        
        public void Reset()
        {
            generatedPositions.Clear();
        }

        public Vector3 Generate()
        {
            Vector3 pos = Vector3.zero;
            float lastDist = 0.0f;
            for (int t = 0; t < 10; ++t) // brute force attempt to find a well spaced position
            {
                var newPos = new Vector3(
                    UnityEngine.Random.Range(min.x, max.x), 0,
                    UnityEngine.Random.Range(min.z, max.z));

                if (generatedPositions.Count == 0)
                    break;

                float minDist = generatedPositions.Min(x => (x - newPos).magnitude);
                if (minDist > lastDist)
                {
                    lastDist = minDist;
                    pos = newPos;
                }
            }
            generatedPositions.Add(pos);

            return pos;
        }
    }

    private RandomPosGenerator posGen;

	// Use this for initialization
	void Start ()
	{
	    _boatSpawner = GetComponent<BoatSpawner>();
	    _waveVisualizer = FindObjectOfType<WaveVisualizer>();

	    posGen = new RandomPosGenerator(GetComponent<BoxCollider>());
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
                StartCoroutine(StartGame(waterDrainAnimationDuration));
            }

            if (_boatSpawner.PlayerBoatParents[0].transform.childCount == 0)
            {
                WinnerIconDisplayer.DisplayWinnerIcon(Assets.Scripts.Player.TWO);
                StartCoroutine(DrainWater(waterDrainAnimationDuration));
            }
            else if (_boatSpawner.PlayerBoatParents[1].transform.childCount == 0)
            {
                WinnerIconDisplayer.DisplayWinnerIcon(Assets.Scripts.Player.ONE);
                StartCoroutine(DrainWater(waterDrainAnimationDuration));
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
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

        FindObjectOfType<WaveManager>().Clear();

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
        posGen.Reset();

        KillFloatingObjects();
        float currentTime = 0;

        while (currentTime < waterAnimationTime)
        {
            currentTime += Time.deltaTime;
            _waveVisualizer.transform.position = new Vector3(_waveVisualizer.transform.position.x, -(_drainWaterAnimation.Evaluate(1- (currentTime / waterAnimationTime)) * 6.8f), _waveVisualizer.transform.position.z);
            yield return null;

        }

        _boatSpawner.StartupSpawn(posGen);

        for (int i = 0; i < 5; i++)
        {
            FallingObstacle newObstacle = Instantiate(FallingObstacleTemplate);
            Vector3 rndPosition = posGen.Generate();
            newObstacle.transform.position = new Vector3(rndPosition.x, 5F, rndPosition.z);
        }

        FallingObstacle newRuberDuck = Instantiate(RubberDuck);
        Vector3 nerndPosition = posGen.Generate();
        newRuberDuck.transform.position = new Vector3(nerndPosition.x, 5F, nerndPosition.z);

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
