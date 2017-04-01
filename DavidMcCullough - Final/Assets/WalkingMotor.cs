using UnityEngine;

public class WalkingMotor : BaseMotor
{
	public override void UpdateMotor(CharacterMover mover)
	{
		float xVel = 0f;
		float zVel = 0f;
		int xDir = -mover.kLeft + mover.kRight;
		int zDir = -mover.kDown + mover.kUp;

		//find actual direction
		//this will prevernt doubling the player's speed when moving along both axis
		float direction = Vector2.Angle(new Vector2(1f, 0f), new Vector2((float)xDir, (float)zDir));
		Debug.Log (direction);

		//Move along x axis
		if (xDir != 0) {
			xVel = -Mathf.Cos (Mathf.Deg2Rad *direction) * mover.moveSpeed;
			Debug.Log ("xVel: " + xVel);
		}

		//Move along y axis
		if (zDir != 0) {
			zVel = Mathf.Sin (Mathf.Deg2Rad *direction) * mover.moveSpeed*zDir;
			Debug.Log ("zVel: " + zVel);
		}
			
		mover.velocity = new Vector3 (zVel, mover.velocity.y, xVel);

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
