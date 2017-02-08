using UnityEngine;

public class StartMotor : BaseMotor {
	
	public override void UpdateMotor(CharacterMover mover)
	{
		Debug.Log("Starting");
	}

	public override void HandleCollision(CharacterMover mover, ControllerColliderHit hit)
	{
	}
}
