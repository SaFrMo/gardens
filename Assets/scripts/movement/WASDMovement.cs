using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class WASDMovement : MonoBehaviour {

	// differentiation between grounded and aerial controls
	// also includes jumping controls - let the player jump while grounded
	public enum MovementType {
		Grounded,
		Jumping,
		Swinging
	};
	
	private MovementType _currentType = MovementType.Grounded;
	public MovementType CurrentType {
		get { return _currentType; }
		set { _currentType = value; }
	}

	// AIRBORNE CONTROLS
	// ==================

	// break from airborne controls
	public KeyCode jumpBreak = KeyCode.Space;
	public KeyCode fallBreak = KeyCode.LeftControl;
	// lengthen/shorten rope
	public float lengthChangeRate = 1f;

	private void AirborneControls() {

		if (Input.GetKeyDown (jumpBreak) || Input.GetKeyDown (fallBreak)) {
			GetComponent<Swinging>().DetachFromAnchor();
		}

		if (Input.GetAxis ("Vertical") != 0) {
			GetComponent<Swinging>().RopeLength += lengthChangeRate * Input.GetAxis ("Vertical") * Time.deltaTime;
		}
	}


	// GROUNDED CONTROLS
	// ===================

	// how fast the character will move while grounded
	public float groundedSpeed = 40f;
	// jumps, but still has grounded controls apply (ie, A/D move left and right)
	public KeyCode jumpKey = KeyCode.Space;
	public float jumpForce = 2f;

	private void GroundedControls () {
		// left/right movement - MovePosition is more forgiving than AddForce
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
		else if (CurrentType == MovementType.Swinging) {
			AirborneControls();
			GroundedControls();
		}
	}
}
