using UnityEngine;
using System.Collections;

public class Sun : MonoBehaviour {

	private Light2D sun;

	public float secondsPerDay = 180f;

	private float startTime;
	
	private void Start () {

		Reset ();
	}

	private bool turnComplete = false;
	private void CompleteTurn()
	{
		GameManager.PLAYER.GetComponent<WASDMovement>().CurrentType = WASDMovement.MovementType.TurnDone;
		turnComplete = true;
		Destroy (sun);
	}

	private void TurnCompleteWindow ()
	{
		float boxSize = 400f;
		GUILayout.BeginArea (new Rect (Screen.width / 2 - boxSize / 2,
		                               Screen.height / 2 - boxSize / 2,
		                               boxSize,
		                               boxSize));
		GUILayout.Box ("LEVEL NAME");
		// level stats
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

	private void Reset ()
	{
		sun = Light2D.Create (new Vector2 (transform.position.x + .5f, transform.position.y),
		                      new Color (1, 1, 1, 1f), .1f);
		startTime = Time.time;
		turnComplete = false;
		GameManager.PLAYER.GetComponent<Respawn>().RespawnPlayer();
		/*
		sun.transform.position = new Vector2 (transform.position.x + .5f, transform.position.y);
		sun.LightColor = new Color (1, 1, 1, 1f);
		sun.LightEnabled = true;
*/
	}

	private void OnGUI ()
	{
		if (turnComplete)
		{
			TurnCompleteWindow();
		}

	}

	private void Update ()
	{
		if (sun != null)
		{
			sun.transform.RotateAround (transform.position, Vector3.forward, 180f / secondsPerDay * Time.deltaTime);
			sun.LightColor = new Color (sun.LightColor.r + 1f / secondsPerDay * Time.deltaTime,
				                            sun.LightColor.g - 1f / secondsPerDay * Time.deltaTime,
				                            sun.LightColor.b - 1f / secondsPerDay * Time.deltaTime);
		}
		
		if (Time.time - startTime > secondsPerDay)
		{
			CompleteTurn();
		}
	}
}
