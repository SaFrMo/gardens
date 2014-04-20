using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class WASDMovement : MonoBehaviour {

	// differentiation between grounded and aerial controls
	public enum MovementType {
		Grounded,
		Airborne
	};
	
	private MovementType _currentType = MovementType.Grounded;
	public MovementType CurrentType {
		get { return _currentType; }
		set { _currentType = value; }
	}

	// changes max speed and how quickly player slows down after letting 
	public float groundedDrag = 4.5f;
	// how fast the character will move while grounded
	public float groundedSpeed = 40f;

	// TODO
	private void AirborneControls() {}

	private void GroundedControls () {
		if (rigidbody.drag != groundedDrag) {
			rigidbody.drag = groundedDrag;
		}
		rigidbody.AddForce (Vector3.right * Input.GetAxis ("Horizontal") * groundedSpeed);
	}
	
	// Update ()
	// ==============

	private void Update () {
		if (CurrentType == MovementType.Grounded) {
			GroundedControls();
		}
		else if (CurrentType == MovementType.Airborne) {
			AirborneControls();
		}
	}
}
