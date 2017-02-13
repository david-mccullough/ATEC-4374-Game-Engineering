using UnityEngine;

public class BaseMotor : MonoBehaviour {

    public MovementState motorState;
    public virtual void UpdateMotor(CharacterMover mover){}
	public virtual void HandleCollision(CharacterMover mover, ControllerColliderHit hit){}
    public virtual void CheckForCollision(CharacterMover mover)
    {
        Vector3 startPos = transform.position;
        Vector3 currentPos = startPos;

        Vector3 moveDelta = mover.velocity * Time.deltaTime;

        RaycastHit hit;
        if (Physics.SphereCast(startPos, mover.radius, moveDelta.normalized, out hit, moveDelta.magnitude, mover.collisionMask))
        {
            currentPos += moveDelta.normalized * hit.distance;

            HandleCollision(mover, hit);
        }
        else
        {
            currentPos += moveDelta;
        }

        transform.position = currentPos;
    }
	
}
