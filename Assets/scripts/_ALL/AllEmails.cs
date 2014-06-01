using UnityEngine;
using System.Collections;

public class AllEmails {

	public static Email chicago1_payBill = new Email(
		GameManager.PLAYER_EMAIL, "dpw@chi.us",
		"We're pleased to see that your efforts to maintain a self-sufficient vertical garden have been successful! We have provided a vertical gardening " +
		"permit and wish you the best of luck in future endeavors.\n\nNow, please remit payment of $250 to the city of Chicago and your startup " +
		"loan will be taken care of. Thank you, and have a pleasant day!", "Permit awarded!");

	public static Email chicago2_fireHouse = new Email(
		GameManager.PLAYER_EMAIL, "cfd@chi.us",
		"The Chicago Fire Department, after another year of having to hold bake sales to buy a new fire engine (we have a few muffins left over - " +
		"adjusted to match the fire engine's scale, they're $4000 each), is very interested in your ability to raise a profit from vertical gardens.\n\nIf you'd be willing to " +
		"work for a percentage of the money you raise, we would let you use the fire house, prime, visible real estate, for your next garden.\n\n" +
		"We can't offer any money beyond revenue share right now, but you'd get plenty of exposure, and isn't that the most important " +
		"thing?\n\nCordially,\n-CFD", "Chicago Fire Department Gardens");
}
