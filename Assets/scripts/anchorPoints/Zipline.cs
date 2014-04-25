using UnityEngine;
using System.Collections;

public class Zipline : MonoBehaviour {

	private Vector2 ziplineStart = Vector2.zero;
	private Vector2 ziplineEnd = Vector2.zero;

	public void ResetZipline () {
		ziplineStart = Vector2.zero;
		ziplineEnd = Vector2.zero;
	}

	void CreateZipline() {
		// create a zipline starting point if one doesn't exist
		if (ziplineStart == Vector2.zero) {
			ziplineStart = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		}

		// endpoint for the zipline
		ziplineEnd = Camera.main.ScreenToWorldPoint (Input.mousePosition);

		Debug.DrawRay (ziplineStart, ziplineEnd);


	}

	private void Update () {
		// slowed-down time is activated and the right mouse button is still down
		if (SlowTime.SLOW_MOTION && Input.GetMouseButton(1)) {
			CreateZipline();
		}
		// there's a zipline present and the player released RMouse
		else if (ziplineStart != Vector2.zero && Input.GetMouseButtonUp(1)) {
			GetComponent<WASDMovement>().ZiplineToPoint (ziplineStart, ziplineEnd);
		}
	}
}
