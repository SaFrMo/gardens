using UnityEngine;
using System.Collections;

public class MoneyChangeNotification : MonoBehaviour {

	public int amount;

	private float side = 100f;

	private float startTime;

	void Start () {
		Destroy (gameObject, 1f);
		startTime = Time.realtimeSinceStartup;
	}

	public void SetAmount (int a) {
		amount = a;
	}

	void OnGUI () {
		// TODO: Make this a label
		GUI.Box (new Rect (Screen.width / 2 - side / 2,
		                     (Screen.height / 2 - side / 2) - (Time.realtimeSinceStartup - startTime) * 30f,
		                     side,
		                     side), amount.ToString());
	}
}
