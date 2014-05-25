using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[SerializeAll]
public class Catalog : MonoBehaviour {

	// reset plant status
	public void ResetPlantsList ()
	{
		/*
		//GameManager.SetGameManager();
		foreach (GrowingPlant go in plantsList)
		{
			Unlockable u = go.GetComponent<Unlockable>();
			u.unlocked = u.unlockedAtStart;
		}
		*/
	}

	// tutorial purposes
	public static bool TUT_SHOW_PLANT_CATALOG;

	public static bool showPlantCatalog = false;
	public KeyCode catalogAccess = KeyCode.Q;

	// plant catalog
	public float plantCatalogSide = 200f;
	private Vector2 scrollPos = Vector2.zero;
	// the main catalog reference
	public static List<GrowingPlant> plantsList;

	/*
	public static List<GameObject> RefreshPlantsList ()
	{
		return GameObject.Find ("__Game Manager").GetComponent<Catalog>().plantsList;
	}
	*/

	private void PlantInfoCell (GrowingPlant g) {
		//GrowingPlant g = go.GetComponent<GrowingPlant>();
		// displays information for each plant prefab in the Catalog list
		GUILayout.BeginHorizontal();
		// must refer to startingCost rather than Cost b/c Cost is set on Start(), but prefab hasn't been instantiated yet
		int cost = g.startingCost;
		if (GUILayout.Button (g.unlockableName)) {
			// do you have enough money for this plant?
			if (GameManager.GAME_MANAGER.GetComponent<PlayerInventory>().Dollars >= cost) {
				// plant the selected plant
				Planter.SELECTED_PLANTER.Plant (g.prefab);
				// hide the catalog
				showPlantCatalog = false;
				// deduct the cost
				GameManager.GAME_MANAGER.GetComponent<PlayerInventory>().Dollars -= cost;
			}
			// TODO: error message: not enough money!
			else {}
		}
		Tooltips.ShowToolTip (g.toolTip, GUILayoutUtility.GetLastRect());
		// displays the cost of the plant in question
		GUILayout.Box ("$" + cost.ToString());
		GUILayout.EndHorizontal();
		GUILayout.Box (string.Format ("${0} max value\n{1} seconds to grow", g.startingMaximumSellingPrice, 
		                              g.startingMaximumSellingPrice / g.ValueIncrement * 5f));
	}
	
	private void PlantCatalog () {
		GUILayout.BeginArea (new Rect (Screen.width / 2 - plantCatalogSide,
		                               Screen.height / 2 - plantCatalogSide,
		                               plantCatalogSide,
		                               plantCatalogSide));
		// should the scroll position reset when the window closes?
		scrollPos = GUILayout.BeginScrollView (scrollPos);
		foreach (GrowingPlant g in plantsList) {
			if (g.unlocked)//o.GetComponent<Unlockable>().unlocked)
				PlantInfoCell (g);
				//PlantInfoCell (go);
		}
		SellContents();
		GUILayout.EndScrollView();
		GUILayout.EndArea();
	}

	// sell a plant if it's already there
	private void SellContents () {
		if (Planter.SELECTED_PLANTER.Contents != null) {
			if (GUILayout.Button ("Sell contents for $" + Planter.SELECTED_PLANTER.Contents.CurrentSellingPrice.ToString ())) {
				Planter.SELECTED_PLANTER.SellContents();
			}
		}
	}

	// contracts list
	public static List<Contract> contractsList = new List<Contract>();

	// ONGUI ()
	// =========
	

	private void OnGUI () {
		GUI.skin = GameManager.GUI_SKIN;
		if (showPlantCatalog) {
			PlantCatalog();
		}
	}

	private void Update () {
		// Doesn't show catalog if turn is starting or finishing
		if (Planter.SELECTED_PLANTER != null && Input.GetKeyDown (catalogAccess)) {
			if (WASDMovement.CurrentType != WASDMovement.MovementType.TurnDone &&
			    WASDMovement.CurrentType != WASDMovement.MovementType.TurnStart)
			{
				showPlantCatalog = !showPlantCatalog;
			}
		}
		else if (Planter.SELECTED_PLANTER == null) {
			showPlantCatalog = false;
		}

		if (WASDMovement.CurrentType == WASDMovement.MovementType.TurnDone ||
		    WASDMovement.CurrentType == WASDMovement.MovementType.TurnStart)
		{
			showPlantCatalog = false;

		}
	}
}	
