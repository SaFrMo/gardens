using UnityEngine;
using System.Collections;

[SerializeAll]
public class Contract {

	public Contract() {}

	public Contract (string city, Texture2D spr, string levelName, string cityDescription, City generalCity, bool UNLOCKED = false)
	{
		CityName = city;
		CitySprite = spr;
		LevelName = levelName;
		CityDescription = cityDescription;
		GeneralCity = generalCity;
		unlocked = UNLOCKED;
	}

	public enum City
	{
		Chicago
	}

	public bool unlocked;
	public string CityName;
	public Texture2D CitySprite;
	public string LevelName;
	public string CityDescription;
	public City GeneralCity;

}
