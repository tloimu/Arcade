using UnityEngine;
using System.Collections;

public class ShotMover : MonoBehaviour
	{

	public float speed;

	private Rigidbody body;

	// Use this for initialization
	void Start ()
		{
		body = GetComponent<Rigidbody>();
		body.velocity = transform.forward * speed;
		}
	
	// Update is called once per frame
	void Update ()
		{
	
		}

	public GameObject enemyExplosion;
	public GameObject playerExplosion;

	void OnTriggerEnter (Collider collider)
		{
		DestroyIfShot(collider);
		}

	void DestroyIfShot (Collider collider)
		{
		if (collider.gameObject.tag == "Player")
			{
			Instantiate(playerExplosion, collider.transform.position, collider.transform.rotation);
			if (GameController.game != null)
				GameController.game.PlayerKilled(collider.gameObject);
			Destroy(gameObject);
			Destroy(collider.gameObject);
			} else if (collider.gameObject.tag == "Enemy")
			{
			if (GameController.game != null)
				GameController.game.EnemyKilled(collider.gameObject);
			Instantiate(enemyExplosion, transform.position, transform.rotation);
			Destroy(gameObject);
			Destroy(collider.gameObject);
			}
		}
	}
