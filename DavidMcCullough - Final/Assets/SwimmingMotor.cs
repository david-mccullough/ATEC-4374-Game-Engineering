using UnityEngine;

public class SwimmingMotor : BaseMotor
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
			

		mover.velocity += mover.gravity * .2f * Time.deltaTime;

		mover.velocity = mover.Accelerate(mover.accelDir, mover.velocity, mover.groundAccel, mover.maxGroundSpeed);

		//jumping
		if (mover.kpJump)
		{
			mover.velocity += new Vector3 (0f, mover.jumpSpeed, 0f);
		}
		//float upwards
		if (mover.kJump)
		{
			mover.velocity += mover.gravity * -.6f * Time.deltaTime;
		}

			
	}

	public override void HandleCollision(CharacterMover mover, ControllerColliderHit hit)
	{
		//placeholder
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Water")
		{
			this.gameObject.GetComponent<CharacterMover>().currentState = MovementState.walking;
			Debug.Log ("Switch to walking");
		}
	}
}
