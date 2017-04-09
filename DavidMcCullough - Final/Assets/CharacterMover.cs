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

	//movement vars
    public Vector3 velocity;
	public Vector3 accelDir;

	public float maxGroundSpeed = .1f;
	public float maxAirSpeed = .2f;

	public float groundAccel = .01f;
	public float airAccel = .01f;
	public float friction = .02f;

	public float jumpSpeed = 0.1f;
	public float gravity = -0.2f;

	//states
	public MovementState currentState;

	public BaseMotor startMotor;
	public BaseMotor walkingMotor;
	public BaseMotor fallingMotor;

	//input bools
	public bool kUp;
	public bool kDown;
	public bool kRight;
	public bool kLeft;
	public bool kpJump;
	public bool krJump;

	private void Awake()
	{
		charController = GetComponent<CharacterController>();
		currentState = MovementState.start;
	}

	private void Update()
	{
		GetInput ();
		SetAccelDir ();
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

	private void GetInput()
	{
		kUp 	= Input.GetKey ("w");
		kLeft 	= Input.GetKey ("a");
		kRight 	= Input.GetKey ("d");
		kDown 	= Input.GetKey ("s");
		kpJump 	= Input.GetKeyDown ("space");
		krJump 	= Input.GetKeyUp ("space");
	}

	//determines where we will direct our acceleration 
	//this frame based on input
	private void SetAccelDir()
	{
		accelDir = Vector3.zero;
		if (kLeft)
			accelDir += Vector3.left;
		if (kLeft)
			accelDir += Vector3.right;
		if (kUp)
			accelDir += Vector3.forward;
		if (kDown)
			accelDir += Vector3.back;
	}

	public Vector3 Accelerate(Vector3 accelDir, Vector3 prevVelocity, float accelerate, float max_velocity)
	{
		float projVel = Vector3.Dot(prevVelocity, accelDir); // Vector projection of Current velocity onto accelDir.
		float accelVel = accelerate * Time.fixedDeltaTime; // Accelerated velocity in direction of movment

		// If necessary, truncate the accelerated velocity so the vector projection does not exceed max_velocity
		if(projVel + accelVel > max_velocity)
			accelVel = max_velocity - projVel;

		return prevVelocity + accelDir * accelVel;
	}

	public bool IsGrounded()
	{
		//getting downward component of vector, if approaching floor
		float dVelocity = 0f;
		if (velocity.y < 0f)
		{
			dVelocity = velocity.y;
		}

		RaycastHit rayHit;
		if (Physics.SphereCast (transform.position, radius, Vector3.down, out rayHit, dVelocity, collisionMask)) {
			return true;
		} else {
			return false;
		}
	}
}
