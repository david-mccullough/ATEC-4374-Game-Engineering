using UnityEngine;

public class BallBehavior : MonoBehaviour {

	public float gravity = .5f;
	public Vector3 velocity = new Vector3(0f, 0f, 0f);
	public float maximumVelocity = .15f;

	public float tailDuration = 5f;
	public Color color = Color.cyan;

	void Update () 
	{
		Vector3 selfPosition = transform.position;

		//Add gravity to acceleration
		velocity += Vector3.down * (gravity * Time.deltaTime);

		RaycastHit hit;
		if (Physics.SphereCast(selfPosition, 1f, velocity, out hit, velocity.magnitude))
		{
			velocity += Vector3.Reflect (velocity, hit.normal);
		}

		//clamp velocity to maximumVelocity
		velocity = Vector3.ClampMagnitude(velocity, maximumVelocity);

		//Move self toward target at accelerating velocity
		Vector3 newPosition = selfPosition + velocity;
		transform.position = newPosition;

		//Draw motion trail
		Debug.DrawRay (selfPosition, Vector3.one, color, tailDuration, true);
	}
}