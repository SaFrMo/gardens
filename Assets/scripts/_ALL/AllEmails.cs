using UnityEngine;
using System.Collections;

public class AllEmails {

	public static Email chicago1_payBill = new Email(
		GameManager.PLAYER_EMAIL, "dpw@chi.us",
		"We're pleased to see that your efforts to maintain a self-sufficient vertical garden have been successful! We have provided a vertical gardening " +
		"permit and wish you the best of luck in future endeavors.\n\nNow, please remit payment of $250 to the city of Chicago and your startup " +
		"loan will be taken care of. Thank you, and have a pleasant day!", "Permit awarded!");
}
