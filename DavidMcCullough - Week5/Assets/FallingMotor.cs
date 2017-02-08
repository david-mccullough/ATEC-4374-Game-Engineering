using UnityEngine;

public class FallingMotor : BaseMotor {
	
	public override void UpdateMotor(CharacterMover mover)
	{
		Debug.Log("Falling");
	}

	public override void HandleCollision(CharacterMover mover, ControllerColliderHit hit)
	{
	}
}
