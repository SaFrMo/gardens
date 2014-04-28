using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {

	// tutorial navigation aid
	private Step currentStep = Step.Start;
	private enum Step
	{
		Start,
		Jumping,
		Swinging,
		BreakingTether,
		Gardening,
		Buying
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
		GUILayout.Box (text, GameManager.GUI_SKIN.customStyles[1]);

		if (GUILayout.Button ("Next..."))
		{
			currentStep = nextStep;
		}

		GUILayout.EndArea();

	}

	private void OnGUI () {
		switch (currentStep)
		{

		case Step.Start:
			TutorialBox("[A] and [D] to move horizontally.", Step.Jumping);
			break;

		case Step.Jumping:
			TutorialBox ("[SPACE] to jump.", Step.Swinging);
			break;

		case Step.Swinging:
			TutorialBox ("[RIGHT MOUSE CLICK] while jumping to swing.", Step.BreakingTether);
			break;

		case Step.BreakingTether:
			TutorialBox ("Hitting a solid object or pressing [LEFT CTRL] or [SPACE] will break the tether. Lean with [A] and [D].", Step.Gardening);
			break;

		case Step.Gardening:
			TutorialBox ("Land on a planter (gray box) and press [Q] to bring up the catalog for the planter.", Step.Buying);
			if (Catalog.TUT_SHOW_PLANT_CATALOG) { currentStep = Step.Buying; }
			break;

		case Step.Buying:
			TutorialBox ("Buy a plant and you'll see its water level when you land on the planter. Press [R] to toggle seeing all water levels from all planters.", Step.Start);
			break;





		}
	}
}
