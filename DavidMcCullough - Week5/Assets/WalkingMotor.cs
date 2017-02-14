using UnityEngine;

public class WalkingMotor : BaseMotor
{
	public override void UpdateMotor(CharacterMover mover)
	{
		float xVel = 0f;
		float zVel = 0f;
		//Move along x axis
		if ((mover.aInput && mover.dInput) || (!mover.aInput && !mover.dInput)) {
			xVel = 0;
		} else if (mover.aInput) {
			xVel = mover.moveSpeed;
		} else {
			xVel = -mover.moveSpeed;
		}

		//Move along y axis
		if ((mover.wInput && mover.sInput) || (!mover.wInput && !mover.sInput)) {
			zVel = 0;
		} else if (mover.wInput) {
			zVel = mover.moveSpeed;
		} else {
			zVel = -mover.moveSpeed;
		}
			
		mover.velocity = new Vector3 (zVel, mover.velocity.y, xVel);

		//jumping
		if (mover.jInput)
		{
			float yVel = mover.jumpSpeed;
			mover.velocity += new Vector3 (0f, yVel, 0f);
		}

		mover.charController.Move (mover.velocity);

		//Check if ground beneath is gone
		RaycastHit rayHit;
		if (Physics.SphereCast (transform.position, mover.radius, Vector3.down, out rayHit, 10f, mover.collisionMask)) {
			if (rayHit.distance > .5f)
			{
				mover.currentState = MovementState.falling;
				Debug.Log ("Switch to falling");
			}
		}
	}
		
}
