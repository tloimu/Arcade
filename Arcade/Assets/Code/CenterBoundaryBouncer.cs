using UnityEngine;
using System.Collections;

public class CenterBoundaryBouncer : MonoBehaviour
	{
	public float force;
	/*
	void OnTriggerStay(Collider other)
		{
		Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
		Transform t = rb.transform;
		float other_x = t.position.x;
		float other_z = t.position.z;

		float my_left = transform.position.x - transform.lossyScale.x;
		float my_right = transform.position.x + transform.lossyScale.x;
		float my_top = transform.position.z - transform.lossyScale.z;
		float my_bottom = transform.position.z + transform.lossyScale.z;

		if (other_x >= my_left)
			rb.AddForce(new Vector3 (-1.0f, 0.0f, 0.0f) * force * (my_left - other_x));
		else if (other_x <= my_right)
			rb.AddForce(new Vector3 (1.0f, 0.0f, 0.0f) * force * (other_x - my_right));
		else if (other_z >= my_top)
			rb.AddForce(new Vector3 (-1.0f, 0.0f, 0.0f) * force * (my_top - other_z));
		else if (other_z <= my_bottom)
			rb.AddForce(new Vector3 (1.0f, 0.0f, 0.0f) * force * (other_z - my_bottom));
		else
			t.position = new Vector3 (-10.0f, 0.0f, 0.0f);
		}*/

/*
	void OnTriggerStay(Collider other)
		{
		Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
		rb.AddForce(new Vector3 (-1.0f, 0.0f, 0.0f) * 10.0f);
		}
*/
	}
