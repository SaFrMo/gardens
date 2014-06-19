using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Bird : MonoBehaviour {

	public float speed = 2f;
	public float pauseLength = 2f;
	public float eatRate = 1f;

	private GrowingPlant _targetPlant = null;
	private Timer landAndRest = null;
	private Timer eat = null;
	private Vector2 origin;

	private void Start ()
	{
		origin = transform.position;
	}

	// What's the bird currently doing?
	public Status MyStatus { get; private set; }
	public enum Status
	{
		FlyingTo,
		Landed,
		Eating,
		Leaving
	}

	// Land on the planter
	private void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject == _targetPlant.container.gameObject && MyStatus == Status.FlyingTo) 
		{
			MyStatus = Status.Landed;
		}
		else if (coll.gameObject == GameManager.PLAYER)
		{
			MyStatus = Status.Leaving;
		}
	}

	private void Update ()
	{
		// wait offstage till there's a plant to eat. diabolical.
		if (_targetPlant == null && Planter.ALL_PLANTS.Count != 0) {
			int targetIndex = UnityEngine.Random.Range (0, Planter.ALL_PLANTS.Count);
			_targetPlant = Planter.ALL_PLANTS[targetIndex];
			MyStatus = Status.FlyingTo;
		}
		switch (MyStatus)
		{

		case Status.FlyingTo:
			rigidbody2D.MovePosition(Vector2.MoveTowards((Vector2)transform.position, (Vector2)_targetPlant.container.transform.position, speed * Time.deltaTime));
			break;

		case Status.Landed:
			if (landAndRest == null)
				landAndRest = new Timer(pauseLength);
			if (landAndRest.RunTimer())
				MyStatus = Status.Eating;
			break;

		case Status.Eating:
			rigidbody2D.velocity = Vector2.zero;
			if (eat == null)
			{
				print ("CHOMPING TIME");
				eat = new Timer(eatRate, true);
			}
			if (eat.RunTimer())
			{
				print ("YUM");
			}
			break;

		case Status.Leaving:
			rigidbody2D.MovePosition(Vector2.MoveTowards((Vector2)transform.position, origin, speed * Time.deltaTime));
			break;
		}

	}
}
