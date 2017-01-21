using UnityEngine;

public class BrakeChaser : MonoBehaviour {

	public Transform target;
	public float acceleration = .1f;
	public Vector3 velocity = Vector3.zero;
	public float deacceleration = .01f;
	public float maximumVelocity = .15f;


	public float offsetLength = 1f;
	public Color color = Color.cyan;

	private Vector3 lastVelocity;
	private bool isBraking = false;

	// Update is called once per frame
	void Update () 
	{
		if (target == null)
			return;

		Vector3 targetPosition = target.position;
		Vector3 selfPosition = transform.position;

		Vector3 relativeOffset = targetPosition - selfPosition;
		// Finding direction from self to target position
		Vector3 relativeDirection = relativeOffset.normalized;
		//Draw relative direction at self
		Debug.DrawRay (selfPosition, velocity.normalized * offsetLength, color);


		//Check angle between normalized vectors
		float angle = Vector3.Dot(velocity.normalized, relativeDirection);

		// if angle is more than 45 degree off course, begin braking
		if (angle < .5f && !isBraking) 
		{
			isBraking = true;
			lastVelocity = velocity;
			Debug.Log ("Braking!");
		}

		if (isBraking && velocity != Vector3.zero)
		{
			//Deaccelerate
			velocity -= deacceleration*lastVelocity;
			color = Color.red;
		}
		else
		{
			//Accelerate
			isBraking = false;
			velocity += (relativeDirection * (acceleration * Time.deltaTime));
			color = Color.cyan;
		}

		//clamp velocity to maximumVelocity
		velocity = Vector3.ClampMagnitude(velocity, maximumVelocity);

		//Move self toward target at accelerating velocity
		Vector3 newPosition = selfPosition + velocity;
		transform.position = newPosition;
	}
}