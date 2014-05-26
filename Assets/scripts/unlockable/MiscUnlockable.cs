using UnityEngine;
using System.Collections;

public class MiscUnlockable {

	public MiscUnlockable (string _name, string _description, int _cost, bool _unlocked = false)
	{
		unlockableName = _name;
		description = _description;
		unlockCost = _cost;
		unlocked = _unlocked;
	}

	public bool unlocked = false;
	//public Texture2D icon;
	public string unlockableName;
	public string description;
	public int unlockCost;
}
