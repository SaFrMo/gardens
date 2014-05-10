using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Planter : MonoBehaviour {

	public static Planter SELECTED_PLANTER = null;
	public static List<GrowingPlant> ALL_PLANTS = new List<GrowingPlant>();
	public static List<GameObject> ALL_PLANTERS = new List<GameObject>();
	public static int PLANTS_PLANTED = 0;

	private Color originalColor;
	private SpriteRenderer spriteRender;

	// what plant is contained inside the planter?
	private GrowingPlant _contents = null;
	public GrowingPlant Contents {
		get { return _contents; }
		set { 
			_contents = value; 
			if (spriteRender.material.color != originalColor)
				spriteRender.material.color = originalColor;
		}
	}



	// plant something if there's nothing there
	// ==========================================
	GameObject newPlant;

	public void Plant (GameObject gp) {
		// uproot old plant, TODO: "warning, you're about to lose the old plant!"
		if (Contents != null) {
			SellContents();
		}
		newPlant = Instantiate (gp) as GameObject;
		// TODO: move it into position more flexibly (take into account sprite size rather than straight Vector2.up)
		newPlant.transform.position = new Vector2 (transform.position.x, transform.position.y) + Vector2.up;
		newPlant.transform.parent = transform;
		ALL_PLANTS.Add (newPlant.GetComponent<GrowingPlant>());
		Contents = newPlant.GetComponent<GrowingPlant>();
		PLANTS_PLANTED++;
	}


	// planter selection
	// ====================

	private void OnCollisionEnter2D (Collision2D c) {
		if (GrowingPlant.AUTO_WATER) {
			if (c.collider.gameObject.name == "Player") {
				try
				{
					GetComponentInChildren<GrowingPlant>().FillWater();
				}
				catch {}
			}
		}

		if (c.collider.gameObject.name == "Player" && Contents == null && SELECTED_PLANTER == null) {
			SELECTED_PLANTER = this;
		}
		if (Contents != null)
		{
			Contents.showWaterLevels = true;
		}

	}

	private void OnCollisionStay2D (Collision2D c) {
		if (c.collider.gameObject.name == "Player") {
			SELECTED_PLANTER = this;
		}

	}

	private void OnCollisionExit2D () {
		SELECTED_PLANTER = null;
		if (Contents != null)
		{
			Contents.showWaterLevels = false;
		}
		Catalog.showPlantCatalog = false;
	}
	
	// glow when planting is available
	// TODO: spruce this up
	private void ShowPlantingAvailable () {
		spriteRender.material.color = Color.green;
	}

	// sell this plant
	public void SellContents ()
	{
		GameManager.GAME_MANAGER.GetComponent<PlayerInventory>().Dollars += Contents.CurrentSellingPrice;
		Destroy (Contents);
		Destroy (newPlant);
	}





	// START() and UPDATE()
	// =======================
	
	private void Start () {
		spriteRender = GetComponent<SpriteRenderer>();
		originalColor = spriteRender.material.color;
		spriteRender.color = originalColor;
		ALL_PLANTERS.Add (gameObject);
	}

	void Update () {
		if (SELECTED_PLANTER == this) {
			/*
			if (Input.GetKeyDown (catalogAccess)) {
				showPlantCatalog = !showPlantCatalog;
			}
			if (!showPlantCatalog && Contents == null) {
				ShowPlantingAvailable();
			}
			*/
			if (Contents == null) {
				ShowPlantingAvailable();
			}
		}
		else {
			if (spriteRender.material.color != originalColor)
				spriteRender.material.color = originalColor;
		}
	}
}
