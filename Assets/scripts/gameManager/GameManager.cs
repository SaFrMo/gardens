using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[SerializeAll]
public class GameManager : MonoBehaviour {

	/// <summary>
	/// Static reference to the player.
	/// </summary>
	public bool isConcourse = false;
	public static GameObject PLAYER = null;
	public static GameObject GAME_MANAGER;
	public static GUISkin GUI_SKIN;
	public static string GAME_NAME = "The Green Steel Canyons";
	public static float SPACER = 10f;
	public string gameName;
	public GUISkin gameSkin;
	public static string PLAYER_EMAIL = "rem22@chi.us";

	public static Dictionary<Unlockable, bool> unlocked = new Dictionary<Unlockable, bool>();

	public static void InitializeLevel ()
	{
		// locate the player
		try { PLAYER = GameObject.Find ("Player"); }
		catch { print ("Can't find Player"); }

		// make sure you don't have $0
		// TODO: let the player find the money on the ground
		if (GAME_MANAGER.GetComponent<PlayerInventory>().Dollars <= 0)
		{
			GAME_MANAGER.GetComponent<PlayerInventory>().Dollars += UnityEngine.Random.Range (3, 5);

		}

		print (Stats.XP + " XP");

	}

	public static void SetGameManager ()
	{
		if (GAME_MANAGER == null)
			GAME_MANAGER = GameObject.Find ("__Game Manager");
	}


	void Start () {
		GAME_MANAGER = gameObject;
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
		/*
		if (GAME_MANAGER == null)
		{
		*/
		//if (isConcourse)	

			//GameObject.DontDestroyOnLoad (GAME_MANAGER);
		//}
		/*
		else
		{
			Destroy (gameObject);
		}
		*/

		/*
		if (!isConcourse)
		{
			List<LevelSerializer.SaveEntry> sL = LevelSerializer.SavedGames[LevelSerializer.PlayerName];
			LevelSerializer.SaveEntry s = sL.Find (x => x.Name == "latest");
			LevelSerializer.LoadNow (s.Data);
			Destroy (gameObject);
		}
		*/	


	}







}
