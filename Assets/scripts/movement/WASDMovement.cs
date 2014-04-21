using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class WASDMovement : MonoBehaviour {

	// differentiation between grounded and aerial controls
	// also includes jumping controls - let the player jump while grounded
	public enum MovementType {
		Grounded,
		Jumping,
		Airborne
	};
	
	private MovementType _currentType = MovementType.Grounded;
	public MovementType CurrentType {
		get { return _currentType; }
		set { _currentType = value; }
	}

	// how fast the character will move while grounded
	public float groundedSpeed = 40f;
	// jumps, but still has grounded controls apply (ie, A/D move left and right)
	public KeyCode jumpKey = KeyCode.Space;
	public float jumpForce = 2f;


	// TODO
	private void AirborneControls() {}

	private void GroundedControls () {
		// left/right movement
		//rigidbody.AddForce (Vector3.right * Input.GetAxis ("Horizontal") * groundedSpeed);
		rigidbody.MovePosition (transform.position + (Vector3.right * Input.GetAxis ("Horizontal") * groundedSpeed));

		// ground-based jumping (no anchor points involved)
		if (Input.GetKeyDown (jumpKey) && CurrentType != MovementType.Jumping) {
			CurrentType = MovementType.Jumping;
			rigidbody.AddForce (Vector3.up * jumpForce, ForceMode.Impulse);
		}
	}

	private void OnCollisionEnter (Collision c) {
		// tag all garden boxes and anything else the player can run/jump on as "Ground"
		if (c.gameObject.CompareTag ("Ground")) {
			// lets the player jump again after landing on ground
			if (CurrentType == MovementType.Jumping) {
				CurrentType = MovementType.Grounded;
			}
		}
	}
	
	// Update ()
	// ==============

	private void Update () {
		if (CurrentType == MovementType.Grounded || CurrentType == MovementType.Jumping) {
			GroundedControls();
		}
		else if (CurrentType == MovementType.Airborne) {
			AirborneControls();
		}
	}
}
