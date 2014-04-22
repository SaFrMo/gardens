using UnityEngine;
using System.Collections;

public class PlacePlanter : MonoBehaviour {

	public GameObject planterPrefab;
	private GameObject planterPlacer = null;
	public static int CUSTOM_PLANTER_COUNT = 0;

	private Vector2 selectedPosition;

	public void SelectPlanterLocation () {
		selectedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		// converts position to Vector2 and snaps to a 1x1 grid
		selectedPosition = new Vector2 (Mathf.Round(selectedPosition.x),
		                                Mathf.Round(selectedPosition.y));

		if (planterPlacer == null) {
			planterPlacer = GameObject.Instantiate (planterPrefab) as GameObject;
		}

		planterPlacer.transform.position = selectedPosition;
	}

	public void CreatePlanter () {
		// only activate when the new planter would not interfere with an old one
		// TODO: use a grid system?

		GameObject planter = GameObject.Instantiate (planterPrefab) as GameObject;
		planter.transform.position = selectedPosition;

		// unique name for each custom planter
		planter.gameObject.name = string.Format ("Planter {0}", PlacePlanter.CUSTOM_PLANTER_COUNT);
		CUSTOM_PLANTER_COUNT++;

		// clear planter placer
		Destroy (planterPlacer);
	}

	protected void Update () {
		if (Input.GetMouseButton(0)) {
			SelectPlanterLocation();
		}
		if (Input.GetMouseButtonUp(0)) {
			CreatePlanter();
		}
	}
}
