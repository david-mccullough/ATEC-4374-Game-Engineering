using UnityEngine;

public class FallingMotor : BaseMotor {
	
	public override void UpdateMotor(CharacterMover mover)
	{
		mover.Move ();

		Vector3 gravity = new Vector3(0f, mover.gravity, 0f);
        mover.velocity += gravity * Time.deltaTime;

		//variable height
		if (mover.krJump != 0 && mover.velocity.y > 0)
		{
			float tempY = mover.velocity.y;
			Vector3 tempVel = new Vector3 (mover.velocity.x, tempY * 0.25f, mover.velocity.z);
			mover.velocity = tempVel;
		}

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
