using UnityEngine;
using System.Collections;

public class GrowingPlant : MonoBehaviour {

	// Base class for all growing plants
	protected int _cost = 10;
	public int Cost {
		get { return _cost; }
		set { _cost = value; }
	}
}
