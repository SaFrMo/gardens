using UnityEngine;
using System.Collections;

public class DGSLogo : MonoBehaviour {

	public Texture logo;
	public string nextLevelName = "mainMenu";

	private void Start ()
	{
		Invoke ("GoToNextLevel", 3f);
	}

	private void GoToNextLevel ()
	{
		Application.LoadLevel(nextLevelName);
	}

	private void OnGUI ()
	{
		GUI.DrawTexture (new Rect (Screen.width / 2 - logo.width / 2,
		                           Screen.height / 2 - logo.height / 2,
		                           logo.width / 2,
		                           logo.height), logo);
	}
}
