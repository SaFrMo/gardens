using UnityEngine;
using System.Collections;

public class SlowTime : MonoBehaviour {

	//the factor used to slow down time
	private float slowFactor = 10f;
	//the new time scale
	private float newTimeScale;

	// timer for time slow activation
	Timer t = null;
	
	// Called when this script starts
	void Start()
	{
		//calculate the new time scale
		newTimeScale = Time.timeScale/slowFactor;
	}

	private void SlowDown () {
		//assign the 'newTimeScale' to the current 'timeScale'
		Time.timeScale = newTimeScale;
		//proportionally reduce the 'fixedDeltaTime', so that the Rigidbody simulation can react correctly
		Time.fixedDeltaTime = Time.fixedDeltaTime/slowFactor; //removing this line makes the Rigidbody movement appear to be "choppy"
		//The maximum amount of time of a single frame
		Time.maximumDeltaTime = Time.maximumDeltaTime/slowFactor;//removing this line makes the physics simulation behave differently when the time is set to slow motion


	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButtonDown(1))
		{
			t = new Timer(.1f);
		}

		else if (Input.GetMouseButton(1) && t.RunTimer()) {
			//if the game is running normally
			if (Time.timeScale == 1.0f)
			{
				SlowDown();
				//StartCoroutine(SlowDown());
			}
		}

		else if (Input.GetMouseButtonUp(1)) {
			//reset the values
			if (Time.timeScale != 1.0f) {
				Time.timeScale = 1.0f;
				Time.fixedDeltaTime = Time.fixedDeltaTime*slowFactor;
				Time.maximumDeltaTime = Time.maximumDeltaTime*slowFactor;
				//StopCoroutine("SlowDown");
			}
			t = null;
		}
	}
}
