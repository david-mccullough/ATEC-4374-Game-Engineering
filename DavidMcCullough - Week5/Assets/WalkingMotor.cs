using UnityEngine;

public class WalkingMotor : BaseMotor
{
	public override void UpdateMotor(CharacterMover mover)
	{
		Debug.Log("Walking");
	}

	public override void HandleCollision(CharacterMover mover, ControllerColliderHit hit)
	{
	}
}
