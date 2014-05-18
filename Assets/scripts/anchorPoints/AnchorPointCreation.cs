using UnityEngine;
using System.Collections;

public class AnchorPointCreation : MonoBehaviour {

	public GameObject anchorPointPrefab;

	private void CreateAnchorPoint () {

		// create anchor point on R mouse button up
		// TODO: R mouse button held down = choose location?
		if (Input.GetMouseButtonUp(1) )
		{
			// play grappling hook sound. TODO: fix - this is ugly
			GetComponent<WASDMovement>().PlaySound (GetComponent<WASDMovement>().sounds[1]);
			if (GetComponent<WASDMovement>().CurrentType != WASDMovement.MovementType.Ziplining) {
				// select preexisting anchor point if one exists
				// TODO: Drag preexisting anchor point to new location?
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

				if (hit.collider != null && hit.collider.gameObject.CompareTag ("Anchor"))
				{
					hit.collider.gameObject.GetComponent<AnchorPoint>().SelectThisAnchorPoint();
				}

				// create new anchor if player clicked on an empty space
				else {
					GameObject newPoint = GameObject.Instantiate (anchorPointPrefab) as GameObject;
					newPoint.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
					newPoint.transform.position = new Vector2 (newPoint.transform.position.x,
					                                           newPoint.transform.position.y);


					// set new anchor point as AnchorPoint.Current
					newPoint.GetComponent<AnchorPoint>().SelectThisAnchorPoint();
				}

				// attach to the newly created or selected anchor point
				GetComponent<Swinging>().AttachToAnchor();
			}
		}

	}

	protected void Update () {
		CreateAnchorPoint();
	}
}
