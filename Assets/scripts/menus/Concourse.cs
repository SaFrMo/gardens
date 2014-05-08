﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Concourse : MonoBehaviour {

	// menu position
	private enum Place
	{
		Main,
		Unlockables,
		Contracts
	}
	Place current = Place.Main;

	private List<Unlockable> unlockables = new List<Unlockable>();
	private List<GrowingPlant> growingPlants = new List<GrowingPlant>();
	private List<Contract> contracts = new List<Contract>();
	
	private void RefreshUnlockables ()
	{

		// refreshes unlockable plants
		growingPlants.Clear ();
		List<GameObject> goList = Catalog.RefreshPlantsList();
		foreach (GameObject go in goList)
		{
			//unlockables.Add (go.GetComponent<Unlockable>());
			growingPlants.Add (go.GetComponent<Unlockable>() as GrowingPlant);
		}

		// refreshes contracts
		contracts.Clear();
		goList = Catalog.RefreshContractsList();
		foreach (GameObject go in goList)
		{
			contracts.Add (go.GetComponent<Unlockable>() as Contract);
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
		// TODO: make this look nicer
	{
		float side = Screen.width * .75f;
		GUILayout.BeginArea (new Rect (0, 0,
		                               side, 
		                               side));
		GUILayout.Box ("Welcome, [error404]!");

		switch (current)
		{
		case Place.Main:
			if (GUILayout.Button ("Unlockable items")) 
			{
				RefreshUnlockables();
				current = Place.Unlockables;
			}
			if (GUILayout.Button ("Contracts"))
			{
				RefreshUnlockables();
				current = Place.Contracts;
			}



			// TODO: quit confirmation
			if (GUILayout.Button ("Quit to Main Menu")) { Application.LoadLevel ("mainMenu"); }
			if (GUILayout.Button ("Quit game")) { Application.Quit(); }
			break;

		// unlockable plants
		case Place.Unlockables:
			foreach (Unlockable u in unlockables)
			{
				UnlockableCell (u);
			}
			if (GUILayout.Button ("Back to Main Menu")) { current = Place.Main; }
			break;

		// contracts
		case Place.Contracts:
			// TODO: horizontal rows
			foreach (Contract c in contracts)
			{
				// creates content with the city sprite and name
				// TODO: include description somehow
				//GUIContent gC = new GUIContent (c.CitySprite, c.CityName);
				if (GUILayout.Button (c.CityName))
				{
					// load relevant scene
					Application.LoadLevel (c.LevelName);
				}
			}
			if (GUILayout.Button ("Back to Main Menu")) { current = Place.Main; }
			break;




		};

		GUILayout.EndArea();
	}
}
