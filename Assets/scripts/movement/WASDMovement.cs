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
		Vector2 anchorPosition = AnchorPoint.Current.transform.position; //store position of the current anchor point


		if (Input.GetKeyDown (jumpBreak) || Input.GetKeyDown (fallBreak)) {
			GetComponent<Swinging>().DetachFromAnchor();
		}

		if (Input.GetAxis ("Vertical") != 0) {
			GetComponent<Swinging>().RopeLength += lengthChangeRate * Input.GetAxis ("Vertical") * Time.deltaTime;
		}

		//Figure out where player will end up if current velocity is applied
		Vector2 testPosition = (Vector2)rigidbody2D.transform.position + rigidbody2D.velocity;
		float distance = Vector2.Distance(testPosition, anchorPosition);


		if (distance > GetComponent<Swinging>().RopeLength) {
			testPosition = (testPosition - anchorPosition).normalized * GetComponent<Swinging>().RopeLength;
		}
		rigidbody2D.velocity = (testPosition - (Vector2)rigidbody2D.transform.position);
		rigidbody2D.transform.position = testPosition;
	}


	// GROUNDED CONTROLS
	// ===================

	// how fast the character will move while grounded
	public float maxSpeed = 4f;
	// jumps, but still has grounded controls apply (ie, A/D move left and right)
	public KeyCode jumpKey = KeyCode.Space;
	public float jumpForce = 3f;

	private bool facingRight = true;

	private void GroundedControls () {
		float move = Input.GetAxis("Horizontal");
		rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);

		// ground-based jumping (no anchor points involved)
		if (Input.GetKeyDown (jumpKey) && CurrentType != MovementType.Jumping) {
			CurrentType = MovementType.Jumping;
			rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, jumpForce);
		}

		//make sure the character is facing the right direction
		if (move > 0 && !facingRight)
		{
			Flip();
		}
		else if (move < 0 && facingRight)
		{
			Flip();
		}
	}

	//Change which way the character is facing. 
	private void Flip() 
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1; //inverts x scale to flip sprite
		transform.localScale = theScale;
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

	private void FixedUpdate () {
		if (CurrentType == MovementType.Grounded || CurrentType == MovementType.Jumping) {
			GroundedControls();
		}
		else if (CurrentType == MovementType.Swinging) {
			AirborneControls();
			GroundedControls();
		}
	}
}
