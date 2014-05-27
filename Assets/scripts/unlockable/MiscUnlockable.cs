using UnityEngine;
using System.Collections;

public class MiscUnlockable {

	// what to do when this item is unlocked
	public delegate void UnlockAction();

	// parameterless constructor for UnitySerializer
	public MiscUnlockable () {}

	public MiscUnlockable (string _name, string _description, int _cost, UnlockAction _action, bool _unlocked = false)
	{
		unlockableName = _name;
		description = _description;
		unlockCost = _cost;
		unlocked = _unlocked;
		Action = _action;
	}

	public void Unlock()
	{
		unlocked = true;
		Action();
	}

	public bool unlocked = false;
	//public Texture2D icon;
	public string unlockableName;
	public string description;
	public int unlockCost;
	public UnlockAction Action;
}
