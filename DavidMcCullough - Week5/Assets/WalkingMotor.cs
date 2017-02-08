using UnityEngine;

public class WalkingMotor : BaseMotor
{
	public override void UpdateMotor(CharacterController mover)
	{
		Debug.Log("Walking");
	}

	public override void HandleCollision(CharacterController mover, ControllerColliderHit hit)
	{
	}
}
