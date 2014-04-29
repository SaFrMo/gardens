using UnityEngine;
using System.Collections;

public class Sun : MonoBehaviour {

	private Light2D sun;

	public float secondsPerDay = 180f;
	
	private void Start () {
		sun = Light2D.Create (new Vector2 (transform.position.x + .5f, transform.position.y),
		                      new Color (1, 1, 1, 1f), .1f);
	}

	private void Update ()
	{
		sun.transform.RotateAround (transform.position, Vector3.forward, 180f / secondsPerDay * Time.deltaTime);
	}
}
