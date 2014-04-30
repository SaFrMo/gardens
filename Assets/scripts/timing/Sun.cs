using UnityEngine;
using System.Collections;

public class Sun : MonoBehaviour {

	private Light2D sun;

	public float secondsPerDay = 180f;

	private float startTime;
	
	private void Start () {
		sun = Light2D.Create (new Vector2 (transform.position.x + .5f, transform.position.y),
		                      new Color (1, 1, 1, 1f), .1f);
		startTime = Time.time;
	}

	private void CompleteTurn()
	{
		GameManager.PLAYER.GetComponent<WASDMovement>().CurrentType = WASDMovement.MovementType.TurnDone;
	}

	private void Update ()
	{
		sun.transform.RotateAround (transform.position, Vector3.forward, 180f / secondsPerDay * Time.deltaTime);
		sun.LightColor = new Color (sun.LightColor.r + 1f / secondsPerDay * Time.deltaTime,
		                            sun.LightColor.g - 1f / secondsPerDay * Time.deltaTime,
		                            sun.LightColor.b - 1f / secondsPerDay * Time.deltaTime);
		if (Time.time - startTime > secondsPerDay)
		{
			CompleteTurn();
		}
	}
}
