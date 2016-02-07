using UnityEngine;
using System.Collections;

public class EnemyController : ShipBase
	{
	public float rotationSpeed;
	public float maxSpeed;

	public Vector3 driveRotation;

	private bool spawnCompleted = false;

	// Use this for initialization
	void Start ()
		{
		spawnCompleted = false;
		OnStart();
		nextTime = Time.time + Random.Range(reloadTime, reloadTime * 2);
		}
	
	// Update is called once per frame
	// Called once per frame
	void Update ()
		{
		if (Time.time > nextTime)
			{
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			nextTime = Time.time + Random.Range(reloadTime, reloadTime * 10);
			AudioSource audio = GetComponent<AudioSource>() as AudioSource;
			audio.Play();
			}
		}

	// Called once per physics update
	void FixedUpdate ()
		{
		if (spawnCompleted == false)
			{
			if (HandleIfSpawning())
				return;
			spawnCompleted = true;
			Vector3 pos = new Vector3 (body.position.x, 0.0f, body.position.z);
			body.MovePosition(pos);
			body.velocity.Set(Random.Range(-maxSpeed, maxSpeed), 0.0f, Random.Range(-maxSpeed, maxSpeed));
			}

		Quaternion deltaRotation;
		deltaRotation = Quaternion.Euler(driveRotation * rotationSpeed);
		body.MoveRotation(body.rotation * deltaRotation);

		Vector3 force = transform.forward * maxSpeed;
		body.AddForce(force);
		}

	}
