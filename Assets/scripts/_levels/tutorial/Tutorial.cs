using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {

	public static bool tutorialOn = false;

	// tutorial navigation aid
	private Step currentStep = Step.Start;
	private enum Step
	{
		Start,
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
				TutorialBox("[A] and [D] to move horizontally.", Step.Jumping);
				break;

			case Step.Jumping:
				TutorialBox ("[SPACE] to jump. You can jump as many times as you like, though it's not the most efficient way to get around.", Step.Swinging);
				break;

			case Step.Swinging:
				TutorialBox ("[RIGHT MOUSE CLICK] while jumping to swing.", Step.BreakingTether);
				break;

			case Step.BreakingTether:
				TutorialBox ("Hitting a solid object or pressing [LEFT CTRL] or [SPACE] will break the tether.", Step.LengthenShorten);
				break;

			case Step.LengthenShorten:
				TutorialBox ("[W] and [S] lengthen or shorten the tether.", Step.Gardening);
				break;

			case Step.Gardening:
				TutorialBox ("Land on a planter (gray box) and press [Q] to bring up the catalog for the planter.", Step.Buying);
				if (Catalog.TUT_SHOW_PLANT_CATALOG) { currentStep = Step.Buying; }
				break;

			case Step.Buying:
				TutorialBox ("Press [R] to switch between plant information views.", Step.Zooming);
				break;

			case Step.Zooming:
				TutorialBox ("Use the mouse wheel to zoom in and out.", Step.Start);
				break;





			}
		}
	}
}
