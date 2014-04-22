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

	private SpringJoint2D springJoint = null;

	/// <summary>
	/// Resizes and rotates the swinging rope to fit between player and AnchorPoint.Current
	/// </summary>
	public void DrawRope () {

		Vector2 v2 = transform.position - AnchorPoint.Current.transform.position;
		// net is located at the average position of PLAYER and AnchorPoint.Current
		_rope.transform.position = (transform.position + AnchorPoint.Current.transform.position) / 2;
		// stretches Y scale to _ropeLength
		_rope.transform.localScale = new Vector2 (transform.localScale.x, _ropeLength);
		// rotates the rope accordingly
		_rope.transform.rotation = Quaternion.FromToRotation (Vector2.up, v2);

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
		_ropeLength = (transform.position - AnchorPoint.Current.transform.position).magnitude;

		// parents anchor point to player
		//transform.parent = AnchorPoint.Current;


		if (springJoint == null) {
			springJoint = gameObject.AddComponent<SpringJoint2D>();
		}
		springJoint.connectedBody = AnchorPoint.Current.rigidbody2D;



	}

	public void DetachFromAnchor () {
		GetComponent<WASDMovement>().CurrentType = WASDMovement.MovementType.Jumping;
		Destroy (_rope);
		Destroy (springJoint);

	}

	public void Swing () {
		/*
		float difference = (GameManager.PLAYER.transform.position - AnchorPoint.Current.transform.position).magnitude - _ropeLength;
		if (difference != 0) {
			//transform.Translate (Vector3.MoveTowards (transform.position, AnchorPoint.Current.transform.position, 1f * Time.deltaTime));
		}
		*/
	}




	void FixedUpdate () {

		if (_rope != null) {
			DrawRope();
			Swing ();
		}
		springJoint.distance = _ropeLength;

	}
}
