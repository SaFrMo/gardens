using UnityEngine;
using System.Collections;

[SerializeAll]
public class Alpha : MonoBehaviour {

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) 
			Application.Quit();
	}
}
