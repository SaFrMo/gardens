using UnityEngine;
using System.Collections;

public class PlacePlanter : MonoBehaviour {

	public KeyCode cancelKey = KeyCode.LeftShift;

	public GameObject planterPrefab;
	public GameObject planterPlacerPrefab;
	private GameObject planterPlacer = null;
	public static int CUSTOM_PLANTER_COUNT = 0;

	private Vector2 selectedPosition;

	public void SelectPlanterLocation () {
		selectedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		// converts position to Vector2 and snaps to a 1x1 grid
		selectedPosition = new Vector2 (Mathf.Round(selectedPosition.x),
		                                Mathf.Round(selectedPosition.y));

		// instantiates an object to show where the planter will go
		if (planterPlacer == null) {
			planterPlacer = GameObject.Instantiate (planterPlacerPrefab) as GameObject;
		}

		planterPlacer.transform.position = selectedPosition;
	}

	public void CreatePlanter () {
		// only activate when the new planter would not interfere with an old one
		// TODO: Add a construction delay and the ability to undo the last placement

		GameObject planter = GameObject.Instantiate (planterPrefab) as GameObject;
		planter.transform.position = selectedPosition;

		// unique name for each custom planter
		planter.gameObject.name = string.Format ("Planter {0}", PlacePlanter.CUSTOM_PLANTER_COUNT);
		CUSTOM_PLANTER_COUNT++;

		// clear planter placer
		Destroy (planterPlacer);
	}

	protected void Update () {
		if (Input.GetMouseButton(0) && GUIUtility.hotControl == 0) {
			SelectPlanterLocation();
		}
		if (Input.GetMouseButtonUp(0) && GUIUtility.hotControl == 0) {
			if (!Input.GetKey(cancelKey)) {
				// can only create a planter if it doesn't overlap another
				Collider2D otherPlanters = Physics2D.OverlapArea (planterPlacer.renderer.bounds.min, planterPlacer.renderer.bounds.max);
				if (otherPlanters == null)
					CreatePlanter();
				else
					Destroy (planterPlacer);
			}
			else {
				// cancel creation and destroy the placer guide
				Destroy (planterPlacer);
			}
		}
	}
}
