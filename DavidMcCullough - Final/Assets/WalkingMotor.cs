﻿using UnityEngine;

public class WalkingMotor : BaseMotor
{
	public override void UpdateMotor(CharacterMover mover)
	{ 
		//apply Friction
		float speed = mover.velocity.magnitude;
		if (speed != 0) //avoid dividing by zero
		{
			float drop = speed * mover.friction * Time.fixedDeltaTime;
			mover.velocity *= Mathf.Max(speed - drop, 0) / speed; //scale the velocity based on friction.
		}
			
		mover.velocity = mover.Accelerate(mover.accelDir, mover.velocity, mover.groundAccel, mover.maxGroundSpeed);


		//jumping
		if (mover.kpJump)
		{
			mover.velocity = new Vector3 (mover.velocity.x, mover.jumpSpeed, mover.velocity.z);
		}
		//variable height
		if (mover.krJump && mover.velocity.y > 0)
		{
			float tempY = mover.velocity.y;
			Vector3 tempVel = new Vector3 (mover.velocity.x, tempY * 0.25f, mover.velocity.z);
			mover.velocity = tempVel;
		}

		mover.charController.Move (mover.velocity);

		//floor no longer beneath us, switch state
		if (!mover.IsGrounded ()) {
			mover.currentState = MovementState.falling;
			Debug.Log ("Switch to falling");
		}

	}
		
	public override void HandleCollision(CharacterMover mover, ControllerColliderHit hit)
	{
		//floor no longer beneath us, switch state
		/*if (!mover.IsGrounded ()) {
			mover.currentState = MovementState.falling;
			Debug.Log ("Switch to falling");
		}*/
	}
}
