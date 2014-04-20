using UnityEngine;
using System.Collections;

public class SwingingRope : MonoBehaviour {

	/// <summary>
	/// Resizes and rotates the swinging rope to fit between player and AnchorPoint.Current
	/// </summary>
	public void DrawRope () {
		Vector3 v3 = GameManager.PLAYER.transform.position - AnchorPoint.Current.transform.position;
		// net is located at the average position of PLAYER and AnchorPoint.Current
		transform.position = (GameManager.PLAYER.transform.position + AnchorPoint.Current.transform.position) / 2;
		// stretches Y scale so it fits between PLAYER and AnchorPoint.Current
		transform.localScale = new Vector3 (transform.localScale.x, v3.magnitude, transform.localScale.z);
		// rotates the rope accordingly
		transform.rotation = Quaternion.FromToRotation (Vector3.up, v3);
	}





	void Update () {
		DrawRope();
	}
}
