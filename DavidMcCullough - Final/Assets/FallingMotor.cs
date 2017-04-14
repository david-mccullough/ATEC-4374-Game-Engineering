using UnityEngine;

public class FallingMotor : BaseMotor {
	
	public override void UpdateMotor(CharacterMover mover)
	{
		mover.velocity = mover.Accelerate(mover.accelDir, mover.velocity, mover.airAccel, mover.maxAirSpeed);

		//variable height
		if (mover.krJump && mover.velocity.y > 0f)
		{
			mover.gravity= new Vector3(0f, -0.6f, 0f);
		}
		if (mover.velocity.y <= 0f)
		{
			mover.gravity= new Vector3(0f, -0.3f, 0f);
		}

		mover.velocity += mover.gravity * Time.deltaTime;

		mover.charController.Move (mover.velocity);
		if (mover.IsGrounded())
		{
			mover.velocity = new Vector3 (mover.velocity.x, 0f, mover.velocity.z);
			mover.currentState = MovementState.walking;
			Debug.Log ("Switch to walking");
		}
    }

	public override void HandleCollision(CharacterMover mover, ControllerColliderHit hit)
	{
		//Wall jump
		if (mover.kpJump)
		{
			mover.velocity = hit.normal.normalized * mover.wallJumpSpeed;
			mover.velocity = new Vector3 (mover.velocity.x, mover.jumpSpeed, mover.velocity.z);
			mover.gravity= new Vector3(0f, -0.3f, 0f);
		}

    }
}
