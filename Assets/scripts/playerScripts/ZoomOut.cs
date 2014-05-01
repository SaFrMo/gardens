using UnityEngine;
using System.Collections;

public class ZoomOut : MonoBehaviour {

	public float maxCameraSize;
	public float goalSize;
	private float originalCameraSize;
	public KeyCode zoomKey = KeyCode.Tab;
	public float rate = 3f;

	private void Start ()
	{
		originalCameraSize = Camera.main.orthographicSize;
	}

	private void Zoom ()
	{
		if (Input.GetKeyDown (zoomKey))
		{
			if (goalSize != maxCameraSize) { goalSize = maxCameraSize; }
			else { goalSize = originalCameraSize; }
		}

		if (Input.GetAxis ("Mouse ScrollWheel") != 0 && !Catalog.showPlantCatalog)
		{
			goalSize -= Input.GetAxis ("Mouse ScrollWheel") * 6;
		}

		goalSize = Mathf.Clamp (goalSize, originalCameraSize, maxCameraSize);

		if (Camera.main.orthographicSize != goalSize)
		{
			Camera.main.orthographicSize = Mathf.Lerp (Camera.main.orthographicSize, goalSize, rate * Time.deltaTime);
		}
	}

	private void Update ()
	{
		Zoom();
	}
}
