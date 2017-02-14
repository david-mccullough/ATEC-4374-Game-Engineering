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
	public float jumpSpeed = 1f;

	public MovementState currentState;

	public BaseMotor startMotor;
	public BaseMotor walkingMotor;
	public BaseMotor fallingMotor;

	public float moveSpeed;
	public bool wInput;
	public bool aInput;
	public bool sInput;
	public bool dInput;
	public bool jInput;

	private void Awake()
	{
		charController = GetComponent<CharacterController>();
		currentState = MovementState.start;
	}

	void GetInput()
	{
		wInput = Input.GetKey ("w");
		aInput = Input.GetKey ("a");
		sInput = Input.GetKey ("s");
		dInput = Input.GetKey ("d");
		jInput = Input.GetKeyDown ("space");
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
}
