using UnityEngine;
using System.Collections;

public class Passerby : MonoBehaviour {
	
	public bool walkLeft = true;
	public float speed = 3f;

	// TODO: add more: impressed, astounded, awed, worshipful, etc.
	public enum State {
		Coming,
		Looking,
		Going
	}
	public State MyState { get; private set; }

	private Vector2 targetPosition;

	private void Start () {
		MyState = State.Coming;
		float targetDistance = UnityEngine.Random.Range (3, 10);
		targetPosition = (Vector2)transform.position + (walkLeft ? -Vector2.right * targetDistance : Vector2.right * targetDistance);
	}

	private void Update () {
		switch (MyState) {
		case State.Coming:
			transform.position = Vector2.MoveTowards((Vector2)transform.position, targetPosition, speed * Time.deltaTime);
			if (SaFrMo.CloseEnough ((Vector2)transform.position, (Vector2)targetPosition, .3f)) {
				MyState = State.Looking;
			}
			break;

		case State.Looking:
			// is your puny garden even worth my time?!
			print ("Hmm...");
			MyState = State.Going;
			break;

		case State.Going:
			Vector2 whichWay = walkLeft ? -Vector2.right : Vector2.right;
			transform.position = Vector2.MoveTowards((Vector2)transform.position, (Vector2)transform.position + (Vector2)whichWay, speed * Time.deltaTime);
			float gone = Camera.main.WorldToViewportPoint(transform.position).x;
			// destroy when far enough offscreen
			if (gone > 2 || gone < -1) {
				Destroy (gameObject);
			}
			break;
		}
	}

}
