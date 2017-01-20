using UnityEngine;

public class AccelerateChaser : MonoBehaviour {

	public Transform target;
	public float speed = .5f;
	public Vector3 velocity = new Vector3(0f, 0f, 0f);
	public float acceleration = 1f;
	public float maximumVelocity = .15f;

	public float offsetLength = 1f;
	public Color color = Color.cyan;

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
		Debug.DrawRay (selfPosition, relativeDirection * offsetLength, color);

		velocity += relativeDirection * (speed * Time.deltaTime);

		//clamp velocity to maximumVelocity
		velocity = Vector3.ClampMagnitude(velocity, maximumVelocity);

		//Move self toward target at accelerating velocity
		Vector3 newPosition = selfPosition + velocity;
		transform.position = newPosition;
	}
}