using UnityEngine;

public class BallBehavior : MonoBehaviour {

	public float gravity = .2f;
	public Vector3 velocity = new Vector3(0f, 0f, 0f);
	public float maximumVelocity = .15f;
	public float radius = .5f;

	public float tailDuration = 10f;
	public Color color = Color.cyan;

	void Update () 
	{
		Vector3 selfPosition = transform.position;
		Vector3 travelDir = velocity.normalized;
		float travelDist = velocity.magnitude;

		//Add gravity to acceleration
		velocity += Vector3.down * (gravity * Time.deltaTime);

		int collisionCount = 5;
		while (collisionCount > 0)
		{
			RaycastHit hit;
			if (Physics.SphereCast (selfPosition, radius, velocity.normalized, out hit, travelDist))
			{
				//Check for collision with special colliders
				if (hit.collider.tag == "Catcher")
				{
					hit.transform.SendMessage("CatchBall", this.gameObject);
					break;
				}
				if (hit.collider.tag == "Bumper")
				{
					velocity *= 2f;
				}
				if (hit.collider.tag == "Pickup")
				{
					hit.transform.SendMessage("BallGet", this.gameObject);
					break;
				}

				selfPosition += travelDir * (hit.distance - 0.000001f);
				velocity = Vector3.Reflect (velocity*.9f, hit.normal);
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