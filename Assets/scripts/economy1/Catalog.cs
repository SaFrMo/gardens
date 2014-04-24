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
	
	private void PlantCatalog () {
		GUILayout.BeginArea (new Rect (Screen.width / 2 - plantCatalogSide,
		                               Screen.height / 2 - plantCatalogSide,
		                               plantCatalogSide,
		                               plantCatalogSide));
		scrollPos = GUILayout.BeginScrollView (scrollPos);
		foreach (GameObject go in plantsList) {
			if (GUILayout.Button (go.name)) {
				Planter.SELECTED_PLANTER.Plant (go);
			}
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
