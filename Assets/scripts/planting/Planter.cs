using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Planter : MonoBehaviour {

	public static Planter SELECTED_PLANTER = null;

	private Color originalColor;
	private SpriteRenderer spriteRender;

	// what plant is contained inside the planter?
	private GameObject _contents = null;
	public GameObject Contents {
		get { return _contents; }
		set { 
			_contents = value; 
			if (spriteRender.material.color != originalColor)
				spriteRender.material.color = originalColor;
		}
	}

	// how much water is there in this planter?
	private int _waterLevel = 100;
	public int WaterLevel {
		get { return _waterLevel; }
		set { _waterLevel = value; }
	}

	// auto water?
	public static bool AUTO_WATER = true;

	// decrease water level every X seconds
	public float waterDelay = 5f;
	private IEnumerator WaterDrain () {
		while (WaterLevel > 0) {
			WaterLevel--;
			//print (string.Format ("{0} water level: {1}", gameObject.name, WaterLevel));
			yield return new WaitForSeconds (waterDelay);
		}
	}

	// plant something if there's nothing there
	// ==========================================
	public KeyCode catalogAccess = KeyCode.Q;
	private bool showPlantCatalog = false;

	public void Plant (GameObject gp) {
		// uproot old plant, TODO: "warning, you're about to lose the old plant!"
		if (Contents != null) {

		}

		Contents = gp;
		GameObject newPlant = Instantiate (gp) as GameObject;
		// TODO: move it into position more flexibly (take into account sprite size rather than straight Vector2.up)
		newPlant.transform.position = new Vector2 (transform.position.x, transform.position.y) + Vector2.up;
		newPlant.transform.parent = transform;

		showPlantCatalog = false;
	}


	// planter selection
	// ====================

	private void OnCollisionEnter2D (Collision2D c) {
		if (AUTO_WATER) {
			if (c.collider.gameObject.name == "Player") {
				WaterLevel = 100;
			}
		}
		if (c.collider.gameObject.name == "Player" && Contents == null && SELECTED_PLANTER == null) {
			SELECTED_PLANTER = this;
		}
	}

	private void OnCollisionStay2D (Collision2D c) {
		if (c.collider.gameObject.name == "Player" && Contents == null && SELECTED_PLANTER == null) {
			SELECTED_PLANTER = this;
		}
	}

	private void OnCollisionExit2D () {
		SELECTED_PLANTER = null;
		showPlantCatalog = false;
	}
	
	// glow when planting is available
	// TODO: spruce this up
	private void ShowPlantingAvailable () {
		spriteRender.material.color = Color.green;
	}

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
				Plant (go);
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

	// START() and UPDATE()
	// =======================
	
	private void Start () {
		spriteRender = GetComponent<SpriteRenderer>();
		originalColor = spriteRender.material.color;
		spriteRender.color = originalColor;
		StartCoroutine (WaterDrain());
	}

	void Update () {
		if (SELECTED_PLANTER == this) {
			if (Input.GetKeyDown (catalogAccess)) {
				showPlantCatalog = !showPlantCatalog;
			}
			if (!showPlantCatalog && Contents == null) {
				ShowPlantingAvailable();
			}
			else {
				print (string.Format ("Catalog from {0} being displayed", gameObject.name));
			}
		}
		else {
			if (spriteRender.material.color != originalColor)
				spriteRender.material.color = originalColor;
		}
	}
}
