using UnityEngine;
using System.Collections;

[SerializeAll]
public class Stats : MonoBehaviour {

	public static int ToNextLevel ()
	{
		return 1;
	}

	public static int XP = 0;
		
	public static int Level 
	{ 
		// TODO: level up slope
		get { return (int)XP / 5; }
	}
}
