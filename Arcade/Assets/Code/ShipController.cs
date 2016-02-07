using UnityEngine;
using System.Collections;


// Class: InputHandler
//	Class for transforming a raw input to actual game play input.
[System.Serializable]
public class InputHandler
	{
	public string axis;
	public float attack;
	public float decay;
	public float coeff;
	public float deadzone;

	private float current, target;

	public float get ()
		{
		setTarget(Input.GetAxis(axis));

		current = target * coeff;
		return current;
		}

	protected void setTarget (float inTarget)
		{
		if (inTarget < deadzone && inTarget > -deadzone)
			target = 0.0f;
		else
			target = inTarget;
		}
	};

public class ShipController : ShipBase
	{
	// Throttle settings
	public float throttleBoost;

	// Rotation settings
	public InputHandler controlThrottle;
	public InputHandler controlRotation;
	public Vector3 driveRotation;

	public GameObject playerExplosion;

	// Controller settings
	public string ButtonBoost, ButtonFire;

	// Called at startup
	void Start ()
		{
		OnStart();
		gameController.gameText.text = "Ready!";
		}

	// Called once per frame
	void Update ()
		{
		if (Input.GetButton(ButtonFire) && Time.time > nextTime)
			{
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			nextTime = Time.time + reloadTime;
			AudioSource audio = GetComponent<AudioSource>() as AudioSource;
			audio.Play();
			}
		}

	// Called once per physics update
	void FixedUpdate ()
		{
		HandleIfSpawning();

		// Handle throttle
		float driveForce = 1.0f;
		if (Input.GetButton(ButtonBoost))
			driveForce = throttleBoost;

		Vector3 force = transform.forward * driveForce * controlThrottle.get();
		body.AddForce(force);

		// Handle rotations
		Quaternion deltaRotation;
		deltaRotation = Quaternion.Euler(driveRotation * controlRotation.get());
		body.MoveRotation(body.rotation * deltaRotation);
		}

	// Called after moving all objects have been done for a frame or physics update
	void LateUpdate ()
		{
		//Camera.main.transform.LookAt(target.transform);
		}

	public void OnTriggerEnter (Collider other)
		{
		base.OnTriggerEnter(other);

		if (other.gameObject.tag == "Enemy")
			{
			Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
			GameController.game.PlayerKilled(other.gameObject);
			Destroy(other.gameObject);
			Destroy(gameObject);
			}
		}

	}
