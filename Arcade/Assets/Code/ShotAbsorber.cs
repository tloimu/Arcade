using UnityEngine;
using System.Collections;

public class ShotAbsorber : MonoBehaviour
	{
	public bool destroyOnEntry;

	void OnTriggerEnter (Collider collider)
		{
		if (destroyOnEntry)
			DestroyIfShot(collider);
		}

	void OnTriggerExit (Collider collider)
		{
		if (!destroyOnEntry)
			DestroyIfShot(collider);
		}

	void DestroyIfShot (Collider collider)
		{
		if (collider.gameObject.tag == "shot")
			{
			Destroy(collider.gameObject);
			AudioSource audio = GetComponent<AudioSource>() as AudioSource;
			if (audio != null)
				audio.Play();
			}
		}
	}
