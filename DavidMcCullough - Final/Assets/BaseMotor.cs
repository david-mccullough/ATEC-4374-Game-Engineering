using UnityEngine;

public class BaseMotor : MonoBehaviour {

    public MovementState motorState;
    public virtual void UpdateMotor(CharacterMover mover){}
	public virtual void HandleCollision(CharacterMover mover, ControllerColliderHit hit){}	
}
