using UnityEngine;
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

	// contract selection
	private void ContractCell (Contract c)
	{
		GUILayout.BeginVertical();
		GUIContent content = new GUIContent(c.CitySprite, c.CityDescription);

		// save catalog status and load a contract
		if (GUILayout.Button (c.CityName))
		{
			LevelSerializer.SaveGame ("latest");
			Application.LoadLevel (c.LevelName);
		}


		GUILayout.Box (content);
		GUILayout.EndVertical();
	}
	
	Vector2 scrollPos;
	public int contractsPerRow = 3;
	
	private void ContractSelection () {
		scrollPos = GUILayout.BeginScrollView(scrollPos, GUIStyle.none);
		GUILayout.BeginHorizontal();
		for (int i = 0; i < contracts.Count; i++)
		{
			if (i % contractsPerRow == 0)
			{
				GUILayout.EndHorizontal();
				GUILayout.BeginHorizontal();
			}
			Contract c = contracts[i].GetComponent<Contract>();
			if (c.unlocked) { ContractCell (c); }
			// TODO: show locked cell?
		}
		GUILayout.EndHorizontal();
		GUILayout.EndScrollView();
	}
	
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
			GUILayout.BeginHorizontal();
			GUILayout.Box (u.name + " [locked]");
			// TODO: Start here! unlocking system
			if (GUILayout.Button (string.Format ("Unlock ({0})", u.unlockCost.ToString())) &&
			    GameManager.GAME_MANAGER.GetComponent<PlayerInventory>().Dollars >= u.unlockCost)
			{
				GameManager.GAME_MANAGER.GetComponent<PlayerInventory>().Dollars -= u.unlockCost;
				u.unlocked = true;
			}
			GUILayout.EndHorizontal();
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

		// unlockables display
		case Place.Unlockables:
			/*
			foreach (Unlockable u in unlockables)
			{
				UnlockableCell (u);
			}
			*/

			// plants
			GUILayout.Box ("Plants");
			foreach (GrowingPlant gp in growingPlants)
			{
				UnlockableCell (gp);
			}
			if (GUILayout.Button ("Back to Main Menu")) { current = Place.Main; }
			break;



		// contracts
		case Place.Contracts:
			ContractSelection();
			if (GUILayout.Button ("Back to Main Menu")) { current = Place.Main; }
			break;




		};

		GUILayout.EndArea();
	}
}
