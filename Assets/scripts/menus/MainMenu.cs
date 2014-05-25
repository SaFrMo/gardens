using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class MainMenu : MonoBehaviour {

	public float boxWidth = 500f;
	public List<GameObject> contracts = new List<GameObject>();

	private void Start ()
	{
		GameManager.SetGameManager();
		string autoPath = Application.persistentDataPath + "/auto.gdn";
		FileStream s = null;
		try { s = File.OpenRead (autoPath); }
		catch { noAutoSave = true; }
		finally { if (s!= null) s.Close(); }



		// create a new game prefab
		LevelSerializer.SaveGame ("new");


	}
	
	public enum Menu
	{
		Main,
		NewGame,
		ContractSelection,
		CitySelection,
		Continue,
		Controls,
		Tutorial
	}
	public static Menu currentState = Menu.Main;

	// main menu
	private void MainMenuFunction() {
		GUILayout.Box (GameManager.GAME_NAME, GameManager.GUI_SKIN.customStyles[3]);
		if (!noAutoSave) 
		{
			if (GUILayout.Button ("Continue Game", GameManager.GUI_SKIN.customStyles[2])) { currentState = Menu.Continue; }
		}
		if (GUILayout.Button ("New Game", GameManager.GUI_SKIN.customStyles[2])) { currentState = Menu.NewGame; }
		if (GUILayout.Button ("Controls", GameManager.GUI_SKIN.customStyles[2])) { currentState = Menu.Controls; }
		if (GUILayout.Button ("Tutorial", GameManager.GUI_SKIN.customStyles[2])) { currentState = Menu.Tutorial; }
		if (GUILayout.Button ("Quit Game", GameManager.GUI_SKIN.customStyles[2])) { Application.Quit(); }
	}

	// continue - load last autosave
	private void ContinueGameFunction ()
	{
		currentState = Menu.Main;
		Autosave.LoadNow(); 
		Application.LoadLevel ("concourse");
		
	}

	private bool noAutoSave = false;

	// new game
	private void NewGameFunction() {
		// TODO: overwrite progress warning dialog?
		// wipes last autosave and starts a new game
		// reset plant status
		// add new game email
		GameManager.GAME_MANAGER.GetComponent<PlayerInventory>().ResetMoney();
		GameManager.GAME_MANAGER.GetComponent<PlayerInventory>().inbox.Add (AllEmails.initialEmail);
		AllContracts.ContractsReset();
		GameManager.GAME_MANAGER.GetComponent<Catalog>().ResetPlantsList();
		Autosave.SaveNow();
		currentState = Menu.Main;
		Application.LoadLevel ("openingMovie");
	}
	/*
	// contract selection
	private void ContractCell (Contract c)
	{
		GUILayout.BeginVertical();
		GUIContent content = new GUIContent(c.CitySprite, c.CityDescription);
		if (GUILayout.Button (c.CityName))
		{
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
			ContractCell (contracts[i].GetComponent<Contract>());
		}
		GUILayout.EndHorizontal();
		GUILayout.EndScrollView();
	}
	*/



	// show controls
	public Dictionary<string, string> controls = new Dictionary<string, string>() {
		{ "A-D", "Horizontal movement" },
		{ "Space", "Jump" },
		{ "", "" }
	};
	private void ControlsDisplay()
	{
		foreach (KeyValuePair<string, string> kv in controls)
		{
			GUILayout.BeginHorizontal();
			GUILayout.Box (kv.Key);
			GUILayout.Box (kv.Value);
			GUILayout.EndHorizontal();
		}
		if (GUILayout.Button ("[Back]")) { currentState = Menu.Main; }
	}

	private void OnGUI ()
	{
		if (currentState != Menu.Tutorial)
		{
			GUILayout.BeginArea (new Rect (Screen.width / 2 - boxWidth / 2,
			                               Screen.height / 2 - boxWidth / 2,
			                               boxWidth,
			                               boxWidth));
			switch (currentState)
			{

			case Menu.Main:
				MainMenuFunction();
				break;

			case Menu.Continue:
				ContinueGameFunction();
				break;

			case Menu.NewGame:
				NewGameFunction();
				break;

			case Menu.Controls:
				ControlsDisplay();
				break;

				/*
			case Menu.ContractSelection:
				ContractSelection();
				break;
	*/

			};
			


			GUILayout.EndArea();
		}
		else { if (!Tutorial.tutorialOn) Tutorial.tutorialOn = true; }
	}
}
