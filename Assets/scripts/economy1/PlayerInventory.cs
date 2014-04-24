using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour {

	public GameObject notificationPrefab;

	private int _dollars = 20;
	public int Dollars {
		get { return _dollars; }
		set { 
			MoneyChange (value - _dollars);
			_dollars = value; 
		}
	}

	// floating "you gained/lost some money" notification
	private void MoneyChange (int amount) {
		GameObject changeAmount = Instantiate (notificationPrefab) as GameObject;
		notificationPrefab.GetComponent<MoneyChangeNotification>().SetAmount(amount);
	}

	public List<Object> inventory = new List<Object>();


	// Inventory display
	// ====================
	private void OnGUI () {
		// TODO: Replace; this is a placeholder
		GUI.Box (new Rect (0, 0, 100f, 100f), "$" + Dollars.ToString());
	}
}
