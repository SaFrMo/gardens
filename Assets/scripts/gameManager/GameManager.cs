using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	/// <summary>
	/// Static reference to the player.
	/// </summary>
	public static GameObject PLAYER = null;
	public static GameObject GAME_MANAGER;
	public static GUISkin GUI_SKIN;
	public static string GAME_NAME = "The Green Steel Canyons";
	public static float SPACER = 10f;
	public string gameName;
	public GUISkin gameSkin;


	void Start () {
		// save the reference to the player at the start of the scene
		if (PLAYER == null) {
			PLAYER = GameObject.Find ("Player");
			if (PLAYER == null) {
				Debug.Log ("Couldn't find \"Player!\" Please add a GameObject named \"Player\" to the scene."); 
			}
		}

		if (GUI_SKIN == null)
			GUI_SKIN = gameSkin;

		// ensure there's only one game manager
		if (GAME_MANAGER == null)
		{
			GAME_MANAGER = gameObject;
			GameObject.DontDestroyOnLoad (GAME_MANAGER);
		}
		else
		{
			Destroy (gameObject);
		}
	}


}
