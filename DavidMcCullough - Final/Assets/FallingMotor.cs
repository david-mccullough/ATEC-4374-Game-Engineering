using UnityEngine;

public class FallingMotor : BaseMotor {
	
	public override void UpdateMotor(CharacterMover mover)
	{
		mover.Accelerate(mover.accelDir, mover.velocity, mover.airAccel, mover.maxAirSpeed);

		Vector3 gravity = new Vector3(0f, mover.gravity, 0f);
        mover.velocity += gravity * Time.deltaTime;

		//variable height
		if (mover.krJump && mover.velocity.y > 0f)
		{
			float tempY = mover.velocity.y;
			Vector3 tempVel = new Vector3 (mover.velocity.x, tempY * 0.25f, mover.velocity.z);
			mover.velocity = tempVel;
		}

		mover.charController.Move (mover.velocity);
    }

	public override void HandleCollision(CharacterMover mover, ControllerColliderHit hit)
	{
		if (mover.IsGrounded())
		{
			mover.velocity = new Vector3 (mover.velocity.x, 0f, mover.velocity.z);
			mover.currentState = MovementState.walking;
			Debug.Log ("Switch to walking");
		}
    }
}
