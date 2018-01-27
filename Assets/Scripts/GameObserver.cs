using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameObserver : MonoBehaviour
{
    public int CountdownDuration = 3;
    public Text Countdown;

    public string [] StartMessages;

    public GamepadCursor[] Cursor;

    private BoatSpawner _boatSpawner;

	// Use this for initialization
	void Start ()
	{
	    _boatSpawner = GetComponent<BoatSpawner>();

	    Countdown.text = "";
	    StartCoroutine(StartGame());
    }
	
	// Update is called once per frame
	void Update ()
	{
	    if (Input.GetKeyDown(KeyCode.R))
	        StartCoroutine(StartGame());

	    if (_boatSpawner.PlayerBoatParents[0].transform.childCount == 0)
	    {
	        Debug.Log("Player 1 wins");
	        StartCoroutine(StartGame());
        }else if (_boatSpawner.PlayerBoatParents[1].transform.childCount == 0)
	    {
	        Debug.Log("Player 1 wins");
	        StartCoroutine(StartGame());
        }

    }

    public void KillOldBoats()
    {
        while(_boatSpawner.PlayerBoatParents[0].transform.childCount>0)
            GameObject.Destroy(_boatSpawner.PlayerBoatParents[0].transform.GetChild(0));

        while (_boatSpawner.PlayerBoatParents[1].transform.childCount > 0)
            GameObject.Destroy(_boatSpawner.PlayerBoatParents[0].transform.GetChild(0));
    }

    IEnumerator StartGame()
    {

        KillOldBoats();
        _boatSpawner.StartupSpawn();

        foreach (var playerCursor in Cursor)
            playerCursor.Locked = true;

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
