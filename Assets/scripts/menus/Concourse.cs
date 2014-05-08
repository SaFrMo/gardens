using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Concourse : MonoBehaviour {

	// menu position
	private enum Place
	{
		Main,
		Unlockables
	}
	Place current = Place.Main;

	private List<Unlockable> unlockables = new List<Unlockable>();

	// TODO: sort by unlockable type
	private void RefreshUnlockables ()
	{
		unlockables.Clear ();

		// refreshes unlockable plants
		List<GameObject> goList = Catalog.RefreshList();
		foreach (GameObject go in goList)
		{
			unlockables.Add (go.GetComponent<Unlockable>());
		}
	}

	private void Start ()
	{

	}

	// info window, a la Risk of Rain's unlockables window
	// TODO: lots of aesthetic work here
	private void UnlockableCell (Unlockable u)
	{
		if (u.unlocked)
		{
			GUILayout.Box (u.name);
		}
		else
		{
			GUILayout.Box ("???");
		}
	}


	private void OnGUI ()
	{
		float side = Screen.width * .75f;
		GUILayout.BeginArea (new Rect (0, 0,
		                               side, 
		                               side));
		GUILayout.Box ("Welcome, [error404]!");

		switch (current)
		{
		case Place.Main:
			if (GUILayout.Button ("View unlockable items")) 
			{
				RefreshUnlockables();
				current = Place.Unlockables;
			}
			break;

		case Place.Unlockables:
			foreach (Unlockable u in unlockables)
			{
				UnlockableCell (u);
			}
			if (GUILayout.Button ("Back to Main Menu")) { current = Place.Main; }
			break;




		};

		GUILayout.EndArea();
	}
}
