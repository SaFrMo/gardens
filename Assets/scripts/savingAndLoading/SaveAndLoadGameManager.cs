using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SaveAndLoadGameManager : MonoBehaviour {
	
	void Start () {
		// load auto-save on start
		/*
		List<LevelSerializer.SaveEntry> sL = LevelSerializer.SavedGames[LevelSerializer.PlayerName];
		LevelSerializer.SaveEntry s = sL[sL.Count - 1];
		LevelSerializer.LoadNow (s.Data);
		*/
		LevelSerializer.LoadNow (Autosave.autosave);
		GameManager.InitializeLevel();
	}
}
