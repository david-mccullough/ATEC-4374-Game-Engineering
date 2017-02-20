using UnityEngine;

public class DropCommand : BaseCommand {

	GameObject itemPrefab;

	public DropCommand()
	{
		itemPrefab = Resources.Load ("Item") as GameObject;
	}

	public override bool Execute(ControllableCharacter character)
	{
		if (character.itemCount > 0) {
			Vector3 location = new Vector3 (character.transform.position.x, 0.75f, character.transform.position.z);
			GameObject.Instantiate (itemPrefab, location, Quaternion.identity);
			character.itemCount--;
		}
		return true;
	}
}