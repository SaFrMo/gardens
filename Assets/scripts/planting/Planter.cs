using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Planter : MonoBehaviour {

	public static Planter SELECTED_PLANTER = null;
	public static List<GameObject> ALL_PLANTERS = new List<GameObject>();

	private Color originalColor;
	private SpriteRenderer spriteRender;

	// what plant is contained inside the planter?
	//private GameObject _contents = null;
	public GameObject Contents;




	/*
	// how much water is there in this planter?
	private int _waterLevel = 100;
	public int WaterLevel {
		get { return _waterLevel; }
		set { _waterLevel = value; }
	}
	*/

	// auto water?
	public static bool AUTO_WATER = true;

	// decrease water level every X seconds
	public float waterDelay = 5f;

	private IEnumerator WaterDrain () {
		while (Contents.GetComponent<GrowingPlant>().currentWaterLevel > 0) {
			Contents.GetComponent<GrowingPlant>().currentWaterLevel--;
			yield return new WaitForSeconds (waterDelay);
		}
	}


	// plant something if there's nothing there
	// ==========================================

	public void Plant (GameObject gp) {
		// uproot old plant, TODO: "warning, you're about to lose the old plant!"
		if (Contents != null) {

		}

		//Contents = gp;
		GameObject newPlant = Instantiate (gp) as GameObject;
		// TODO: move it into position more flexibly (take into account sprite size rather than straight Vector2.up)
		newPlant.transform.position = new Vector2 (transform.position.x, transform.position.y) + Vector2.up;
		newPlant.transform.parent = transform;
		Contents = newPlant;

		StartCoroutine (WaterDrain());
	}


	// planter selection
	// ====================


	private void OnCollisionEnter2D (Collision2D c) {
		if (AUTO_WATER) {
			if (c.collider.gameObject.name == "Player") {
				try
				{
					Contents.GetComponent<GrowingPlant>().FillWater();
				}
				catch {}
			}
		}
		if (c.collider.gameObject.name == "Player") {
			SELECTED_PLANTER = this;
		}
	}



	private void OnCollisionStay2D (Collision2D c) {

		if (c.collider.gameObject.name == "Player" && SELECTED_PLANTER != this) {
			SELECTED_PLANTER = this;
		}

	}


	private void OnCollisionExit2D () {
		SELECTED_PLANTER = null;
	}
	
	// glow when planting is available
	// TODO: spruce this up
	private void ShowPlantingAvailable () {
		spriteRender.material.color = Color.green;
	}




	// planter water level display
	// =============================
	public bool showWaterLevel = false;
	Rect thisObjectRect;
	private void GetRect () 
	{
		thisObjectRect = SaFrMo.GUIOverObject (SELECTED_PLANTER.gameObject);
	}

	private void OnGUI () {
		// TODO: make this prettier
		if (showWaterLevel)
		{
			// TODO: this is terrible, to throw an unnecessary and frequent error
			try
			{
				float percentage = Contents.GetComponent<GrowingPlant>().CurrentWater;
				GUI.DrawTexture (new Rect (thisObjectRect), SaFrMo.CreateColor(Color.black));
				GUI.DrawTexture (new Rect (thisObjectRect.x,
				                           thisObjectRect.y,
				                           thisObjectRect.width * percentage,
				                           thisObjectRect.height), SaFrMo.CreateColor (Color.blue));
			}
			catch {}
		}
	}



	// START() and UPDATE()
	// =======================
	
	private void Start () {
		spriteRender = GetComponent<SpriteRenderer>();
		originalColor = spriteRender.material.color;
		spriteRender.color = originalColor;
		if (!ALL_PLANTERS.Contains(gameObject))
		{
			ALL_PLANTERS.Add (gameObject);
		}
	}

	void Update () {
		showWaterLevel = true;
		if (SELECTED_PLANTER == this) {
			GetRect ();
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
			else
			{
				showWaterLevel = true;
			}
		}
		else {
			if (spriteRender.material.color != originalColor)
				spriteRender.material.color = originalColor;
			showWaterLevel = false;
		}
	}
}
