using UnityEngine;
using System.Collections;

public class CheckWater : MonoBehaviour {

	private bool infoDisplayed = false;
	public KeyCode checkKey = KeyCode.R;

	private void Update ()
	{
		if (Input.GetKeyDown (checkKey))
		{
			infoDisplayed = !infoDisplayed;
			foreach (GrowingPlant gp in Planter.ALL_PLANTS)
			{
				gp.showWaterLevels = (infoDisplayed ? true : false);
			}
		}
	}
}
