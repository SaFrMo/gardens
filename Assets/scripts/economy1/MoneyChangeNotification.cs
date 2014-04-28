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
		GameManager.GUI_SKIN.customStyles[0].normal.textColor = ( _amount > 0 ? Color.green : Color.red );
		GUI.Box (new Rect (Screen.width / 2 - side / 2,
		                   Screen.height / 2 - side / 2 - (Time.time - startTime) * 30f,
		                   side,
		                   side), string.Format ("{0}${1}", (_amount > 0 ? "" : "-"), Mathf.Abs(_amount).ToString()), GameManager.GUI_SKIN.customStyles[0]);
	}

	void Start () {
		Destroy (gameObject, 1f);
	}
}
