using UnityEngine;

public class BouncyBall : MonoBehaviour {

	public float radius;
	public LayerMask collisionMask;

	public Vector3 gravity = new Vector3(0f, -9.8f, 0f);
	public Vector3 velocity;

		
	// Update is called once per frame
	void Update ()
	{
		UpdateVelocity ();
		UpdatePosition ();
	}

	void UpdateVelocity()
	{
		velocity += gravity * Time.deltaTime;
	}

	void UpdatePosition()
	{
		Vector3 startPos = transform.position;
		Vector3 currentPos = startPos;

		Vector3 moveDelta = velocity * Time.deltaTime;

		RaycastHit hit;
		if (Physics.SphereCast(startPos, radius, moveDelta.normalized, out hit, moveDelta.magnitude, collisionMask))
		{
			currentPos += moveDelta.normalized * hit.distance;

			velocity = Vector3.Reflect(velocity, hit.normal);
		}
		else
		{
			currentPos += moveDelta;
		}

		transform.position = currentPos;
	}
}
