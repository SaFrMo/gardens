using UnityEngine;
using System.Collections;

public class FlyingCarMovement : MonoBehaviour {

	private float speed;

	private void Start () {
		speed = UnityEngine.Random.Range (15, 70);
	}

	private void Update () {
		transform.Translate (Vector2.right * speed * Time.deltaTime, transform);
		if (Mathf.Abs(transform.position.x) > 110) {
			Destroy (gameObject);
		}
	}
}
