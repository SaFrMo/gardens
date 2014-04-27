using UnityEngine;
using System.Collections;


public class GrowingPlant : MonoBehaviour {
	
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
	public int maxWaterLevel = 50;
	
	protected virtual void Start ()
	{
		_cost = startingCost;
		_maxSellingPrice = startingMaximumSellingPrice;
		_currentSellingPrice = startingCurrentSellingPrice;
		_origin = startingOrigin;
		_thisColor = startingColor;
		_hp = startingCurrentHP;
		_maxHP = startingMaxHP;
		_maxWaterLevel = maxWaterLevel;
		_currentWaterLevel = maxWaterLevel;
	}

	public string information;
	
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

	// water level: higher-maintenance plants have lower levels
	protected int _maxWaterLevel;
	public int MaxWaterLevel
	{
		get { return _maxWaterLevel; }
		set { _maxWaterLevel = value; }
	}

	protected int _currentWaterLevel;
	public int CurrentWaterLevel
	{
		get { return _currentWaterLevel; }
		set { _currentWaterLevel = value; }
	}

	// for GUI display purposes
	public float CurrentWater
	{
		get { return _currentWaterLevel / _maxWaterLevel; }
	}

}