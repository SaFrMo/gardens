using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Goal : MonoBehaviour
{
	// A goal:
	// - presented to a player via a Description
	// - Completed upon satisfying certain Requirements
	// - performs certain Rewards when complete


	public string description;
	protected List<bool> requirements = new List<bool>();
	public bool complete;
	public virtual void Rewards () {}

	public virtual void CheckGoal()
	{
		if (!complete)
		{
			foreach (bool r in requirements)
			{
				if (!r) return;
			}
			Rewards ();
			complete = true;
		}

	}
}