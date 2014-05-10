using UnityEngine;
using System.Collections;

public class Autosave : MonoBehaviour {

	public static byte[] autosave;

	void Start ()
	{
		DontDestroyOnLoad (gameObject);
	}
}
