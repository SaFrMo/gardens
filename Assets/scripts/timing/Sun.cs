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
	
	private void Start () {
		Reset ();
	}

	// turn finishing steps
	private bool turnComplete = false;
	private void CompleteTurn()
	{
		// freeze player
		GameManager.PLAYER.GetComponent<WASDMovement>().CurrentType = WASDMovement.MovementType.TurnDone;
		turnComplete = true;
		// remove the sun timer
		Destroy (sun);
		// save player's finishing money
		endMoney = GameManager.PLAYER.GetComponent<PlayerInventory>().Dollars;
	}

	// show this turn's stats
	// TODO: make this look nicer
	public static float income;
	private bool checkDone = false;
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
		}
			

		// navigation from this menu
		GUILayout.BeginHorizontal();
		if (GUILayout.Button ("Back to base..."))
		{
			print ("Going back to base!");
		}
		if (GUILayout.Button ("Skip to next day..."))
		{
			Reset ();
		}
		//sun.LightEnabled = false;
		GUILayout.EndHorizontal();
		GUILayout.EndArea();
	}

	private void OnGUI ()
	{
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
		GameManager.PLAYER.GetComponent<Respawn>().RespawnPlayer();
		// save player's starting money
		startMoney = GameManager.PLAYER.GetComponent<PlayerInventory>().Dollars;
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
