using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	/// <summary>
	/// Static reference to the player.
	/// </summary>
	public static GameObject PLAYER = null;

	void Start () {
		// save the reference to the player at the start of the scene
		if (PLAYER == null) {
			PLAYER = GameObject.Find ("Player");
			if (PLAYER == null) {
				Debug.Log ("Couldn't find \"Player!\" Please add a GameObject named \"Player\" to the scene."); 
			}
		}
	}
}
