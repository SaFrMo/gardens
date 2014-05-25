using UnityEngine;
using System.Collections;

public class AllEmails {

	public static Email initialEmail = new Email("rem22@chi.us", "dpw@chi.us", "Welcome and thank you for your interest in serving the city of Chicago." +
	                                              "\n\nThe members of the Chicago City Council are awaiting the results of your vertical gardens experiment with great interest." +
	                                              "We applaud your effort to make this a cleaner and more productive city, and we are pleased to collaborate with you on a project " +
	                                              "of such import.\n\nPlease remember that, due to the economic situation which the city of Chicago is currently facing, we cannot offer" +
	                                              " any startup monies as a grant - your fee should be considered a loan and, in order to improve our understanding of economic viability" +
	                                              " of the vertical gardens project, we expect it to be paid back in full.\n\nHowever, given the idealistic nature of your task, " +
	                                              "we are only charging 8% interest (compound) instead of 9%, the normal rate. We view this as a reasonable sacrifice to make to " +
	                                              "support such a noble goal.\n\nThank you, good luck, and remember to pay your bills!\n\nThe Alderperson and City Council of Chicago",
	                                              "Congratulations and startup monies");

	public static Email chicago1_payBill = new Email(
		GameManager.PLAYER_EMAIL, "dpw@chi.us",
		"We're pleased to see that your efforts to maintain a self-sufficient vertical garden have been successful! We have provided a vertical gardening " +
		"permit and wish you the best of luck in future endeavors.\n\nNow, please remit payment of $250 to the city of Chicago and your startup " +
		"loan will be taken care of. Thank you, and have a pleasant day!", "Permit awarded!");
}
