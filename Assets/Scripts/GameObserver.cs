using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameObserver : MonoBehaviour
{
    public int CountdownDuration = 3;
    public Text Countdown;

    public GamepadCursor[] Cursor;

	// Use this for initialization
	void Start () {
	    Countdown.text = "";
	    StartCoroutine(StartGame());
    }
	
	// Update is called once per frame
	void Update ()
	{
	    if (Input.GetKeyDown(KeyCode.Z))
	        StartCoroutine(StartGame());
	}

    IEnumerator StartGame()
    {
        foreach (var playerCursor in Cursor)
            playerCursor.Locked = true;

        for (int i = CountdownDuration; i >=0 ; i--)
        {
            Countdown.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
        Countdown.text = "";

        foreach (var playerCursor in Cursor)
            playerCursor.Locked = false;

    }
}
