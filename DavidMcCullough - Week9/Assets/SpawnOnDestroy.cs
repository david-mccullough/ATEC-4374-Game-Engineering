using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DestructableBlock))]

public class SpawnOnDestroy : MonoBehaviour {

	public GameObject prefab;
	public GameObject soundDestroy;
	public float lifetime = 3f;

	void OnBlockDestroyed()
	{
		int i = 0;
		while (i < 3) {
			GameObject newGO = Instantiate (prefab, transform.position, Quaternion.identity);
			i++;
		}

		GameObject newSound = Instantiate(soundDestroy, transform.position, Quaternion.identity);
		Destroy (newSound, lifetime);

	}
}
