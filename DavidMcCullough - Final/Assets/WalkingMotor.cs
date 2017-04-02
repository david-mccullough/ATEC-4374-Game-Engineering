using UnityEngine;

public class WalkingMotor : BaseMotor
{
	public override void UpdateMotor(CharacterMover mover)
	{ 
		//Movement move = new Movement();
		mover.Move();

		//jumping
		if (mover.kpJump != 0)
		{
			float yVel = mover.jumpSpeed;
			mover.velocity += new Vector3 (0f, yVel, 0f);
		}
		//variable height
		if (mover.krJump != 0 && mover.velocity.y > 0)
		{
			float tempY = mover.velocity.y;
			Vector3 tempVel = new Vector3 (mover.velocity.x, tempY * 0.25f, mover.velocity.z);
			mover.velocity = tempVel;
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
