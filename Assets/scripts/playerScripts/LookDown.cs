using UnityEngine;
using System.Collections;

public class LookDown : MonoBehaviour {

	// Cast a ray downward from player and see if it hits a planter

	private SpriteRenderer _arrow;
	public float range = 10f;

	Timer t = new Timer (.1f, true);

	private void Start ()
	{
		_arrow = GameObject.Find ("Look Down Arrow").GetComponent<SpriteRenderer>();
	}

	private void Update ()
	{
		if (t.RunTimer())
		{
			RaycastHit2D hit = Physics2D.Raycast ((Vector2)transform.position - Vector2.up, -Vector2.up, range);
			if (hit.collider != null && hit.collider.CompareTag ("Ground")) 
			{
				_arrow.renderer.enabled = true;
			}
			else if (hit.collider == null || !hit.collider.CompareTag ("Ground"))
			{
				_arrow.renderer.enabled = false;
			}
		}
	}
}
