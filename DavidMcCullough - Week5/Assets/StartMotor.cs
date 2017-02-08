using UnityEngine;

public class StartMotor : BaseMotor {
	
	public override void UpdateMotor(CharacterController mover)
	{
		Debug.Log("Starting");
	}

	public override void HandleCollision(CharacterController mover, ControllerColliderHit hit)
	{
	}
}
