using UnityEngine;

public class FindDirection : MonoBehaviour {

	public Transform target;
	public float offsetLength = 1f;
	public Color color = Color.cyan;
	public float duration;

	// Update is called once per frame
	void Update () {

		if (target == null)
			return;

		Vector3 targetPosition = target.position;
		Vector3 selfPosition = transform.position;
		//transform.position is shorthand for GetComponent<Transfrom>.position

		Vector3 relativeOffset = targetPosition - selfPosition;

		Vector3 relativeDirection = relativeOffset.normalized * offsetLength;

		Debug.DrawRay (selfPosition, relativeDirection, color, duration, true); 
		Debug.DrawRay (selfPosition + relativeDirection, Vector3.up, color, duration, false);
		//Debug.DrawRay (target.position, relativeOffset, color, duration); 
	}
}
