using UnityEngine;

public class BaseMotor : MonoBehaviour {

	public virtual void UpdateMotor(CharacterController mover){}
	public virtual void HandleCollision(CharacterController mover, ControllerColliderHit hit){}
	public MovementState motorState;
}
