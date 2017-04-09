using UnityEngine;

public class WalkingMotor : BaseMotor
{
	public override void UpdateMotor(CharacterMover mover)
	{ 
		// Apply Friction
		float speed = mover.velocity.magnitude;
		if (speed != 0) // To avoid divide by zero errors
		{
			float drop = speed * mover.friction * Time.fixedDeltaTime;
			mover.velocity *= Mathf.Max(speed - drop, 0) / speed; // Scale the velocity based on friction.
		}

		// ground_accelerate and max_velocity_ground are server-defined movement variables
		mover.Accelerate(mover.accelDir, mover.velocity, mover.groundAccel, mover.maxGroundSpeed);

		//jumping
		if (mover.kpJump)
		{
			float yVel = mover.jumpSpeed;
			mover.velocity += new Vector3 (0f, yVel, 0f);
		}
		//variable height
		if (mover.krJump && mover.velocity.y > 0)
		{
			float tempY = mover.velocity.y;
			Vector3 tempVel = new Vector3 (mover.velocity.x, tempY * 0.25f, mover.velocity.z);
			mover.velocity = tempVel;
		}

		mover.charController.Move (mover.velocity);			

	}
		
	public override void HandleCollision(CharacterMover mover, ControllerColliderHit hit)
	{
		//floor no longer beneath us, switch state
		if (mover.IsGrounded ()) {
			mover.currentState = MovementState.falling;
			Debug.Log ("Switch to falling");
		}
	}
}
