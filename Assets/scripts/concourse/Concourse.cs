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
	
	private List<GrowingPlant> growingPlants = new List<GrowingPlant>();
	private List<Contract> contracts = new List<Contract>();

	// contract selection
	private void ContractCell (Contract c)
	{
		GUILayout.BeginVertical();
		GUIContent content = new GUIContent(c.CityDescription);

		// save catalog status and load a contract
		if (GUILayout.Button (c.CityName))
		{
			//LevelSerializer.SaveGame ("latest");
			Autosave.SaveNow();
			Application.LoadLevel (c.LevelName);
		}


		GUILayout.Box (content);
		GUILayout.EndVertical();
	}
	
	Vector2 scrollPos;
	public int contractsPerRow = 3;
	
	private void ContractSelection () {
		//scrollPos = GUILayout.BeginScrollView(scrollPos, GUIStyle.none);

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

		//GUILayout.EndScrollView();
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
			GUILayout.Box (u.name, GameManager.GUI_SKIN.customStyles[3]);
		}
		else
		{
			GUILayout.BeginHorizontal();
			GUILayout.Box (u.name + " [locked]", GameManager.GUI_SKIN.customStyles[3]);
			if (GUILayout.Button (string.Format ("Unlock {0} ({1})", u.name, u.unlockCost.ToString()), GameManager.GUI_SKIN.customStyles[2]) &&
			    GameManager.GAME_MANAGER.GetComponent<PlayerInventory>().Dollars >= u.unlockCost)
			{
				GameManager.GAME_MANAGER.GetComponent<PlayerInventory>().Dollars -= u.unlockCost;

				// unlock and autosave
				u.unlocked = true;
				Autosave.SaveNow();
			}
			GUILayout.EndHorizontal();
		}
	}


	private void OnGUI ()
		// TODO: make this look nicer
	{
		GUI.skin = GameManager.GUI_SKIN;
		float side = Screen.width * .33f;
		GUILayout.BeginArea (new Rect (GameManager.SPACER,
		                               Screen.height / 4,
		                               side - GameManager.SPACER, 
		                               side), GameManager.GUI_SKIN.customStyles[3]);
		GUILayout.Box ("Planner", GameManager.GUI_SKIN.customStyles[3]);

		switch (current)
		{
		case Place.Main:

			if (GUILayout.Button ("Contracts", GameManager.GUI_SKIN.customStyles[2]))
			{
				RefreshUnlockables();
				current = Place.Contracts;
			}

			// spacer
			GUILayout.Box ("");

			// TODO: shopping window logo
			if (GUILayout.Button ("Unlockable Items", GameManager.GUI_SKIN.customStyles[2])) 
			{
				RefreshUnlockables();
				current = Place.Unlockables;
			}

			// spacer
			GUILayout.Box ("");



			// TODO: quit confirmation, autosave confirmation
			if (GUILayout.Button ("Quit to main menu", GameManager.GUI_SKIN.customStyles[2])) { 
				Autosave.SaveNow();
				Application.LoadLevel ("mainMenu"); 
			}
			if (GUILayout.Button ("Quit game", GameManager.GUI_SKIN.customStyles[2])) 
			{ 
				Autosave.SaveNow();
				Application.Quit(); 
			}
			break;

		// unlockables display
		case Place.Unlockables:

			// plants
			GUILayout.Box ("Plants", GameManager.GUI_SKIN.customStyles[3]);
			foreach (GrowingPlant gp in growingPlants)
			{
				UnlockableCell (gp);
			}
			if (GUILayout.Button ("Back to main menu", GameManager.GUI_SKIN.customStyles[2])) { current = Place.Main; }
			break;



		// contracts
		case Place.Contracts:
			ContractSelection();
			if (GUILayout.Button ("Back to main menu", GameManager.GUI_SKIN.customStyles[2])) { current = Place.Main; }
			break;




		};
		GUILayout.EndArea();
	}
}
