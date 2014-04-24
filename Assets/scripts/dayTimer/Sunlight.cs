using UnityEngine;
using System.Collections;

public class Sunlight : MonoBehaviour {

	private Light2D sun;

	private void Start () {
		sun = Light2D.Create (new Vector2 (-32, 19),
		                      new Color (1, 1, 1, .001f),
		                      500f);
	}
}
