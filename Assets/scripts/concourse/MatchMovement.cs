using UnityEngine;
using System.Collections;

public class MatchMovement : MonoBehaviour {

	public GameObject toMatch;
	
	// Update is called once per frame
	void Update () {
		transform.position = toMatch.transform.position;
	}
}
