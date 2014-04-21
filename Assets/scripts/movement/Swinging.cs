using UnityEngine;
using System.Collections;

public class Swinging : MonoBehaviour {

	public GameObject ropePrefab;

	private GameObject _rope;
	private float _ropeLength;
	public float RopeLength {
		get { return _ropeLength; }
		set { _ropeLength = value; }
	}

	/// <summary>
	/// Resizes and rotates the swinging rope to fit between player and AnchorPoint.Current
	/// </summary>
	public void DrawRope () {

		Vector3 v3 = GameManager.PLAYER.transform.position - AnchorPoint.Current.transform.position;
		// net is located at the average position of PLAYER and AnchorPoint.Current
		_rope.transform.position = (GameManager.PLAYER.transform.position + AnchorPoint.Current.transform.position) / 2;
		// stretches Y scale to _ropeLength
		_rope.transform.localScale = new Vector3 (transform.localScale.x, _ropeLength, transform.localScale.z);
		// rotates the rope accordingly
		_rope.transform.rotation = Quaternion.FromToRotation (Vector3.up, v3);

		// it's done setting up, so make it visible if it's not already
		if (!_rope.renderer.enabled)
			_rope.renderer.enabled = true;
	}

	public void AttachToAnchor () {
		// change movement type to Swinging
		GetComponent<WASDMovement>().CurrentType = WASDMovement.MovementType.Swinging;

		if (_rope == null) {
			_rope = GameObject.Instantiate (ropePrefab) as GameObject;
			// prevent box from drawing in middle of the screen
			_rope.renderer.enabled = false;
		}

		// sets initial length of rope
		_ropeLength = (GameManager.PLAYER.transform.position - AnchorPoint.Current.transform.position).magnitude;

		// parents anchor point to player
		//transform.parent = AnchorPoint.Current;


	}

	public void DetachFromAnchor () {
		GameManager.PLAYER.GetComponent<WASDMovement>().CurrentType = WASDMovement.MovementType.Jumping;
		Destroy (_rope);

	}

	public void Swing () {
		/*
		float difference = (GameManager.PLAYER.transform.position - AnchorPoint.Current.transform.position).magnitude - _ropeLength;
		if (difference != 0) {
			//transform.Translate (Vector3.MoveTowards (transform.position, AnchorPoint.Current.transform.position, 1f * Time.deltaTime));
		}
		*/
	}



	void Update () {
		if (_rope != null) {
			DrawRope();
			Swing ();
		}
	}
}
