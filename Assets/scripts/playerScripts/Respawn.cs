using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour {
	//use an empty GameObject to set where the player spawns
	public GameObject spawnPoint;

	//Set this to true to make the player spawn at spawnPoint

	// Use this for initialization
	void Start () {
	
	}
	//Sets movement type back to Grounded from Dead and moves player to the spawnpoint
	public void RespawnPlayer() {
		GetComponent<WASDMovement>().CurrentType = WASDMovement.MovementType.Grounded;
		rigidbody2D.transform.position = spawnPoint.transform.position;

	}
	// Update is called once per frame
	void Update () {
	
	}
}
