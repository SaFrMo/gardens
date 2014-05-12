using UnityEngine;
using System.Collections;

public class Autosave : MonoBehaviour {

	public static byte[] autosave;

	public static void SaveNow ()
	{
		autosave = LevelSerializer.SerializeLevel (false, GameManager.GAME_MANAGER.GetComponent<UniqueIdentifier>().Id);
	}

	void Start ()
	{
		DontDestroyOnLoad (gameObject);
	}
}
