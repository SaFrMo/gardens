using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OpeningMovie : MonoBehaviour {

	// resets on every new slide, counts down to auto slide change
	Timer t;
	// slides through which to scroll
	public List<string> subtitles = new List<string>();
	private int currentTitle = 0;

	private void NextSlide (float time = 6f)
	{
		// go to next slide
		try 
		{ 
			currentTitle++; 
			print (subtitles[currentTitle]);
		}
		// load the concourse if there are no more subtitles
		catch { Application.LoadLevel ("concourse"); }
		// reset timer
		t = new Timer (time);
	}

	private void Start ()
	{
		//transform.position = Camera.main.ViewportToWorldPoint (new Vector3 (0.5f, 0.75f, 1));
		t = new Timer (4f);
	}

	private void Update ()
	{
		// automatically go to the next slide
		if (t.RunTimer())
		{
			NextSlide();
		}
		// allows skipping ahead for fast readers
		if (Input.GetMouseButtonDown (0))
		{
			NextSlide();
		}
	}

	private float width = 400f;
	public GUISkin skin;
	private void OnGUI ()
	{
		GUI.skin = skin;
		GUI.Box (new Rect (Screen.width / 2 - width / 2,
		                   Screen.height * .75f,
		                   width,
		                   Screen.height * .25f - GameManager.SPACER), subtitles[currentTitle]);

	}
}
