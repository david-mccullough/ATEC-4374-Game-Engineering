using UnityEngine;

public class StartMotor : BaseMotor {
	
	public override void UpdateMotor(CharacterMover mover)
	{
		Debug.Log("Starting");
		RaycastHit hit;
		if (Physics.SphereCast (transform.position, mover.radius, Vector3.down, out hit, mover.radius, mover.collisionMask)) {
			mover.currentState = MovementState.walking; 
		} else {
			mover.currentState = MovementState.falling;
		}
	}

	public override void HandleCollision(CharacterMover mover, ControllerColliderHit hit)
	{
	}
}
