using UnityEngine;
using System.Collections;

public class Die : MonoBehaviour {
	// Setting this to true will make the player respawn
	bool shouldRespawn = false;

	//Particle system to use for blood effects
	public ParticleSystem bloodEffect;
	//Store the instance of the blood particle system
	Object bloodInstance;
	// Use this for initialization
	void Start () {
		bloodEffect.enableEmission = false;
		bloodEffect.Stop();
	}
	private void OnCollisionEnter2D (Collision2D c) {
		// tag all "floor" tiles that should kill the player "Floor"
		if (c.gameObject.tag == "Floor") {
			KillPlayer ();
		

		}
	}
	void KillPlayer() {

		//Keep player from moving
		GetComponent<WASDMovement>().CurrentType = WASDMovement.MovementType.Dead;
		//Show blood particles
		bloodEffect.enableEmission = true;
		bloodEffect.Emit (1000);
		
		//wait a few seconds then respawn
		StartCoroutine( WaitAndRespawn());
		


	}
	IEnumerator WaitAndRespawn() {
		Debug.Log ("waiting");
		yield return new WaitForSeconds(2);
		Debug.Log ("waited");
		//setting this to true respawns the player in the next frame
		shouldRespawn = true;

	}

	// Update is called once per frame
	void Update () {
		if (shouldRespawn == true) {
			//respawn at playerStart
			GetComponent<Respawn>().RespawnPlayer();
			//make sure player dosen't keep respawning
			shouldRespawn = false;

			//make sure particles aren't emitting anymore, the simulation has reset and all particles are cleared
			bloodEffect.enableEmission = false;
			bloodEffect.Stop();
			bloodEffect.Clear(); 
		}
	}
}
