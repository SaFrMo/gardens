using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour {
	// set the spawn point as the place the player first enters the scene
	private Vector2 spawnPoint;

	//Set this to true to make the player spawn at spawnPoint

	// Use this for initialization
	void Start () {
		spawnPoint = rigidbody2D.transform.position;
	}
	//Sets movement type back to Grounded from Dead and moves player to the spawnpoint
	public void RespawnPlayer() {
		GetComponent<WASDMovement>().CurrentType = WASDMovement.MovementType.Grounded;
		rigidbody2D.transform.position = spawnPoint;

	}
	// Update is called once per frame
	void Update () {
	
	}
}
