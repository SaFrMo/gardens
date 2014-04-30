using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public float boxWidth = 500f;
	
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

	private void ContractSelection () {

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
