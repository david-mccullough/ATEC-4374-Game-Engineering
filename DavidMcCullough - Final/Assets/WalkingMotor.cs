﻿using UnityEngine;

public class WalkingMotor : BaseMotor
{
	public override void UpdateMotor(CharacterMover mover)
	{ 
		//apply Friction
		float speed = mover.velocity.magnitude;
		if (speed != 0) //avoid dividing by zero
		{
			float drop = speed * mover.friction * Time.deltaTime;
			mover.velocity *= Mathf.Max(speed - drop, 0) / speed; //scale the velocity based on friction.
		}
			
		mover.velocity = mover.Accelerate(mover.accelDir, mover.velocity, mover.groundAccel, mover.maxGroundSpeed);

		//jumping
		if (mover.kpJump)
		{
			mover.velocity = new Vector3 (mover.velocity.x, mover.jumpSpeed, mover.velocity.z);
			mover.gravity= new Vector3(0f, mover.gSpeed, 0f);
		}
			
		//floor no longer beneath us, switch state
		if (!mover.IsGrounded ()) {
			mover.currentState = MovementState.falling;
			Debug.Log ("Switch to falling");
		}
	}
		
	public override void HandleCollision(CharacterMover mover, ControllerColliderHit hit)
	{
		//placeholder
	}

	void OnTriggerStay(Collider other)
	{
		if (other.tag == "Water")
		{
			this.gameObject.GetComponent<CharacterMover>().currentState = MovementState.swimming;
			Debug.Log ("Switch to swimming");
		}
	}
}
