using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BlockDigger))]

public class SpawnOnMine : MonoBehaviour {

	public GameObject particlePrefab;
	public GameObject soundHit;
	public float lifetime = 3f;
	public float mineRate = 1f;
	float nextMineTime = 0f;

	void OnDigHit()
	{
		if (nextMineTime > Time.time)
			return;

		nextMineTime = Time.time + mineRate;

		RaycastHit mineHit = GetComponent<BlockDigger>().GetLastHit();
		GameObject newGO = Instantiate(particlePrefab, mineHit.point, Quaternion.LookRotation(mineHit.normal));
		GameObject newSound = Instantiate(soundHit, mineHit.point, Quaternion.identity);

		Destroy (newSound, lifetime);
		Destroy(newGO, lifetime);
	}

}
