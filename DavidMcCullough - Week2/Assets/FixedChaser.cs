using UnityEngine;

public class FixedChaser : MonoBehaviour {

	public Transform target;
	public float speed = 5f;
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

		Vector3 velocity = relativeDirection * (speed * Time.deltaTime);

		//Move self toward target at fixed velocity if out of range
		if (relativeOffset.magnitude > .1f)
		{
			Vector3 newPosition = selfPosition + velocity;
			transform.position = newPosition;
		}
	}
}