using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {
	public float maxSpeed = 10;
	private bool facingRight = true;

	// Use this for initialization
	void Start () {

	}



	
	// Update is called once per frame
	void FixedUpdate () {

		float move = Input.GetAxis("Horizontal");
		rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);


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
	void Flip() 
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1; //inverts x scale to flip sprite
		transform.localScale = theScale;
	}
}
