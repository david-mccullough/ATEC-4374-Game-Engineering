using UnityEngine;

public class FallingMotor : BaseMotor {
	
	public override void UpdateMotor(CharacterMover mover)
	{
		Debug.Log("Falling");
        Vector3 gravity = new Vector3(0f, -9.8f, 0f);
        mover.velocity += gravity * Time.deltaTime;

       
    }

	public override void HandleCollision(CharacterMover mover, ControllerColliderHit hit)
	{
	}
}
