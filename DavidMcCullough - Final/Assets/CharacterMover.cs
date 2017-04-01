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
}
