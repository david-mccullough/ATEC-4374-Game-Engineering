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
	public MainCamera camera;

	//movement vars
    public Vector3 velocity;
	public Vector3 accelDir;

	public float maxGroundSpeed = .2f;
	public float maxAirSpeed = .3f;

	public float groundAccel = .1f;
	public float airAccel = .075f;
	public float friction = .1f;

	public float jumpSpeed = 0.1f;
	public float wallJumpSpeed = .13f;
	public Vector3 gravity = new Vector3(0f, -0.4f, 0f);

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
		//float camAngle = Mathf.Deg2Rad*camera.GetCameraAngle();
		//Vector3 left, right, forward, back;

		//Vector3 relativeDir = new Vector3((Mathf.Cos(camAngle)), 0f, (Mathf.Sin(camAngle)));
		//Debug.Log(Mathf.Cos(camAngle));

		Transform camTransform = camera.transform;
		Vector3 camForward = camTransform.TransformDirection(Vector3.forward);
		camForward.y = 0f;
		camForward = camForward.normalized;
		Vector3 camRight = new Vector3(camForward.z, 0f, -camForward.x);

		float v = Input.GetAxisRaw("Vertical");
		float h = Input.GetAxisRaw("Horizontal");

		accelDir = h * camRight + v * camForward;
	}

	public Vector3 Accelerate(Vector3 accelDir, Vector3 prevVelocity, float accelerate, float max_velocity)
	{
		float projVel = Vector3.Dot(prevVelocity, accelDir); // Vector projection of Current velocity onto accelDir.
		float accelVel = accelerate * Time.fixedDeltaTime; // Accelerated velocity in direction of movment

		// If necessary, truncate the accelerated velocity so the vector projection does not exceed max_velocity
		if(projVel + accelVel > max_velocity)
			accelVel = max_velocity - projVel;

		Vector3 newVel = prevVelocity + accelDir * accelVel;

		Vector3 tempVel = new Vector3(newVel.x, 0f, newVel.z);
		gameObject.transform.LookAt(gameObject.transform.position + tempVel, Vector3.up);
			
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
		//+Mathf.Abs(dVelocity)
		//Debug.Log(dVelocity);

		RaycastHit rayHit;
		if (Physics.SphereCast (transform.position, radius, Vector3.down, out rayHit, radius/4, collisionMask)) {
			return true;
		} else {
			return false;
		}
	}
}
