using UnityEngine;

public enum MovementState
{
	start,
	walking,
	falling
}

[RequireComponent(typeof(CharacterController))]
public class CharacterMover : MonoBehaviour {

	CharacterController charController;

	public Vector3 velocity;

	MovementState currentState;

	public BaseMotor startMotor;
	public BaseMotor walkingMotor;
	public BaseMotor fallingMotor;

	private void Awake()
	{
		charController = GetComponent<CharacterController>();
	}

	void GetMotors()
	{
		BaseMotor[] motors = GetComponents<BaseMotor>();
		for (int i = 0; i < motors.Length; i++)
		{
			
		}
	}

	private void Update()
	{
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

	private void OnCharacterColliderHit(ControllerColliderHit hit)
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
