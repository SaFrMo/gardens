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

	// plant something if there's nothing there
	public KeyCode catalogAccess = KeyCode.Q;
	private bool showPlantCatalog = false;

	private void PlantSomething () {

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
	
	private void Start () {
		spriteRender = GetComponent<SpriteRenderer>();
		originalColor = spriteRender.material.color;
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
