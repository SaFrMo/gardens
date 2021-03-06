﻿using UnityEngine;
using System.Collections;

public class cameraController : MonoBehaviour {
	private float cameraDepth = -10; //store the Z position of the camera

	public GameObject player;
	// Use this for initialization
	void Start () {

		//Set camera's orthographic size to ensure that the scene displays the same on screens with different resolutions
		camera.orthographicSize = (Screen.height / 100f / 2.0f); // 100f is the PixelPerUnit that you have set on your sprite. Default is 100.
	}
	
	// Update is called once per frame
	void Update () { 
		//Update the camera to follow the player's X and Y position while maintaining the same depth
		transform.position = new Vector3(player.transform.position.x, player.transform.position.y +1, cameraDepth);

	}
}
