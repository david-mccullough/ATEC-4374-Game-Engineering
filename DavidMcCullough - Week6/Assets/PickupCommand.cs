using UnityEngine;

public class PickupCommand : BaseCommand {

	GameObject targetObject;

	public PickupCommand(GameObject targetObject)
	{
		this.targetObject = targetObject;
	}

	public override bool Execute(ControllableCharacter character)
	{
		GameObject.Destroy (targetObject.gameObject);
		character.itemCount++;;
		return true;
	}
}