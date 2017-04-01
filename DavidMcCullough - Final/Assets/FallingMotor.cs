using UnityEngine;

public class FallingMotor : BaseMotor {
	
	public override void UpdateMotor(CharacterMover mover)
	{
		float xVel = 0f;
		float zVel = 0f;
		int xDir = -mover.kLeft + mover.kRight;
		int zDir = -mover.kDown + mover.kUp;
		//Move along x axis
		xVel = xDir * mover.moveSpeed;

		//Move along y axis
		zVel = zDir * mover.moveSpeed;

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
