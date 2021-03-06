﻿using UnityEngine;

public class BallBehavior : MonoBehaviour {

	public float gravity = .25f;
	public Vector3 velocity = new Vector3(0f, 0f, 0f);
	public float maximumVelocity = .15f;
	public float radius = .5f;

	public float tailDuration = 5f;
	public Color color = Color.cyan;

	void Update () 
	{
		Vector3 selfPosition = transform.position;
		Vector3 travelDir = velocity.normalized;
		float travelDist = velocity.magnitude;

		//Add gravity to acceleration
		velocity += Vector3.down * (gravity * Time.deltaTime);

		int collisionCount = 3;
		while (collisionCount > 0)
		{
			RaycastHit hit;
			if (Physics.SphereCast (selfPosition, radius, velocity.normalized, out hit, travelDist)) {
				selfPosition += travelDir * (hit.distance - 0.1f);
				velocity = Vector3.Reflect (velocity, hit.normal);
				Debug.Log ("Hit surface");
			}
			else
			{
				selfPosition += travelDir * travelDist;
				break;
			}
			collisionCount--;
		}

		//clamp velocity to maximumVelocity
		velocity = Vector3.ClampMagnitude(velocity, maximumVelocity);

		//Move self toward target at accelerating velocity
		Vector3 newPosition = selfPosition;
		transform.position = newPosition;

		//Draw motion trail
		Debug.DrawRay (selfPosition, -velocity, color, tailDuration, true);
	}
}