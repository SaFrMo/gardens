using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class WASDMovement : MonoBehaviour {
	// Maximum Speed
	public float maxSpeedX = 5;
	public float maxSpeedY = 5;
	// differentiation between grounded and aerial controls
	// also includes jumping controls - let the player jump while grounded
	public enum MovementType {
		Grounded,
		Jumping,
		Swinging,
		Dead,
		Ziplining
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
	public float lengthChangeRate = 5f;
	// minimum rope length
	public float minRopeLength = 1f;
	// force applied to swinging: higher the force, faster player can swing (TODO: upgrades to this value as player continues?)
	public float swingForce = .8f;

	private void AirborneControls() {
		//Vector2 anchorPosition = AnchorPoint.Current.transform.position; //store position of the current anchor point
		//float ropeLength = GetComponent<Swinging>().RopeLength;

		// break rope
		if (Input.GetKeyDown (jumpBreak) || Input.GetKeyDown (fallBreak)) {
			// gives you a boost if you jump
			if (Input.GetKeyDown (jumpBreak)) { rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, jumpForce * 2); }
			GetComponent<Swinging>().DetachFromAnchor();
		}

		// rope length change
		// you can always increase rope length... (TODO: make a max length)
		if (Input.GetAxis ("Vertical") <= 0) {
			GetComponent<Swinging>().RopeLength -= lengthChangeRate * Input.GetAxis ("Vertical") * Time.deltaTime;
		}
		// ...but there's a minimum rope length you can't pass
		else if (Input.GetAxis ("Vertical") > 0) {
			if (GetComponent<Swinging>().RopeLength > minRopeLength) {
				GetComponent<Swinging>().RopeLength -= lengthChangeRate * Input.GetAxis ("Vertical") * Time.deltaTime;
			}
		}

		// swing!
		if (Input.GetAxis ("Horizontal") != 0) {
			rigidbody2D.AddForce (Vector2.right * Input.GetAxis("Horizontal") * swingForce);
		}
	}

	// ZIPLINING CONTROLS
	// =====================
	private Vector2 goal;
	private Vector2 offset;
	private Vector2 ziplinePosition;
	public float ziplineRate = 1f;

	public void ZiplineToPoint (Vector2 start, Vector2 end) {
		if (CurrentType == MovementType.Swinging) {
			GetComponent<Swinging>().DetachFromAnchor();
		}
		CurrentType = MovementType.Ziplining;
		rigidbody2D.gravityScale = 0;
		rigidbody2D.drag = 0;
		rigidbody2D.velocity = Vector2.zero;
		offset = (Vector2)transform.position - start;
		ziplinePosition = start;
		goal = end;
	}
	private void ZiplineMovement() {
		// move the player toward the goal
		transform.position = Vector3.MoveTowards (transform.position, goal, ziplineRate * Time.deltaTime);
		// break zipline
		if (Input.GetKeyDown (jumpBreak) || Input.GetKeyDown (fallBreak)) {
			// gives you a boost if you jump
			if (Input.GetKeyDown (jumpBreak)) { rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, jumpForce * 2); }
			goal = transform.position;
		}


		// zipline is close enough to goal, so reset it
		if (Vector2.Distance (transform.position, goal) <= .1f) {
			CurrentType = MovementType.Jumping;
			GetComponent<Zipline>().ResetZipline();
			rigidbody2D.gravityScale = 1;
			rigidbody2D.drag = .1f;
		}

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
		if (Input.GetKeyDown (jumpKey) && CurrentType != MovementType.Jumping){
			CurrentType = MovementType.Jumping;
			rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, jumpForce);
		}
			else if (CurrentType != MovementType.Swinging) {
				GetComponent<Swinging>().DetachFromAnchor();
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

	//make sure the player is not moving too fast
	private void CheckSpeed ()
	{
		if (rigidbody2D.velocity.x > maxSpeedX)
		{
			rigidbody2D.velocity = new Vector2(maxSpeedX, rigidbody2D.velocity.y);
		}
		if (rigidbody2D.velocity.y > maxSpeedY)
		{
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, maxSpeedY);
		}
	}

	// TODO: Neater way to handle switching back to Grounded type
	/*
	private void OnCollisionEnter2D (Collision2D c) {
		// tag all garden boxes and anything else the player can run/jump on as "Ground"
		if (c.gameObject.tag == "Ground") {
			// lets the player jump again after landing on ground
			if (CurrentType != MovementType.Grounded) {

				CurrentType = MovementType.Grounded;

			}
		}
	}
*/


	private void OnCollisionStay2D (Collision2D c) {
		// tag all garden boxes and anything else the player can run/jump on as "Ground"
		if (c.gameObject.tag == "Ground") {
			// lets the player jump again after landing on ground
			if (CurrentType != MovementType.Grounded) {
				//Debug.Log ("collision");
					CurrentType = MovementType.Grounded;

			}
		}
	}

	
	// Update ()
	// ==============

	private void Update () {
		if (CurrentType == MovementType.Grounded || CurrentType == MovementType.Jumping) {
			this.renderer.enabled = true; // this hides the player sprite when dead, there's probably a better way around but this will do for now
			GroundedControls();
		}
		else if (CurrentType == MovementType.Swinging) {
			this.renderer.enabled = true;
			AirborneControls();
			//GroundedControls();
		}
		else if (CurrentType == MovementType.Ziplining) {
			this.renderer.enabled = true;
			ZiplineMovement();
			AirborneControls();
		}

		else if (CurrentType == MovementType.Dead) {
			this.renderer.enabled = false; //hide the player when you die
		}
		CheckSpeed ();
	}
}
