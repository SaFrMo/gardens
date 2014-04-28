using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	/// <summary>
	/// Static reference to the player.
	/// </summary>
	public static GameObject PLAYER = null;
	public static GameObject GAME_MANAGER;
	public static GUISkin GUI_SKIN;
	public static string GAME_NAME;
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

		if (GUI_SKIN == null) { GUI_SKIN = gameSkin; }
		GAME_NAME = gameName;
	}
}
