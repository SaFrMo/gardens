using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Goal
{
	public Goal (string description)
	{
		_description = description;
	}

	private string _description;
	// puts the description into a presentable format
	public string Description
	{
		get { return string.Format ("{0} . {1}", (_complete ? "X" : "_"), _description); }
		set { _description = value; }
	}

	private bool _complete = false;
	public bool Complete { get; set; }
}