using UnityEngine;
using System.Collections;

public class Sun : MonoBehaviour {

	private Light2D sun;

	// one turn will last this many seconds
	public float secondsPerDay = 180f;

	private float startTime;

	// money values - allows comparison in a single turn
	private float startMoney;
	private float endMoney;

	// xp change
	private int xpChange = 0;
	private int xpOld = 0;
	
	private void Start () {
		Invoke ("Reset", .2f);
	}

	// turn finishing steps
	private bool turnComplete = false;
	private void CompleteTurn()
	{
		if (!turnComplete)
		// put all one-time events (xp calculation, etc.) here
		{
			print ("done");
			// basic XP rewards
			// remember old XP
			xpOld = Stats.XP;
			// calculate reward XP
			xpChange = (int)(income / 2); // make money, get XP
			// can't lost XP
			if (xpChange < 0) xpChange = 0;
			// apply XP
			Stats.XP += xpChange;

			// mark one-time events as done
			turnComplete = true;
		}

		// freeze player
		GameManager.PLAYER.GetComponent<WASDMovement>().CurrentType = WASDMovement.MovementType.TurnDone;
		
		// TODO: hide catalog

		// remove the sun timer
		Destroy (sun);
		// save player's finishing money
		endMoney = GameManager.GAME_MANAGER.GetComponent<PlayerInventory>().Dollars;
	}

	// show this turn's stats
	// TODO: make this look nicer
	public static float income = -1f;
	//private bool checkDone = false;
	private void TurnCompleteWindow ()
	{
		float boxSize = 400f;
		GUILayout.BeginArea (new Rect (Screen.width / 2 - boxSize / 2,
		                               Screen.height / 2 - boxSize / 2,
		                               boxSize,
		                               boxSize));
		GUILayout.Box ("LEVEL NAME");
		// TODO: more level stats

		// net income/loss
		income = endMoney - startMoney;
		GUILayout.Box (string.Format ("Net income: {0}${1}",
		                              (income >= 0 ? "+" : "-"),
		                              income.ToString ()));

		// check goals
		foreach (Goal g in GoalsDisplay.allGoalsArray)
		{
			GUILayout.Box (g.description);
			if (g.complete)
				g.Rewards();
		}

		// XP rewards display
		GUILayout.BeginHorizontal();
		// where'd you get your XP>
		GUILayout.Box (string.Format ("XP Breakdown\n{0} from money", (int)income / 2));
		// how much XP did you have before? how much XP did you get? how much to the next level?
		GUILayout.Box (string.Format ("XP gain: {0}\nTotal XP: {1}\nTo Next Level: {2}", xpChange, xpOld + xpChange, Stats.ToNextLevel()));
		GUILayout.EndHorizontal();
			

		// navigation from this menu
		GUILayout.BeginHorizontal();
		if (GUILayout.Button ("Back to base..."))
		{
			//LevelSerializer.SaveGame("latest");
			Autosave.SaveNow();
			Application.LoadLevel ("concourse");
		}
		if (GUILayout.Button ("Skip to next day..."))
		{
			Reset ();
		}



		GUILayout.EndHorizontal();
		GUILayout.EndArea();
	}

	private void OnGUI ()
	{
		GUI.skin = GameManager.GUI_SKIN;
		if (turnComplete)
		{
			TurnCompleteWindow();
		}
		
	}

	private void Reset ()
	{
		// instantiate a new sun
		sun = Light2D.Create (new Vector2 (transform.position.x + .5f, transform.position.y),
		                      new Color (1, 1, 1, 1f), .1f);
		// record the starting time
		startTime = Time.time;
		turnComplete = false;
		// reset player's position
		try { GameManager.PLAYER.GetComponent<Respawn>().RespawnPlayer(); }
		catch {}
		// save player's starting money
		startMoney = GameManager.GAME_MANAGER.GetComponent<PlayerInventory>().Dollars;

	}



	private void Update ()
	{
		// sun functions as timer - rotates around the city tableau at the top of the screen
		// once it sets in the west, the day (and the turn) are done
		if (sun != null)
		{
			sun.transform.RotateAround (transform.position, Vector3.forward, 180f / secondsPerDay * Time.deltaTime);
			sun.LightColor = new Color (sun.LightColor.r + 1f / secondsPerDay * Time.deltaTime,
				                            sun.LightColor.g - 1f / secondsPerDay * Time.deltaTime,
				                            sun.LightColor.b - 1f / secondsPerDay * Time.deltaTime);
		}

		// register turn as complete
		if (Time.time - startTime > secondsPerDay)
		{
			CompleteTurn();
		}
	}
}
