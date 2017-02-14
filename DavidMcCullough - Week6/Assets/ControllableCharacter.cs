using UnityEngine;

public class ControllableCharacter : MonoBehaviour {

	BaseCommand currentCommand;

	public float moveSpeed = 2f;

	void Update()
	{
		ExecuteCommands();
	}

	public void AddCommand(BaseCommand newCommand)
	{
		currentCommand = newCommand;
	}

	void ExecuteCommands()
	{
		//Do I have a command?
		if (currentCommand != null)
		{
			if (currentCommand.Execute(this))
				currentCommand = null;
		}
	}

	//********************************
	//Helper methods for commands
	public bool MoveToLocation(Vector3 targetLocation)
	{
		Vector3 currentPos = transform.position;

		Vector3 moveDelta = targetLocation - currentPos;

		if (moveDelta.magnitude < moveSpeed*Time.deltaTime)
		{
			transform.position = targetLocation;
			return true;
		}
		else
		{
			transform.position += moveDelta.normalized * moveSpeed *Time.deltaTime;
				return false;
		}
	}
}
