using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlyingCarSpawner : MonoBehaviour {

	public List<GameObject> availableFlyingCars = new List<GameObject>();

	// how many seconds go by before spawning a new instance of traffic
	public float rateMin = 3f;
	public float rateMax = 5f;

	private void SpawnCar () {
		GameObject toSpawn = availableFlyingCars[UnityEngine.Random.Range (0, availableFlyingCars.Count)];
		GameObject newCar = Instantiate (toSpawn) as GameObject;
		float y = UnityEngine.Random.Range (5, 30);
		// facing right or facing left? random according to spawn time
		if ((int)Time.time % 2 == 0) {
			newCar.transform.position = new Vector2 (-100, y);
		}
		else {
			newCar.transform.position = new Vector2 (100, y);
			newCar.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 180));
		}		
	}

	private IEnumerator SpawnTimer () {
		for (;;) {
			SpawnCar();
			yield return new WaitForSeconds(UnityEngine.Random.Range (rateMin, rateMax));
		}
	}

	private void Start () {
		StartCoroutine (SpawnTimer());
	}
}
