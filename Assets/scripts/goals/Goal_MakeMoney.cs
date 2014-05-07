using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Goal_MakeMoney : Goal {

	public float goalAmount = 10f;

	public override void CheckGoal()
	{
		base.requirements = new List<bool>() 
		{
			Mathf.Abs(Sun.income) >= Mathf.Abs(goalAmount)
		};
		base.CheckGoal();

	}
}
