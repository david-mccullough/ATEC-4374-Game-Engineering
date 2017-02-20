using System.Collections.Generic;
using UnityEngine;

public class ControllableCharacter : MonoBehaviour {

	public Queue<BaseCommand> commandQueue = new Queue<BaseCommand>();
	BaseCommand currentCommand;

	public float moveSpeed = 2f;
	public int itemCount = 0;

	void Update()
	{
		ExecuteCommands();
	}

	public void AddCommand(BaseCommand newCommand)
	{
		commandQueue.Enqueue(newCommand);
	}

	void ExecuteCommands()
	{
		if (commandQueue.Count != 0)
		{
		//Do I have a command?
		BaseCommand currentCommand = commandQueue.Peek();
		if (currentCommand.Execute(this))
			commandQueue.Dequeue();
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
	//*********************************

	public void ClearCommands()
	{
		commandQueue.Clear ();
	}
}
