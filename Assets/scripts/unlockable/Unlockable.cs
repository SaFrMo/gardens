using UnityEngine;
using System.Collections;

[SerializeAll]
public class Unlockable : MonoBehaviour {

	public Unlockable () {}

	public bool unlocked = false;
	public string unlockableName;
	public string description;
	public int unlockCost;


}
