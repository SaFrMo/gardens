using UnityEngine;
using System.Collections;

[SerializeAll]
public class GrowingPlant : Unlockable {

	public GameObject prefab;
	
	public enum Purpose
	{
		Food,
		Science,
		Aesthetic
	}

	public enum Region
	{
		Europe
	}

	public enum PlantColor
	{
		Green
	}
	
	// plant setup - usable in the editor
	// quality is calculated automatically, so no need to have a starting value
	public int startingCost = 10;
	public int startingMaximumSellingPrice = 5;
	public int startingCurrentSellingPrice = 1;
	public Region startingOrigin = Region.Europe;
	public PlantColor startingColor = PlantColor.Green;
	public int startingCurrentHP = 10;
	public int startingMaxHP = 10;
	public int startingMaxWater = 50;


	
	protected virtual void Start ()
	{
		_cost = startingCost;
		_maxSellingPrice = startingMaximumSellingPrice;
		_currentSellingPrice = startingCurrentSellingPrice;
		_origin = startingOrigin;
		_thisColor = startingColor;
		_hp = startingCurrentHP;
		_maxHP = startingMaxHP;
		_maxWater = startingMaxWater;
		_currentWater = startingMaxWater;
	}

	public string information;
	public string toolTip;
	
	// how much for seeds in the store
	protected int _cost;
	public int Cost {
		get { return _cost; }
		set { _cost = value; }
	}
	
	// see Gardening, Economics, and Aesthetics in the wiki for more detail on these members
	
	// selling prices - maximum and current
	protected int _maxSellingPrice;
	public int MaxSellingPrice
	{
		get { return _maxSellingPrice; }
		set { _maxSellingPrice = value; }
	}
	
	protected int _currentSellingPrice;
	public int CurrentSellingPrice
	{
		get { return _currentSellingPrice; }
		set { _currentSellingPrice = value; }
	}
	
	// plant quality = current value / max value. returned as a percentage.
	public float Quality
	{
		get { return Mathf.Round((_currentSellingPrice / _maxSellingPrice) * 100); }
	}
	
	// origin region
	protected Region _origin;
	public Region Origin
	{
		get { return _origin; }
		set { _origin = value; }
	}
	
	// color
	protected PlantColor _thisColor;
	public PlantColor ThisColor
	{
		get { return _thisColor; }
		set { _thisColor = value; }
	}
	
	// HP of the plant - think predators, weather, vandals, etc.
	protected int _hp;
	public int HP
	{
		get { return _hp; }
		set { _hp = value; }
	}
	
	protected int _maxHP;
	public int MaxHP
	{
		get { return _maxHP; }
		set { _maxHP = value; }
	}

	// water-related stats
	protected int _maxWater;
	public int MaxWater
	{
		get { return _maxWater; }
		set { _maxWater = value; }
	}

	protected int _currentWater;
	public int CurrentWater
	{
		get { return _currentWater; }
		set { _currentWater = value; }
	}

	public float CurrentWaterLevel
	{
		get { return ((float)CurrentWater / (float)MaxWater); }
	}

	// show water levels
	private Rect _containingPlanter;
	public bool showWaterLevels = true;
	private void OnGUI ()
	{
		GUI.skin = GameManager.GUI_SKIN;
		if (showWaterLevels)
		{
			// TODO: make this look nicer
			_containingPlanter = SaFrMo.GUIOverObject (transform.parent.gameObject);
			GUI.DrawTexture (new Rect (_containingPlanter), SaFrMo.CreateColor(Color.black));
			GUI.DrawTexture (new Rect (_containingPlanter.x,
			                           _containingPlanter.y,
			                           _containingPlanter.width * CurrentWaterLevel,
			                           _containingPlanter.height), SaFrMo.CreateColor (Color.blue));
		}

		// TODO: make this look nicer, too
		GUI.Box (_thisRect, string.Format ("VALUE: ${0} out of a possible ${1}", CurrentSellingPrice.ToString(), MaxSellingPrice.ToString()));
	}

	// auto water?
	public static bool AUTO_WATER = true;


	public void FillWater ()
	{
		CurrentWater = MaxWater;
	}
	
	// every X seconds, decrease water level, change value, etc.
	public static float DELAY = 5f;
	protected Timer t = new Timer (DELAY, true);
	public int ValueIncrement = 3;

	private Rect _thisRect;

	protected void OverTime () {

		CurrentWater--;
		if (CurrentWaterLevel >= .6f && CurrentSellingPrice < MaxSellingPrice)
			CurrentSellingPrice += ValueIncrement;
		else if (CurrentWaterLevel >= .3f && CurrentSellingPrice < MaxSellingPrice)
			CurrentSellingPrice += ValueIncrement / 2;
		else
			CurrentSellingPrice -= ValueIncrement;
	}

	protected void Update ()
	{
		if (t.RunTimer())
			OverTime();
		_thisRect = SaFrMo.GUIOverObject (gameObject);
	}


}