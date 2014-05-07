﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Goal_MakeMoney : Goal {

	// how much income the goal requires
	public float goalAmount = 10f;

	public override void CheckGoal()
	{
		base.requirements = new List<bool>() 
		{
			Sun.income >= goalAmount,
			// you have to plant something to complete the goal
			Planter.PLANTS_PLANTED > 0
		};
		base.CheckGoal();
	}
}