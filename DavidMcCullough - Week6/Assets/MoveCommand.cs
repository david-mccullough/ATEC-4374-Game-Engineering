using UnityEngine;

public class MoveCommand : BaseCommand {

	Vector3 targetLocation;

	public MoveCommand(Vector3 targetLocation)
	{
		this.targetLocation = targetLocation;
	}

	public override bool Execute(ControllableCharacter character)
	{
		Vector3 offset = new Vector3 (targetLocation.x, 1f, targetLocation.z);
		return character.MoveToLocation(offset);
	}
}