using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour {

	private int _dollars;
	public int Dollars {
		get { return _dollars; }
		set { _dollars = value; }
	}

	public List<Object> inventory = new List<Object>();
}
