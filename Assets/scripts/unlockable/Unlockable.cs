using UnityEngine;
using System.Collections;

[SerializeAll]
public class Unlockable : MonoBehaviour {

	public bool unlocked = false;
	public bool unlockedAtStart = false;
	public Texture2D icon;
	public string unlockableName;
	public string description;
	public int unlockCost;

	public void ResetLock ()
	{
		unlocked = unlockedAtStart;
	}
}
