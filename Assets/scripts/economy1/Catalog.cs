using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Catalog : MonoBehaviour {

	private bool showPlantCatalog = false;
	public KeyCode catalogAccess = KeyCode.Q;

	// plant catalog
	public float plantCatalogSide = 200f;
	private Vector2 scrollPos = Vector2.zero;
	public List<GameObject> plantsList;

	private void PlantInfoCell (GameObject go) {
		// displays information for each plant prefab in the Catalog list
		GUILayout.BeginHorizontal();
		// must refer to startingCost rather than Cost b/c Cost is set on Start(), but prefab hasn't been instantiated yet
		int cost = go.GetComponent<GrowingPlant>().cost;
		if (GUILayout.Button (go.name)) {
			// do you have enough money for this plant?
			if (GetComponent<PlayerInventory>().Dollars >= cost) {
				// plant the selected plant
				Planter.SELECTED_PLANTER.Plant (go);
				// hide the catalog
				showPlantCatalog = false;
				// deduct the cost
				GetComponent<PlayerInventory>().Dollars -= cost;
			}
			// TODO: error message: not enough money!
			else {}
		}
		// displays the cost of the plant in question
		GUILayout.Box ("$" + cost.ToString());
		GUILayout.EndHorizontal();
	}
	
	private void PlantCatalog () {
		GUILayout.BeginArea (new Rect (Screen.width / 2 - plantCatalogSide,
		                               Screen.height / 2 - plantCatalogSide,
		                               plantCatalogSide,
		                               plantCatalogSide));
		// should the scroll position reset when the window closes?
		scrollPos = GUILayout.BeginScrollView (scrollPos);
		foreach (GameObject go in plantsList) {
			PlantInfoCell (go);
		}
		GUILayout.EndScrollView();
		GUILayout.EndArea();
	}

	// ONGUI ()
	// =========
	
	private void OnGUI () {
		if (showPlantCatalog) {
			PlantCatalog();
		}
	}

	private void Update () {
		if (Planter.SELECTED_PLANTER != null && Input.GetKeyDown (catalogAccess)) {
			showPlantCatalog = !showPlantCatalog;
		}
		else if (Planter.SELECTED_PLANTER == null) {
			showPlantCatalog = false;
		}
	}
}
