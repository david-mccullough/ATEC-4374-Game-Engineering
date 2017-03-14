using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBehavior : MonoBehaviour {

	public float gravity = .25f;
	Vector3 velocity;
	public float maximumVelocity = .15f;
	public float radius = .5f;
	public float minerDist = 1.5f;

	float liveTime = 1f;

	private GameObject miner;

	public Material[] cols;


	void Awake()
	{
		//Assign color
		GetComponent<Renderer>().material = cols[(int)Random.Range(0,3)];

		velocity = new Vector3(Random.Range(-.05f, .05f), 0f, Random.Range(-.05f,.05f));
		miner = GameObject.Find("MiningCharacter");
	}

	void Update () 
	{
		if (liveTime > 0f)
			liveTime -= Time.deltaTime;

		Vector3 selfPosition = transform.position;
		Vector3 travelDir = velocity.normalized;
		float travelDist = velocity.magnitude;

		//Add gravity to acceleration
		velocity += Vector3.down * (gravity * Time.deltaTime);

		int collisionCount = 1;
		while (collisionCount > 0)
		{
			RaycastHit hit;
			if (Physics.SphereCast (selfPosition, radius, velocity.normalized, out hit, travelDist)) {
				selfPosition += travelDir * (hit.distance - .001f);
				velocity = Vector3.zero;
			}
			else
			{
				selfPosition += travelDir * travelDist;
				break;
			}
			collisionCount--;
		}

		if (liveTime <= 0f && Vector3.Distance(selfPosition, miner.transform.position) < minerDist)
		{
			selfPosition = Vector3.MoveTowards(selfPosition, miner.transform.position, .1f);
			if (Vector3.Distance (selfPosition, miner.transform.position) < 0.25f)
				Destroy (this.gameObject);
		}

		//clamp velocity to maximumVelocity
		velocity = Vector3.ClampMagnitude(velocity, maximumVelocity);

		//Move self toward target at accelerating velocity
		Vector3 newPosition = selfPosition;
		transform.position = newPosition;
	}
}
