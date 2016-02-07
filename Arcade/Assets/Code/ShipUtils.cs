using UnityEngine;
using System.Collections;


class Bouncer
	{
	public enum Direction
		{
		top,
		bottom,
		left,
		right
		}

	public enum BounceTowards
		{
		Outwards,
		Inwards
		}

	private static Direction CalcRectSide (float x, float z, Transform r)
		{
		Transform transform = r;

		float dx = x - transform.position.x;
		float dz = z - transform.position.z;

		float w = transform.lossyScale.x;
		float h = transform.lossyScale.z;

		float f1 = dx * (h / w);
		float f2 = -f1;

		if (dz >= f1)
			{
			if (dz >= f2)
				return Direction.top;
			else
				return Direction.right;
			} else
			{
			if (dz >= f2)
				return Direction.left;
			else
				return Direction.bottom;
			}
		}


	public static void Bounce (Rigidbody mover, Transform obstacle, BounceTowards bounceTo)
		{
		Vector3 direction;

		Direction side = CalcRectSide(mover.transform.position.x, mover.transform.position.z, obstacle);

		float dirCoeff = 1.0f;
		if (bounceTo == BounceTowards.Inwards)
			dirCoeff = -1.0f;

		if (side == Bouncer.Direction.top)
			direction = new Vector3 (mover.velocity.x, 0.0f, dirCoeff * Mathf.Abs(mover.velocity.z));
		else if (side == Bouncer.Direction.bottom)
			direction = new Vector3 (mover.velocity.x, 0.0f, -dirCoeff * Mathf.Abs(mover.velocity.z));
		else if (side == Bouncer.Direction.left)
			direction = new Vector3 (dirCoeff * Mathf.Abs(mover.velocity.x), 0.0f, mover.velocity.z);
		else if (side == Bouncer.Direction.right)
			direction = new Vector3 (-dirCoeff * Mathf.Abs(mover.velocity.x), 0.0f, mover.velocity.z);
		else
			direction = new Vector3 (0.0f, 0.0f, 0.0f);

		mover.velocity = direction;
		}

	};

[System.Serializable]
public class ShipBase : MonoBehaviour
	{
	public GameObject ship;

	public GameObject shot;
	public Transform shotSpawn;
	public float reloadTime;

	protected float nextTime;

	protected Rigidbody body;
	protected GameController gameController;

	// Called at startup
	protected void OnStart ()
		{
		nextTime = 0.0f;
		body = GetComponent<Rigidbody>();
		gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
		}

	// Handle collisions

	protected void OnTriggerEnter (Collider other)
		{
		if (other.gameObject == gameController.boundCenter)
			Bouncer.Bounce(body, other.transform, Bouncer.BounceTowards.Outwards);
		}

	protected void OnTriggerExit (Collider other)
		{
		if (other.gameObject == gameController.boundOuter)
			Bouncer.Bounce(body, other.transform, Bouncer.BounceTowards.Inwards);
		}

	protected bool HandleIfSpawning ()
		{
		if (body.position.y > 0.0f)
			{
			Vector3 dirY = new Vector3 (0.0f, 2.5f, 0.0f);
			body.MovePosition(body.position - dirY * Time.deltaTime);
			return true;
			}
		return false;
		}
	}
