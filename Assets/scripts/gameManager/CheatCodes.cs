using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CheatCodes : MonoBehaviour {

	// CHEAT CODES
	// ====================
	// Remove to prevent cheating

	private bool showConsole = false;

	private void DisplayConsole ()
	{
		if (Input.GetKeyDown (KeyCode.Home))
		{
			showConsole = !showConsole;
		}
	}

	private void CheckCode ()
	{
		string lowerCurrent = currentCode.ToLower();

		if (lowerCurrent.Contains ("setmoney "))
		{
			lowerCurrent = lowerCurrent.Remove (0, "setmoney ".Length);
			GameManager.GAME_MANAGER.GetComponent<PlayerInventory>().Dollars = int.Parse (lowerCurrent);
		}
		else if (lowerCurrent.Contains ("settime "))
		{
			lowerCurrent = lowerCurrent.Remove (0, "settime ".Length);
			try { Sun.secondsPerDay = int.Parse (lowerCurrent); }
			catch {}
		}
		currentCode = string.Empty;
	}

	private string currentCode = string.Empty;

	private void Update ()
	{
		DisplayConsole();
	}

	private void OnGUI ()
	{
		if (showConsole)
		{
			currentCode = GUI.TextField (new Rect(0, 0, 100f, 100f), currentCode);
			if (GUI.Button (new Rect (100f, 0, 100f, 100f), "submit")) 
				CheckCode();
		}
	}
}
