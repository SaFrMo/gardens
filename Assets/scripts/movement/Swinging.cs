﻿using UnityEngine;
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
	private ConstantForce _constantForce = null;

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
		WASDMovement.CurrentType = WASDMovement.MovementType.Swinging;

		if (_rope == null) {
			_rope = GameObject.Instantiate (ropePrefab) as GameObject;
			// prevent box from drawing in middle of the screen
			_rope.renderer.enabled = false;
		}

		// sets initial length of rope
		_ropeLength = (transform.position - AnchorPoint.Current.transform.position).magnitude;

		// creates a new spring joint if there's none existing
		if (springJoint == null) {
			springJoint = gameObject.AddComponent<SpringJoint2D>();
			springJoint.frequency = 0;
		}
		springJoint.connectedBody = AnchorPoint.Current.rigidbody2D;






	}

	public void DetachFromAnchor () {
		WASDMovement.MovementType cT = WASDMovement.CurrentType;
		if (cT != WASDMovement.MovementType.Grounded && cT != WASDMovement.MovementType.Jumping
		    && cT != WASDMovement.MovementType.Running) 
		{ 
			WASDMovement.CurrentType = WASDMovement.MovementType.Jumping; 
		}
		Destroy (_rope);
		Destroy (_constantForce);
		Destroy (springJoint);
		Destroy (AnchorPoint.Current);

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
		if (springJoint != null) {
			//springJoint.connectedAnchor = new Vector2 (0, -5);//transform.position as Vector2 + _ropeLength;
			springJoint.distance = _ropeLength;
		}
		if (_rope != null) {
			DrawRope();
			Swing ();
		}
	}
}
