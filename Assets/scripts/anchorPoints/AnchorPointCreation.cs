﻿using UnityEngine;
using System.Collections;

public class AnchorPointCreation : MonoBehaviour {

	public GameObject anchorPointPrefab;

	private void CreateAnchorPoint () {

		// create anchor point on R mouse button up
		// TODO: R mouse button held down = slow time and choose location?
		if (Input.GetMouseButtonUp(1)) {
			// select preexisting anchor point if one exists
			// TODO: Drag preexisting anchor point to new location?
			Ray mouseRay = new Ray (Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward);
			RaycastHit mouseHit;
			if (Physics.Raycast (mouseRay, out mouseHit, Mathf.Infinity)) {
				if (mouseHit.transform.gameObject.CompareTag ("Anchor")) {
					mouseHit.collider.gameObject.GetComponent<AnchorPoint>().SelectThisAnchorPoint();
				}
			}
			// create new anchor if player clicked on an empty space
			else {
				GameObject newPoint = GameObject.Instantiate (anchorPointPrefab) as GameObject;
				newPoint.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				newPoint.transform.position = new Vector3 (newPoint.transform.position.x,
				                                           newPoint.transform.position.y,
				                                           0);


				// set new anchor point as AnchorPoint.Current
				newPoint.GetComponent<AnchorPoint>().SelectThisAnchorPoint();
			}

			// attach to the newly created or selected anchor point
			GetComponent<Swinging>().AttachToAnchor();
		}
	}

	protected void Update () {
		CreateAnchorPoint();
	}
}
