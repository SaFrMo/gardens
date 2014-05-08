using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Scientist1 : Conversation {

	public int reward = 50;
	public GameObject seeds;

	// IMPLEMENTATION:
	/* GetContent is the main function here. First, it resets the following values:
	 * 		playerLines: the dictionary<string, int> that contains what a player says and what index that leads to
	 * 		showContinueButton: whether or not the NPC has more to say before the player can chime in
	 * 		showPlayerLine: what the player can say to the NPC
	 * 		whereTo: -1 is a flag value that means clicking on the "next" button will simply advance the conversation by 1.
	 * 			If AllowContinue(int x) is called, -1 will be replaced by x, which will take the conversation to a different
	 * 			place than the next index.
	 * 
	 */

	// this is the string the NPC will say
	string toContent;



	// allow progression to next conversation index
	void AllowContinue () {
		showContinueButton = true;
	}

	// jump to a special index
	void AllowContinue (int where) {
		showContinueButton = true;
		whereTo = where;
	}

	void AllowPlayerLines () {
		showPlayerLine = true;
	}

	// HOW TO USE
	// 1. toContent = what the NPC will say.
	// 2a. If the player is allowed to progress to the next line without any choice, call AllowContinue() or AllowContinue(int where).
	// 2b. If the player has something to say, call AllowPlayerLines() and then create:
	// 		Dictionary<string, int> playerLines = new Dictionary<string, int>() { ... };
	// 		where each string is the player dialogue choice and each int is the corresponding index of the NPC's response
	// 3. If the NPC is to be interrupted, call Interrupt (gameObject interrupter, int lineInterrupterSays); on the relevant case.
	//		Make sure to set conversationIndex on the other character to the one you want them to start out with when you speak with them next.
	protected override void GetContent (int key, out string content, out Dictionary<string, int> playerLines) {

		interruptionOverride = false;
		playerLines = null;
		showContinueButton = false;
		showPlayerLine = false;
		whereTo = -1;

		switch (key) {

		case 0:
			toContent = "Say, I'd bet you could use some spare cash! You'd like that, wouldn't you?";
			AllowPlayerLines();
			playerLines = new Dictionary<string, int>() {
				{ "You have my curiosity!", 1 }
			};
			break;

		case 1:
			toContent = "I represent a legitimate business with some bright young folks who are so gosh-darned curious about what certain plants do to their surroundings" +
				" that they'd love to just test those plants in an off-the-record kind of way! And pay the owner of the gardens where they do the testing in cash!";
			AllowPlayerLines();
			playerLines = new Dictionary<string, int> () {
				{ "...now you have my attention!", 2 }
			};
			break;

		case 2:
			toContent = "Excellent! Just sign here, here, and here, and I'll toss you a cool $" + reward.ToString() + "!";
			AllowPlayerLines();
			playerLines = new Dictionary<string, int>() {
				{ "How could I say no? [Sign and receive bonus on planting seeds]", 99 }
			};
			break;


		case 99:
			toContent = "Pleased as a peach to meet you! Good luck, and remember: don't plant those seeds 'til I'm out of sight!";
			// TODO: fix this
			if (seeds != null && !GameManager.PLAYER.GetComponent<Catalog>().plantsList.Contains (seeds))
			{
				GameManager.PLAYER.GetComponent<Catalog>().plantsList.Add (seeds);
				print ("Seeds added!");
			}
			AllowContinue();
			break;

		case 100:
			DoneTalking();
			key = 99;
			break;


		};

		if (!showConversation)
			showPlayerLine = false;

		content = toContent;
	}
	


	
}
