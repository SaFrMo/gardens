using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
		Running,
		Swinging,
		Dead,
		Ziplining,
		Hanging,
		TurnDone,
		TurnStart
	};

	// sounds list: TODO: swap out sounds?
	public List<AudioClip> sounds = new List<AudioClip>();
	public void PlaySound (AudioClip which)
	{
		try { GetComponent<AudioSource>().PlayOneShot (which); }
		catch { print ("Couldn't play " + CurrentType); }
	}

	private static int oldAnimation = 0;
	private static MovementType _currentType = MovementType.Grounded;
	public static MovementType CurrentType {
		get { return _currentType; }
		// sets movement type and animation type
		set {
			_currentType = value; 
			int i = 0;
			// this is where the Animator's integers are defined
			switch (CurrentType)
			{
			case MovementType.Grounded:
				i = 0;
				break;
			case MovementType.Jumping:
				//print (StackTraceUtility.ExtractStackTrace());
				i = 1;
				break;
			case MovementType.Running:
				i = 2;
				break;
			case MovementType.Ziplining:
				GameManager.PLAYER.GetComponent<WASDMovement>().PlaySound (GameManager.PLAYER.GetComponent<WASDMovement>().sounds[3]);
				break;
			}
			GameManager.PLAYER.GetComponentInChildren<Animator>().SetInteger ("AnimationType", i);
		}
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
			if (Input.GetKeyDown (jumpBreak)) 
			{ 
				// jump force
				rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, jumpForce * 2); 
				// jump sound
				PlaySound (sounds[0]);
			}
			GetComponent<Swinging>().DetachFromAnchor();
		}

		// rope length change
		// you can always increase rope length... (TODO: make a max length)
		if (Input.GetAxis ("Vertical") < 0) {
			GetComponent<Swinging>().RopeLength -= lengthChangeRate * Input.GetAxis ("Vertical") * Time.deltaTime;
			if (!GetComponent<AudioSource>().isPlaying)
			{
				GetComponent<AudioSource>().clip = sounds[2];
				GetComponent<AudioSource>().Play();
			}
		}
		// ...but there's a minimum rope length you can't pass
		else if (Input.GetAxis ("Vertical") > 0) {
			if (GetComponent<Swinging>().RopeLength > minRopeLength) {
				GetComponent<Swinging>().RopeLength -= lengthChangeRate * Input.GetAxis ("Vertical") * Time.deltaTime;
				if (!GetComponent<AudioSource>().isPlaying)
				{
					GetComponent<AudioSource>().clip = sounds[2];
					GetComponent<AudioSource>().Play();
				}
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
	public float ziplineRate = 1f;

	public void ZiplineToPoint (Vector2 start, Vector2 end) {
		if (CurrentType == MovementType.Swinging) {
			GetComponent<Swinging>().DetachFromAnchor();
		}
		if (CurrentType != MovementType.Ziplining)
		{
			CurrentType = MovementType.Ziplining;
		}
		rigidbody2D.gravityScale = 0;
		rigidbody2D.drag = 0;
		rigidbody2D.velocity = Vector2.zero;
		goal = end;
	}
	private void ZiplineMovement() {
		// move the player toward the goal
		transform.position = Vector3.MoveTowards (transform.position, goal, ziplineRate * Time.deltaTime);
		// break zipline
		if (Input.GetKeyDown (jumpBreak) || Input.GetKeyDown (fallBreak)) {
			// gives you a boost if you jump
			if (Input.GetKeyDown (jumpBreak)) 
			{ 
				rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, jumpForce * 2); 
				// play jump noise
				PlaySound (sounds[0]);
			}
			goal = transform.position;
		}


		// zipline is close enough to goal, so reset it
		if (Vector2.Distance (transform.position, goal) <= .1f) {
			LeaveZipline();
		}
	}

	private void LeaveZipline () {
		CurrentType = MovementType.Jumping;
		GetComponent<Zipline>().ResetZipline();
		rigidbody2D.gravityScale = 1;
		rigidbody2D.drag = .1f;
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
		// set state to Running - only if grounded and moving
		if (move != 0 && CurrentType == MovementType.Grounded)
		{
			CurrentType = MovementType.Running;
		}
		// set to idle
		else if (move == 0 && CurrentType == MovementType.Running)
		{
			CurrentType = MovementType.Grounded;
		}




		rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);

		// ground-based jumping (no anchor points involved)
		if (Input.GetKeyDown (jumpKey) && CurrentType != MovementType.Jumping){
			CurrentType = MovementType.Jumping;
			rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, jumpForce);
			PlaySound (sounds[0]);
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
	private bool isTouchingGround = false;

	private void OnCollisionEnter2D (Collision2D c) {
		// tag all garden boxes and anything else the player can run/jump on as "Ground"
		if (c.gameObject.CompareTag ("Ground")) {
			// TODO: make this better
			/*
			RaycastHit2D hitDown = Physics2D.Raycast ((Vector2)transform.position + -Vector2.up, -Vector2.up, .5f);
			RaycastHit2D hitUp = Physics2D.Raycast ((Vector2)transform.position + Vector2.up, Vector2.up, .5f);
			// player is on the side of a planter
			if (hitDown.collider == null && hitUp.collider == null)
			{
				//print ("JUMP UP!");
				CurrentType = MovementType.Hanging;
			}
			*/
			// lets the player jump again after landing on ground

			if (CurrentType == MovementType.Jumping)
			{
				CurrentType = MovementType.Grounded;
			}

			if (!isTouchingGround) { isTouchingGround = true; }
		}
	}

	private void OnCollisionExit2D (Collision2D c)
	{
		if (c.gameObject.CompareTag ("Ground"))
		{
			if (isTouchingGround) { isTouchingGround = false; }
		}
	}




	/*
	private void OnCollisionStay2D (Collision2D c) {
		// tag all garden boxes and anything else the player can run/jump on as "Ground"
		if (c.gameObject.tag == "Ground") {
			// lets the player jump again after landing on ground
			if (CurrentType != MovementType.Grounded) {
				if (CurrentType == MovementType.Ziplining) {
					LeaveZipline();
				}
				CurrentType = MovementType.Grounded;

			}
		}
	}
	*/

	private void HangingControls ()
		// TODO: make this actually work
	{
		if (CurrentType == MovementType.Hanging)
		{
			if (Input.GetAxis ("Vertical") > 0)
			{
				CurrentType = MovementType.Jumping;
				rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, jumpForce);
			}
		}
	}

	
	// Update ()
	// ==============

	private void Update () {
		print (CurrentType);
		if (CurrentType == MovementType.Grounded || CurrentType == MovementType.Jumping || CurrentType == MovementType.Running) {
			//this.renderer.enabled = true; // this hides the player sprite when dead, there's probably a better way around but this will do for now
			GroundedControls();
		}
		else if (CurrentType == MovementType.Swinging) {
			//this.renderer.enabled = true;
			AirborneControls();
			//GroundedControls();
		}
		else if (CurrentType == MovementType.Ziplining) {
			//this.renderer.enabled = true;
			ZiplineMovement();
			AirborneControls();
		}

		else if (CurrentType == MovementType.Dead) {
			//this.renderer.enabled = false; //hide the player when you die
		}
		else if (CurrentType == MovementType.TurnDone || CurrentType == MovementType.TurnStart)
		{
			rigidbody2D.velocity = Vector2.zero;
		}
		//CheckSpeed ();
		//HangingControls();
	}
}
