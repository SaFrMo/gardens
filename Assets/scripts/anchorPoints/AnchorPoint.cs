using UnityEngine;
using System.Collections;

public class AnchorPoint : MonoBehaviour {

	// referencing AnchorPoint.Current in another script will return the currently active AnchorPoint
	public static AnchorPoint Current = null;

	/// <summary>
	/// Sets AnchorPoint.Current to this anchor point.
	/// </summary>
	public void SelectThisAnchorPoint () {
		AnchorPoint.Current = this;
	}

	// TEST PURPOSES ONLY
	protected void Start () {
		SelectThisAnchorPoint();
	}


}
