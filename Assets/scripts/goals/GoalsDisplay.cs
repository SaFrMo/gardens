using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GoalsDisplay : MonoBehaviour {

	public static Goal[] allGoalsArray;
	private bool showGoals = false;
	public KeyCode toggleDisplay = KeyCode.Tab;

	// store all local goals in a private array
	void Start ()
	{
		allGoalsArray = GetComponents<Goal>();
	}

	// toggle key
	void Update ()
	{
		if (Input.GetKeyDown(toggleDisplay))
		{
			foreach (Goal g in allGoalsArray)
				g.CheckGoal();
			showGoals = !showGoals;
		}
	}

	private Vector2 scrollPos = Vector2.zero;
	void OnGUI ()
	{
		GUI.skin = GameManager.GUI_SKIN;
		if (showGoals)
		{
			GUILayout.BeginArea (new Rect (GameManager.SPACER, 
			                               Screen.height * .66f, 
			                               Screen.width * .66f,
			                               Screen.height * .33f - GameManager.SPACER));
			scrollPos = GUILayout.BeginScrollView (scrollPos, GameManager.GUI_SKIN.customStyles[0]);
			foreach (Goal g in allGoalsArray)
			{
				GUILayout.Box (string.Format ("{0} {1}", (g.complete ? "[complete] " : "[incomplete] "), g.description));
			}
			GUILayout.EndScrollView();
			GUILayout.EndArea();

		}
	}
}
