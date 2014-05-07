using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GoalsDisplay : MonoBehaviour {

	private Goal[] allGoalsArray;
	private bool showGoals = false;
	public KeyCode toggleDisplay = KeyCode.Tab;

	void Start ()
	{
		allGoalsArray = GetComponents<Goal>();
	}

	void Update ()
	{
		if (Input.GetKeyDown(toggleDisplay))
		{
			showGoals = !showGoals;
		}
	}

	private Vector2 scrollPos = Vector2.zero;
	void OnGUI ()
	{
		if (showGoals)
		{
			GUILayout.BeginArea (new Rect (GameManager.SPACER, 
			                               Screen.height * .66f, 
			                               Screen.width * .66f,
			                               Screen.height * .33f - GameManager.SPACER));
			scrollPos = GUILayout.BeginScrollView (scrollPos, GameManager.GUI_SKIN.customStyles[1]);
			foreach (Goal g in allGoalsArray)
			{
				GUILayout.Box (string.Format ("{0} {1}", (g.complete ? "X" : "_"), g.description));
			}
			GUILayout.EndScrollView();
			GUILayout.EndArea();

		}
	}
}
