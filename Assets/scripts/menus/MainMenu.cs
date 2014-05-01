﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour {

	public float boxWidth = 500f;
	public List<GameObject> contracts = new List<GameObject>();
	
	private enum Menu
	{
		Main,
		NewGame,
		ContractSelection
	}
	private Menu currentState = Menu.Main;

	// main menu
	private void MainMenuFunction() {
		GUILayout.Box (GameManager.GAME_NAME);
		if (GUILayout.Button ("New Game")) { currentState = Menu.NewGame; }
	}

	// new game
	private void NewGameFunction() {
		currentState = Menu.ContractSelection;
	}

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

	private void OnGUI ()
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

		case Menu.NewGame:
			NewGameFunction();
			break;

		case Menu.ContractSelection:
			ContractSelection();
			break;


		};
		


		GUILayout.EndArea();
	}
}