using UnityEngine;
using System.Collections;

public class MoneyChangeNotification : MonoBehaviour {

	private int _amount;
	private float side = 200f;
	private float startTime;

	public void SetAmount(int amount) {
		_amount = amount;
		startTime = Time.time;
	}



	void OnGUI() {
		GUI.Box (new Rect (Screen.width / 2 - side / 2,
		                   Screen.height / 2 - side / 2 - (Time.time - startTime) * 30f,
		                   side,
		                   side), _amount.ToString ());
	}

	void Start () {
		Destroy (gameObject, 1f);
	}
}
