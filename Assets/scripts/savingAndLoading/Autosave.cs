using UnityEngine;
using System.Collections;
using System.IO;

public class Autosave : MonoBehaviour {

	public static byte[] autosave;
	public static string autoPath;

	public static void SaveNow ()
	{
		autosave = LevelSerializer.SerializeLevel (false, GameManager.GAME_MANAGER.GetComponent<UniqueIdentifier>().Id);
		File.WriteAllBytes (autoPath, autosave);
	}

	public static void LoadNow ()
	{
		autosave = File.ReadAllBytes (Autosave.autoPath);
	}

	void Start ()
	{
		DontDestroyOnLoad (gameObject);
		autoPath = Application.persistentDataPath + "/auto.gdn";
	}
}
