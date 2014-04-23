using UnityEngine;
using System.Collections;

public class SlowTime : MonoBehaviour {

	//the factor used to slow down time
	private float slowFactor = 10f;
	//the new time scale
	private float newTimeScale;
	
	// Called when this script starts
	void Start()
	{
		//calculate the new time scale
		newTimeScale = Time.timeScale/slowFactor;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//when "Fire1" is pressed
		if (Input.GetMouseButtonDown(1))
		{
			//if the game is running normally
			if (Time.timeScale == 1.0f)
			{
				//assign the 'newTimeScale' to the current 'timeScale'
				Time.timeScale = newTimeScale;
				//proportionally reduce the 'fixedDeltaTime', so that the Rigidbody simulation can react correctly
				Time.fixedDeltaTime = Time.fixedDeltaTime/slowFactor; //removing this line makes the Rigidbody movement appear to be "choppy"
				//The maximum amount of time of a single frame
				Time.maximumDeltaTime = Time.maximumDeltaTime/slowFactor;//removing this line makes the physics simulation behave differently when the time is set to slow motion
			}
		}

		else if (Input.GetMouseButtonUp(1)) {
			//reset the values
			Time.timeScale = 1.0f;
			Time.fixedDeltaTime = Time.fixedDeltaTime*slowFactor;
			Time.maximumDeltaTime = Time.maximumDeltaTime*slowFactor;
		}
	}
}
