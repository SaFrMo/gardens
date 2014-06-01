using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {

	public static bool tutorialOn = false;

	// tutorial navigation aid
	private Step currentStep = Step.Start;
	private enum Step
	{
		Start,
		Moving,
		Jumping,
		Swinging,
		BreakingTether,
		LengthenShorten,
		Gardening,
		Buying,
		Zooming
	}

	// tutorial box size details
	//private float screenMidX, screenMidY;
	public float boxWidth = 200f;
	private void Start ()
	{
		//screenMidX = Screen.width / 2;
		//screenMidY = Screen.height / 2;
	}




	private void TutorialBox (string text, Step nextStep, float rX = 0, float rY = 300, float rWidth = 400f, float rHeight = 200f)
	{
		GUILayout.BeginArea (new Rect (rX, rY, rWidth, rHeight));
		GUILayout.Box (text, GameManager.GUI_SKIN.customStyles[3]);

		if (GUILayout.Button ("Next...", GameManager.GUI_SKIN.customStyles[2]))
		{
			currentStep = nextStep;
		}

		if (GUILayout.Button ("Back to Menu", GameManager.GUI_SKIN.customStyles[2])) 
		{ 
			tutorialOn = false;
			MainMenu.currentState = MainMenu.Menu.Main; 
		}

		GUILayout.EndArea();

	}

	private void OnGUI () {
		if (tutorialOn)
		{
			switch (currentStep)
			{

			case Step.Start:
				TutorialBox("Welcome to the vertical gardens! In this game, you'll create and manage gardens that line the sides of skyscrapers in major cities.", Step.Moving);
				break;

			case Step.Moving:
				TutorialBox("You'll need to be able to get around smoothly, though. Use [A] and [D] to move horizontally.", Step.Jumping);
				break;

			case Step.Jumping:
				TutorialBox ("[SPACE] to jump.", Step.Swinging);
				break;

			case Step.Swinging:
				TutorialBox ("[RIGHT MOUSE CLICK] to swing. You can only swing if you're in the air already.", Step.BreakingTether);
				break;

			case Step.BreakingTether:
				TutorialBox ("Hitting a solid object or pressing [LEFT CTRL] or [SPACE] will break the tether.", Step.LengthenShorten);
				break;

			case Step.LengthenShorten:
				TutorialBox ("[W] and [S] will lengthen or shorten the tether.", Step.Gardening);
				break;

			case Step.Gardening:
				TutorialBox ("Land on a planter box (you'll see it turn green when you do so) and press [Q] to bring up the plant catalog.", Step.Buying);
				if (Catalog.TUT_SHOW_PLANT_CATALOG) { currentStep = Step.Buying; }
				break;

			case Step.Buying:
				TutorialBox ("[NOT YET IMPLEMENTED] Press [R] to switch between plant information views.", Step.Zooming);
				break;

			case Step.Zooming:
				TutorialBox ("[NOT YET IMPLEMENTED] Use the mouse wheel to zoom in and out.", Step.Start);
				break;





			}
		}
	}
}
