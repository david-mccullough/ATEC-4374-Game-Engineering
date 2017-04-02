using UnityEngine;

public enum MovementState
{
	start,
	walking,
	falling
}

[RequireComponent(typeof(CharacterController))]
public class CharacterMover : MonoBehaviour {

	public CharacterController charController;
    public float radius = .5f;
    public LayerMask collisionMask;
    public Vector3 velocity;

	public float moveSpeed = 0f;
	public float maxSpeed = .1f;
	public float acceleration = .01f;
	public float deceleration = .02f;
	public float jumpSpeed = 0.1f;
	public float gravity = -0.2f;
	float xVel = 0f;
	float zVel = 0f;
	float direction = 0f;

	public MovementState currentState;

	public BaseMotor startMotor;
	public BaseMotor walkingMotor;
	public BaseMotor fallingMotor;

	public int kUp;
	public int kDown;
	public int kRight;
	public int kLeft;
	public int kpJump;
	public int krJump;

	private void Awake()
	{
		charController = GetComponent<CharacterController>();
		currentState = MovementState.start;
	}

	void GetInput()
	{
		kUp 	= Input.GetKey ("w") ? 1:0;
		kLeft 	= Input.GetKey ("a") ? 1:0;
		kRight 	= Input.GetKey ("d") ? 1:0;
		kDown 	= Input.GetKey ("s") ? 1:0;
		kpJump 	= Input.GetKeyDown ("space") ? 1:0;
		krJump 	= Input.GetKeyUp ("space") ? 1:0;
	}

	private void Update()
	{
		GetInput ();
		switch(currentState)
		{
			case MovementState.start:
				startMotor.UpdateMotor(this);
				break;
			case MovementState.walking:
				walkingMotor.UpdateMotor(this);
				break;

			case MovementState.falling:
				fallingMotor.UpdateMotor(this);
				break;
		}
	}

	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		switch(currentState)
		{
		case MovementState.start:
			startMotor.HandleCollision(this, hit);
			break;
		case MovementState.walking:
			walkingMotor.HandleCollision(this, hit);
			break;

		case MovementState.falling:
			fallingMotor.HandleCollision(this, hit);
			break;
		}
	}

	public void Move()
	{
		int xDir = -kLeft + kRight;
		int zDir = -kDown + kUp;

		Math math = new Math ();
		if (xDir != 0 || zDir != 0) {
			moveSpeed = math.Approach (moveSpeed, maxSpeed, acceleration*Time.deltaTime);
		} else {
			moveSpeed = math.Approach (moveSpeed, 0, deceleration*Time.deltaTime);
		}

		//find actual direction
		//this will prevernt doubling the player's speed when moving along both axis
		if (xDir != 0 || zDir != 0) {
			float direction = Vector2.Angle (new Vector2 (1f, 0f), new Vector2 ((float)xDir, (float)zDir));
			Debug.Log (direction);

			//Move along x axis
			xVel = -Mathf.Cos (Mathf.Deg2Rad * direction) * moveSpeed;
			//Move along y axis
			zVel = Mathf.Sin (Mathf.Deg2Rad * direction) * moveSpeed * Mathf.Sign (zDir);
		} else {
			//Move along x axis
			xVel = 0f;
			//Move along y axis
			zVel = 0f;
		}



		velocity = new Vector3 (zVel, velocity.y, xVel);
	}
}
