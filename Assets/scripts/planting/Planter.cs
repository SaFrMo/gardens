using UnityEngine;
using System.Collections;

public class Planter : MonoBehaviour {

	public static Planter SELECTED_PLANTER = null;

	private Color originalColor;
	private SpriteRenderer spriteRender;

	// what plant is contained inside the planter?
	private GrowingPlant _contents = null;
	public GrowingPlant Contents {
		get { return _contents; }
		set { _contents = value; }
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
			print (string.Format ("{0} water level: {1}", gameObject.name, WaterLevel));
			yield return new WaitForSeconds (waterDelay);
		}
	}

	// plant something if there's nothing there
	// ==========================================
	public KeyCode catalogAccess = KeyCode.Q;
	private bool showPlantCatalog = false;


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
			if (!showPlantCatalog) {
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
