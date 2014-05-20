using UnityEngine;
using System.Collections;
 
[RequireComponent(typeof(BoxCollider))]
 
public class MovePoint : MonoBehaviour 
{
 
	private Vector3 screenPoint;
	private Vector3 offset;
	
	
	public bool snapTo = false;
	public GameObject toSnapTo;
	
	Vector3 originalPosition;
	//public GameObject background;
	//GameObject originalParent;
	
	void Start () {
		originalPosition = transform.localPosition;
	}
	 
	void OnMouseDown()
	{
	    screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		print ("clicked!");
	 
	    offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	 	
		
	}
	 
	void OnMouseDrag() {
	    Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
	 
		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
		transform.position = curPosition;
	 
	}
	
	void OnMouseUp () {
		if (snapTo) {
			Vector3 rayPos = new Vector3 (transform.position.x, transform.position.y, transform.position.z + 0.1f);
			Ray myRay = new Ray (rayPos, Vector3.forward * 5);
			RaycastHit myHit = new RaycastHit();
			
			if (Physics.Raycast (myRay, out myHit)) {
				if (myHit.transform.name != toSnapTo.transform.name) {
					transform.parent = null;
				}
				else {
					transform.parent = toSnapTo.transform;
					transform.localPosition = originalPosition;
				}
				
			}
			
			
			
			
			
		}
	}
	
	//void Update () {
		//print (transform.parent);
		//Debug.DrawRay(transform.position, Vector3.forward * 5);
		/*if (snapTo) {
			Ray myRay = new Ray (transform.position, Vector3.forward * 5);
			RaycastHit myHit;
			
			
			if (myHit.collider.name == originalParent.collider.name) {
				transform.parent = originalParent.transform;
			}
		}*/
	//}
			
}
