using UnityEngine;

public class BaseMotor : MonoBehaviour {

	public virtual void UpdateMotor(CharacterMover mover){}
	public virtual void HandleCollision(CharacterMover mover, ControllerColliderHit hit){}
	public MovementState motorState;
}
