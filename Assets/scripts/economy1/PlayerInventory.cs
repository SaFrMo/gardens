using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[SerializeAll]
public class PlayerInventory : MonoBehaviour {


	public GameObject notificationPrefab;

	private int startingDollars;

	// TODO: adjust for difficulty
	private int _dollars = 50;
	public int Dollars {
		get { return _dollars; }
		set { 
			MoneyChange (value - _dollars);
			_dollars = value; 
		}
	}

	// floating "you gained/lost some money" notification
	private void MoneyChange (int amount) {
		if (amount != 0)
		{
			GameObject changeAmount = Instantiate (notificationPrefab) as GameObject;
			changeAmount.GetComponent<MoneyChangeNotification>().SetAmount(amount);
		}
	}

	public List<Object> inventory = new List<Object>();
	// inbox with the default email
	// TODO: rewrite this
	public List<Email> inbox = new List<Email>();

	private void Start () { startingDollars = _dollars; }
	public void ResetMoney () { _dollars = startingDollars; }

	// Inventory display
	// ====================
	private void OnGUI () {
		GUI.skin = GameManager.GUI_SKIN;
		// TODO: Replace; this is a placeholder
		GUI.Box (new Rect (0, 0, 100f, 100f), "$" + Dollars.ToString());
	}
}
