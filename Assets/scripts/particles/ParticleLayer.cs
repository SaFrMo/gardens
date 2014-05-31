using UnityEngine;
using System.Collections;

public class ParticleLayer : MonoBehaviour {

	// Use this for initialization
	void Start () {

		GetComponent<ParticleRenderer>().sortingLayerName = "Particles";
	}
}
