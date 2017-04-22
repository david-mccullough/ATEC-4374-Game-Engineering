using UnityEngine;

public class FallingMotor : BaseMotor {
	
	public override void UpdateMotor(CharacterMover mover)
	{
		mover.velocity = mover.Accelerate(mover.accelDir, mover.velocity, mover.airAccel, mover.maxAirSpeed);

		//variable height
		if (mover.krJump && mover.velocity.y > 0f)
		{
			mover.gravity= new Vector3(0f, mover.gSpeedVariable, 0f);
		}
		if (mover.velocity.y <= 0f)
		{
			if (mover.wallSlide)
				mover.gravity= new Vector3(0f, mover.gSpeed/4, 0f);
			else
				mover.gravity= new Vector3(0f, mover.gSpeed, 0f);
		}
		mover.velocity += mover.gravity * Time.deltaTime;
		mover.wallSlide = false;

		//mover.charController.Move (mover.velocity);
		if (mover.IsGrounded())
		{
			mover.velocity = new Vector3 (mover.velocity.x, 0f, mover.velocity.z);
			mover.currentState = MovementState.walking;
			mover.wallSlide = false;
			mover.gravity= new Vector3(0f, mover.gSpeed, 0f);
			Debug.Log ("Switch to walking");
		}
    }

	public override void HandleCollision(CharacterMover mover, ControllerColliderHit hit)
	{
		mover.wallSlide = false;
		if (mover.velocity.y < 0)
		{
			mover.wallSlide = true;
		}
		//Wall jump
		if (mover.kpJump)
		{
			mover.velocity = hit.normal.normalized * mover.wallJumpSpeed;
			mover.velocity += new Vector3 (mover.velocity.x, mover.jumpSpeed, mover.velocity.z);
			mover.gravity= new Vector3(0f, mover.gSpeed, 0f);

		}

    }
}
