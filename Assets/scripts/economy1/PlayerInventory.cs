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
	public List<Email> inbox = new List<Email>() {
		new Email ("rem22@chi.us", "dpw@chi.us", "Welcome and thank you for your interest in serving the city of Chicago." +
			"\n\nThe members of the Chicago City Council are awaiting the results of your vertical gardens experiment with great interest." +
			"We applaud your effort to make this a cleaner and more productive city, and we are pleased to collaborate with you on a project " +
			"of such import.\n\nPlease remember that, due to the economic situation which the city of Chicago is currently facing, we cannot offer" +
			" any startup monies as a grant - your fee should be considered a loan and, in order to improve our understanding of economic viability" +
		           " of the vertical gardens project, we expect it to be paid back in full.\n\nHowever, given the idealistic nature of your task, " +
		           "we are only charging 8% interest (compound) instead of 9%, the normal rate. We view this as a reasonable sacrifice to make to " +
		           "support such a noble goal.\n\nThank you, good luck, and remember to pay your bills!\n\nThe Alderperson and City Council of Chicago",
		           "Congratulations and startup monies")
	};

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
