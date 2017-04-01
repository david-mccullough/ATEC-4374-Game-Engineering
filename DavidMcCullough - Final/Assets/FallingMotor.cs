using UnityEngine;

public class FallingMotor : BaseMotor {
	
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
        Vector3 gravity = new Vector3(0f, -.2f, 0f);
        mover.velocity += gravity * Time.deltaTime;

		//mover.velocity += new Vector3 (mover.hInput * mover.moveSpeed, 0f, mover.vInput * mover.moveSpeed);
		mover.charController.Move (mover.velocity);
    }

	public override void HandleCollision(CharacterMover mover, ControllerColliderHit hit)
	{
		RaycastHit rayHit;
		if (Physics.SphereCast (transform.position, mover.radius, Vector3.down, out rayHit, .1f, mover.collisionMask)) {
			mover.velocity = new Vector3 (mover.velocity.x, 0f, mover.velocity.z);
			mover.currentState = MovementState.walking;
			Debug.Log ("Switch to walking");
		}
    }
}
