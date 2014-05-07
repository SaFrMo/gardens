using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Goal_MakeMoney : Goal {

	public float goalAmount = 10f;

	public override void CheckGoal()
	{
		base.requirements = new List<bool>() 
		{
			Sun.income >= goalAmount,
			Planter.PLANTS_PLANTED > 0
		};
		base.CheckGoal();

	}
}
